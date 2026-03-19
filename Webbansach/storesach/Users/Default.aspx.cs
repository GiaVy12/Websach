using System;
using System.Data;
using System.Data.SqlClient;

namespace storesach.Users
{
    public partial class Default : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNewBooks();
                LoadBestSeller();
            }
        }
        void LoadNewBooks()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT TOP 8 * FROM Books ORDER BY BookId DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptNewBooks.DataSource = dt;
                rptNewBooks.DataBind();
            }
        }
        void LoadBestSeller()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT TOP 4 * FROM Books ORDER BY SoldCount DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptBestSeller.DataSource = dt;
                rptBestSeller.DataBind();
            }
        }
    }
}
