using System;
using System.Data.SqlClient;
using storesach.Helpers; // thêm namespace để dùng ValidationHelper

namespace storesach.Users
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // ✅ Kiểm tra độ dài tối thiểu
            if (!ValidationHelper.MinLength(txtUsername.Text, 5) || !ValidationHelper.MinLength(txtPassword.Text, 5))
            {
                lblMessage.CssClass = "mt-2 d-block text-danger";
                lblMessage.Text = "⚠ Tên đăng nhập và mật khẩu phải có ít nhất 5 ký tự!";
                return;
            }

            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Session["UserId"] = dr["UserId"];
                    Session["Username"] = dr["Username"];
                    Session["Role"] = dr["Role"];

                    if (dr["Role"].ToString() == "Admin")
                        Response.Redirect("/Admin/Dashboard.aspx");
                    else
                        Response.Redirect("/Users/Default.aspx");
                }
                else
                {
                    lblMessage.CssClass = "mt-2 d-block text-danger";
                    lblMessage.Text = "❌ Sai tên đăng nhập hoặc mật khẩu!";
                }
            }
        }
    }
}
