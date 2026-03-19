using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace storesach.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindCategories();
        }

        void BindCategories()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Categories", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("EXEC sp_AddCategory @name", con);
                cmd.Parameters.AddWithValue("@name", txtCategory.Text);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            txtCategory.Text = "";
            BindCategories();
            lblMessage.Text = "✅ Thêm thể loại thành công!";
            lblMessage.CssClass = "text-success fw-bold";
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvCategories.DataKeys[e.RowIndex].Value;

            // Kiểm tra thể loại này có sách không
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Books WHERE CategoryId=@id", con);
                checkCmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // Có sách -> Không cho xóa
                    lblMessage.Text = "⚠️ Không thể xóa vì thể loại này còn sách!";
                    lblMessage.CssClass = "text-danger fw-bold";
                    return;
                }
            }

            // Xóa thể loại nếu không có sách
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("EXEC sp_DeleteCategory @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            BindCategories();
            lblMessage.Text = "🗑️ Đã xóa thể loại thành công!";
            lblMessage.CssClass = "text-success fw-bold";
        }

        // Thêm confirm xóa khi render GridView
        protected void gvCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvCategories.EditIndex)
            {
                LinkButton btn = (LinkButton)e.Row.Cells[2].Controls[0];
                if (btn != null && btn.CommandName == "Delete")
                {
                    btn.OnClientClick = "return confirm('Bạn có chắc chắn muốn xóa thể loại này không?');";
                }
            }
        }
    }
}
