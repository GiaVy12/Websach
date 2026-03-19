using System;
using System.Data;
using System.Data.SqlClient;

namespace storesach.Admin
{
    public partial class Books : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                Response.Redirect("~/Users/Login.aspx");

            if (!IsPostBack)
            {
                BindCategories();
                BindBooks();
            }
        }

        // ======= Load Thể loại =======
        void BindCategories()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryId, Name FROM Categories", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
            }
        }

        // ======= Load Sách =======
        void BindBooks()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT b.BookId, b.Title, b.Author, b.Price, c.Name AS CategoryName " +
                    "FROM Books b LEFT JOIN Categories c ON b.CategoryId = c.CategoryId", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvBooks.DataSource = dt;
                gvBooks.DataBind();
            }
        }

        // ======= Lưu (Thêm / Sửa) =======
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd;

                if (string.IsNullOrEmpty(hfBookId.Value)) // Thêm mới
                {
                    cmd = new SqlCommand("INSERT INTO Books (Title, Author, Price, Image, CategoryId) VALUES (@t,@a,@p,@i,@c)", con);
                }
                else // Cập nhật
                {
                    cmd = new SqlCommand("UPDATE Books SET Title=@t, Author=@a, Price=@p, Image=@i, CategoryId=@c WHERE BookId=@id", con);
                    cmd.Parameters.AddWithValue("@id", hfBookId.Value);
                }

                cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@a", txtAuthor.Text.Trim());
                cmd.Parameters.AddWithValue("@p", decimal.Parse(txtPrice.Text.Trim()));
                cmd.Parameters.AddWithValue("@i", txtImage.Text.Trim());
                cmd.Parameters.AddWithValue("@c", ddlCategory.SelectedValue);

                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "✅ Lưu thành công!";
            ClearForm();
            BindBooks();
        }

        // ======= Nút Edit / Delete trong GridView =======
        protected void gvBooks_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int bookId = Convert.ToInt32(gvBooks.DataKeys[index].Value);

            if (e.CommandName == "EditBook") // Load dữ liệu lên form
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@id", con);
                    cmd.Parameters.AddWithValue("@id", bookId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hfBookId.Value = dr["BookId"].ToString();
                        txtTitle.Text = dr["Title"].ToString();
                        txtAuthor.Text = dr["Author"].ToString();
                        txtPrice.Text = dr["Price"].ToString();
                        txtImage.Text = dr["Image"].ToString();
                        ddlCategory.SelectedValue = dr["CategoryId"].ToString();
                    }
                }
            }
            else if (e.CommandName == "DeleteBook")
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    // Kiểm tra có tồn tại trong OrderDetails không
                    SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM OrderDetails WHERE BookId=@id", con);
                    check.Parameters.AddWithValue("@id", bookId);
                    int count = (int)check.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Text = "❌ Không thể xóa vì sách đã có trong đơn hàng!";
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE BookId=@id", con);
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.ExecuteNonQuery();
                }
                lblMessage.Text = "🗑️ Đã xóa thành công!";
                BindBooks();
            }

        }

        // ======= Nút Hủy =======
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void ClearForm()
        {
            hfBookId.Value = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPrice.Text = "";
            txtImage.Text = "";
            ddlCategory.SelectedIndex = 0;
        }
    }
}
