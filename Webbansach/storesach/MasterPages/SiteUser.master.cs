using System;
using System.Data;

namespace storesach.MasterPages
{
    public partial class SiteUser : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int count = 0;
                if (Session["Cart"] != null)
                {
                    DataTable cart = (DataTable)Session["Cart"];
                    count = cart.Rows.Count;
                }
                lnkCart.Text = "🛒 Giỏ hàng (" + count + ")";
            }
        }

        // Sự kiện click nút tìm kiếm global
        protected void btnSearchGlobal_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchGlobal.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                Response.Redirect("~/Users/Books.aspx?search=" + Server.UrlEncode(keyword));
            }
        }
    }
}
