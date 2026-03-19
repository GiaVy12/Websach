using System;

namespace storesach.Users
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();       // Xóa toàn bộ session
            Session.Abandon();     // Hủy session
            Response.Redirect("~/Users/Default.aspx"); // Quay về trang chủ
        }
    }
}
