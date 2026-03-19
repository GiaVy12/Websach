using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace storesach.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadOrders();
        }

        void LoadOrders(string fromDate = null, string toDate = null)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT OrderId, UserId, OrderDate, Total FROM Orders WHERE 1=1";
                if (!string.IsNullOrEmpty(fromDate))
                    sql += " AND OrderDate >= @from";
                if (!string.IsNullOrEmpty(toDate))
                    sql += " AND OrderDate <= @to";

                SqlCommand cmd = new SqlCommand(sql, con);
                if (!string.IsNullOrEmpty(fromDate))
                    cmd.Parameters.AddWithValue("@from", fromDate);
                if (!string.IsNullOrEmpty(toDate))
                    cmd.Parameters.AddWithValue("@to", toDate);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string fromDate = string.IsNullOrEmpty(txtFrom.Text) ? null : txtFrom.Text;
            string toDate = string.IsNullOrEmpty(txtTo.Text) ? null : txtTo.Text;
            LoadOrders(fromDate, toDate);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFrom.Text = "";
            txtTo.Text = "";
            LoadOrders();
        }

        protected void gvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(gvOrders.SelectedDataKey.Value);

            // Lấy thông tin khách hàng
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmdUser = new SqlCommand(@"
                    SELECT u.FullName, u.Username, u.Email, u.Phone, u.Address
                    FROM Users u
                    JOIN Orders o ON u.UserId=o.UserId
                    WHERE o.OrderId=@oid", con);
                cmdUser.Parameters.AddWithValue("@oid", orderId);

                SqlDataAdapter daUser = new SqlDataAdapter(cmdUser);
                DataTable dtUser = new DataTable();
                daUser.Fill(dtUser);
                dvCustomer.DataSource = dtUser;
                dvCustomer.DataBind();
            }

            // Lấy chi tiết đơn hàng
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT b.Title, od.Quantity, od.Price, (od.Quantity*od.Price) AS Total
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

        // 🟢 Bổ sung hàm này để tránh lỗi CS1061
        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int orderId = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmdDetail = new SqlCommand("DELETE FROM OrderDetails WHERE OrderId=@id", con, tran);
                    cmdDetail.Parameters.AddWithValue("@id", orderId);
                    cmdDetail.ExecuteNonQuery();

                    SqlCommand cmdOrder = new SqlCommand("DELETE FROM Orders WHERE OrderId=@id", con, tran);
                    cmdOrder.Parameters.AddWithValue("@id", orderId);
                    cmdOrder.ExecuteNonQuery();

                    tran.Commit();
                    lblMessage.Text = "✅ Đã xóa đơn hàng #" + orderId;
                }
                catch
                {
                    tran.Rollback();
                    lblMessage.Text = "❌ Lỗi khi xóa đơn hàng!";
                }
            }

            LoadOrders();
        }
    }
}
