using System;

namespace storesach.Users
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Thực tế có thể lưu vào DB hoặc gửi email
            lblContact.Text = "✅ Cảm ơn bạn " + txtName.Text + ", chúng tôi sẽ phản hồi sớm!";
        }
    }
}
