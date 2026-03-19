using System;
using System.Data.SqlClient;

namespace storesach.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM Users WHERE Username=@u AND Password=@p AND Role='Admin'";
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
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblMessage.Text = "Sai tài khoản hoặc không phải Admin!";
                }
            }
        }
    }
}
