using System;
using System.Data.SqlClient;
using storesach.Helpers; // để dùng ValidationHelper

namespace storesach.Users
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
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
                // ✅ Kiểm tra trùng tên đăng nhập
                string checkSql = "SELECT COUNT(*) FROM Users WHERE Username=@u";
                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@u", txtUsername.Text);

                con.Open();
                int exists = (int)checkCmd.ExecuteScalar();
                if (exists > 0)
                {
                    lblMessage.CssClass = "mt-2 d-block text-danger";
                    lblMessage.Text = "⚠ Tên đăng nhập đã tồn tại!";
                    return;
                }

                // ✅ Thêm user mới
                string sql = "INSERT INTO Users(Username,Password,FullName,Role) VALUES(@u,@p,@f,'Customer')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);
                cmd.Parameters.AddWithValue("@f", txtFullName.Text);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    lblMessage.CssClass = "mt-2 d-block text-success";
                    lblMessage.Text = "✅ Đăng ký thành công! Hãy đăng nhập.";
                }
                else
                {
                    lblMessage.CssClass = "mt-2 d-block text-danger";
                    lblMessage.Text = "❌ Lỗi đăng ký!";
                }
            }
        }
    }
}
