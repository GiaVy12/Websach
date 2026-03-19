using System;
using System.Data;
using System.Data.SqlClient;

namespace storesach.Users
{
    public partial class Cart : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["add"] != null)
                {
                    int bookId = int.Parse(Request.QueryString["add"]);
                    AddToCart(bookId);
                }
                BindCart();
            }
        }

        // Thêm sách vào giỏ
        void AddToCart(int bookId)
        {
            DataTable cart = Session["Cart"] as DataTable;
            if (cart == null)
            {
                cart = new DataTable();
                cart.Columns.Add("BookId", typeof(int));
                cart.Columns.Add("Title", typeof(string));
                cart.Columns.Add("Quantity", typeof(int));
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Total", typeof(decimal));
            }

            DataRow[] rows = cart.Select("BookId=" + bookId);
            if (rows.Length > 0)
            {
                rows[0]["Quantity"] = (int)rows[0]["Quantity"] + 1;
                rows[0]["Total"] = (int)rows[0]["Quantity"] * (decimal)rows[0]["Price"];
            }
            else
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Title, Price FROM Books WHERE BookId=@id", con);
                    cmd.Parameters.AddWithValue("@id", bookId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        DataRow row = cart.NewRow();
                        row["BookId"] = bookId;
                        row["Title"] = dr["Title"].ToString();
                        row["Quantity"] = 1;
                        row["Price"] = (decimal)dr["Price"];
                        row["Total"] = row["Price"];
                        cart.Rows.Add(row);
                    }
                }
            }

            Session["Cart"] = cart;
        }

        // Hiển thị giỏ hàng
        void BindCart()
        {
            DataTable cart = Session["Cart"] as DataTable;
            if (cart != null && cart.Rows.Count > 0)
            {
                gvCart.DataSource = cart;
                gvCart.DataBind();

                decimal total = 0;
                foreach (DataRow r in cart.Rows)
                {
                    total += (decimal)r["Total"];
                }
                lblTotal.Text = "Tổng cộng: " + total.ToString("N0") + " VNĐ";
            }
            else
            {
                gvCart.DataSource = null;
                gvCart.DataBind();
                lblTotal.Text = "";
            }
        }

        // Xóa sách khỏi giỏ
        protected void gvCart_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable cart = Session["Cart"] as DataTable;
                if (cart != null && index < cart.Rows.Count)
                {
                    cart.Rows.RemoveAt(index);
                    Session["Cart"] = cart;
                    BindCart();
                }
            }
        }

        // Bấm "Thanh toán" -> hiện form nhập thông tin
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            pnlCheckout.Visible = true;
        }

        // Bấm "Xác nhận đặt hàng"
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                lblMessage.Text = "❌ Bạn cần đăng nhập để đặt hàng!";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                lblMessage.Text = "❌ Vui lòng nhập đầy đủ thông tin giao hàng!";
                return;
            }

            DataTable cart = Session["Cart"] as DataTable;
            if (cart == null || cart.Rows.Count == 0)
            {
                lblMessage.Text = "❌ Giỏ hàng trống!";
                return;
            }

            int userId = (int)Session["UserId"];
            decimal total = 0;
            foreach (DataRow r in cart.Rows) total += (decimal)r["Total"];

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Lưu Order
                    SqlCommand cmdOrder = new SqlCommand(@"
                        INSERT INTO Orders (UserId, FullName, Phone, Address, PaymentMethod, Total)
                        OUTPUT INSERTED.OrderId
                        VALUES (@uid, @name, @phone, @addr, @payment, @total)", con, tran);

                    cmdOrder.Parameters.AddWithValue("@uid", userId);
                    cmdOrder.Parameters.AddWithValue("@name", txtName.Text);
                    cmdOrder.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmdOrder.Parameters.AddWithValue("@addr", txtAddress.Text);
                    cmdOrder.Parameters.AddWithValue("@payment", rblPayment.SelectedValue);
                    cmdOrder.Parameters.AddWithValue("@total", total);

                    int orderId = (int)cmdOrder.ExecuteScalar();

                    // Lưu OrderDetails
                    foreach (DataRow r in cart.Rows)
                    {
                        SqlCommand cmdDetail = new SqlCommand(
                            "INSERT INTO OrderDetails (OrderId, BookId, Quantity, Price) VALUES (@oid, @bid, @qty, @price)",
                            con, tran);

                        cmdDetail.Parameters.AddWithValue("@oid", orderId);
                        cmdDetail.Parameters.AddWithValue("@bid", (int)r["BookId"]);
                        cmdDetail.Parameters.AddWithValue("@qty", (int)r["Quantity"]);
                        cmdDetail.Parameters.AddWithValue("@price", (decimal)r["Price"]);
                        cmdDetail.ExecuteNonQuery();
                    }

                    tran.Commit();
                    Session["Cart"] = null;
                    lblMessage.Text = "✅ Đặt hàng thành công! Cảm ơn bạn.";
                    BindCart();
                    pnlCheckout.Visible = false;
                }
                catch
                {
                    tran.Rollback();
                    lblMessage.Text = "❌ Lỗi khi đặt hàng, vui lòng thử lại.";
                }
            }
        }
    }
}
