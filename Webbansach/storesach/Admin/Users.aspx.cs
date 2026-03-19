using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace storesach.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindUsers();
        }

        void BindUsers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT UserId, Username, FullName, Email, Phone, Address, Role FROM Users", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            BindUsers();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gvUsers.DataKeys[e.RowIndex].Value;

            GridViewRow row = gvUsers.Rows[e.RowIndex];
            string fullName = ((TextBox)row.Cells[2].Controls[0]).Text;
            string email = ((TextBox)row.Cells[3].Controls[0]).Text;
            string phone = ((TextBox)row.Cells[4].Controls[0]).Text;
            string address = ((TextBox)row.Cells[5].Controls[0]).Text;
            string role = ((TextBox)row.Cells[6].Controls[0]).Text;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"UPDATE Users 
                       SET FullName=@name, Email=@mail, Phone=@phone, Address=@addr, Role=@role
                       WHERE UserId=@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", fullName);
                cmd.Parameters.AddWithValue("@mail", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@addr", address);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvUsers.EditIndex = -1;
            BindUsers();
        }


        protected void gvUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = (int)gvUsers.DataKeys[e.RowIndex].Value;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // Kiểm tra xem user có đơn hàng không
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE UserId=@id", con);
                checkCmd.Parameters.AddWithValue("@id", id);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // Có đơn hàng => không cho xóa
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert", "alert('⚠️ Không thể xóa vì người dùng này còn đơn hàng!');", true);
                    return;
                }

                // Nếu không có đơn hàng thì xóa
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert", "alert('🗑️ Xóa người dùng thành công!');", true);
            }

            BindUsers();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvUsers.EditIndex)
            {
                foreach (Control ctrl in e.Row.Cells[e.Row.Cells.Count - 1].Controls)
                {
                    if (ctrl is LinkButton btn && btn.CommandName == "Delete")
                    {
                        btn.OnClientClick = "return confirm('Bạn có chắc chắn muốn xóa người dùng này không?');";
                    }
                }
            }
        }


    }
}
