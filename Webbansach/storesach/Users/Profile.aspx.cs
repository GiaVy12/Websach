using System;
using System.Data.SqlClient;

namespace storesach.Users
{
    public partial class Profile : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack) LoadProfile();
        }

        void LoadProfile()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT Username, FullName, Email, Phone, Address FROM Users WHERE UserId=@id", con);
                cmd.Parameters.AddWithValue("@id", (int)Session["UserId"]);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtUsername.Text = dr["Username"].ToString();
                    txtFullName.Text = dr["FullName"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    txtPhone.Text = dr["Phone"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "UPDATE Users SET FullName=@name, Email=@mail, Phone=@phone, Address=@addr";
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    sql += ", Password=@pass";
                }
                sql += " WHERE UserId=@id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@addr", txtAddress.Text);
                if (!string.IsNullOrEmpty(txtPassword.Text))
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Parameters.AddWithValue("@id", (int)Session["UserId"]);

                con.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "✅ Cập nhật thành công!";
            }
        }

    }
}
