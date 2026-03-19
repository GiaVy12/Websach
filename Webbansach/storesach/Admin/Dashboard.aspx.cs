using System;
using System.Data.SqlClient;

namespace storesach.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadStats();
        }

        void LoadStats()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlCommand cmdBooks = new SqlCommand("SELECT COUNT(*) FROM Books", con);
                lblBooks.Text = cmdBooks.ExecuteScalar().ToString();

                SqlCommand cmdOrders = new SqlCommand("SELECT COUNT(*) FROM Orders", con);
                lblOrders.Text = cmdOrders.ExecuteScalar().ToString();

                SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Role='Customer'", con);
                lblUsers.Text = cmdUsers.ExecuteScalar().ToString();

                SqlCommand cmdRevenue = new SqlCommand("SELECT ISNULL(SUM(Total),0) FROM Orders", con);
                lblRevenue.Text = Convert.ToDecimal(cmdRevenue.ExecuteScalar()).ToString("N0") + " VNĐ";

            }
        }
    }
}
