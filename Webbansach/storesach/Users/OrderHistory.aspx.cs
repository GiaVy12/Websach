using System;
using System.Data;
using System.Data.SqlClient;

namespace storesach.Users
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        void LoadOrders()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT OrderId, OrderDate, Total FROM Orders WHERE UserId=@uid", con);
                cmd.Parameters.AddWithValue("@uid", (int)Session["UserId"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void gvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(gvOrders.SelectedDataKey.Value);
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT b.Title, od.Quantity, od.Price, (od.Quantity*od.Price) AS Total
                                                  FROM OrderDetails od
                                                  JOIN Books b ON od.BookId=b.BookId
                                                  WHERE od.OrderId=@oid", con);
                cmd.Parameters.AddWithValue("@oid", orderId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrderDetails.DataSource = dt;
                gvOrderDetails.DataBind();
            }
        }
    }
}
