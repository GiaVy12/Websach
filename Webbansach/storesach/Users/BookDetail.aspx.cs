using System;
using System.Data.SqlClient;
using System.Data;

namespace storesach.Users
{
    public partial class BookDetail : System.Web.UI.Page
    {
        int bookId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                bookId = int.Parse(Request.QueryString["id"]);
                if (!IsPostBack) LoadBook();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        void LoadBook()
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@id", con);
                cmd.Parameters.AddWithValue("@id", bookId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblTitle.Text = dr["Title"].ToString();
                    lblAuthor.Text = dr["Author"].ToString();
                    lblPrice.Text = Convert.ToDecimal(dr["Price"]).ToString("N0") + " VND";
                    imgBook.ImageUrl = dr["Image"].ToString();
                    lblStock.Text = "Còn hàng";
                    litPreview.Text = dr["Preview"] != DBNull.Value
                        ? dr["Preview"].ToString()
                        : "Chưa có nội dung đọc thử.";
                }
            }
        }


        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
            {
                DataTable cart = new DataTable();
                cart.Columns.Add("BookId", typeof(int));
                cart.Columns.Add("Title");
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Quantity", typeof(int));
                cart.Columns.Add("Total", typeof(decimal)); // ✅ thêm cột Total
                Session["Cart"] = cart;
            }

            DataTable cartTable = (DataTable)Session["Cart"];
            bool exists = false;
            foreach (DataRow r in cartTable.Rows)
            {
                if ((int)r["BookId"] == bookId)
                {
                    r["Quantity"] = (int)r["Quantity"] + 1;
                    r["Total"] = (int)r["Quantity"] * (decimal)r["Price"]; // ✅ cập nhật Total
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@id", con);
                    cmd.Parameters.AddWithValue("@id", bookId);
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        DataRow row = cartTable.NewRow();
                        row["BookId"] = dr["BookId"];
                        row["Title"] = dr["Title"];
                        row["Price"] = (decimal)dr["Price"];
                        row["Quantity"] = 1;
                        row["Total"] = row["Price"]; // ✅ thành tiền ban đầu = giá
                        cartTable.Rows.Add(row);
                    }
                }
            }

            lblMessage.Text = "✅ Đã thêm vào giỏ!";
        }

    }
}
