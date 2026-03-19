using System;
using System.Data;
using System.Data.SqlClient;

namespace storesach.Users
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void btnOrder_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
            {
                lblMessage.Text = "Giỏ hàng trống!";
                return;
            }

            DataTable cart = (DataTable)Session["Cart"];
            decimal total = 0;
            foreach (DataRow r in cart.Rows)
                total += (decimal)r["Price"] * (int)r["Quantity"];

            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Insert order
                    SqlCommand cmdOrder = new SqlCommand("INSERT INTO Orders(UserId,Total) OUTPUT INSERTED.OrderId VALUES(@uid,@total)", con, tran);
                    cmdOrder.Parameters.AddWithValue("@uid", Session["UserId"]);
                    cmdOrder.Parameters.AddWithValue("@total", total);
                    int orderId = (int)cmdOrder.ExecuteScalar();

                    // Insert order details
                    foreach (DataRow r in cart.Rows)
                    {
                        SqlCommand cmdDetail = new SqlCommand("INSERT INTO OrderDetails(OrderId,BookId,Quantity,Price) VALUES(@oid,@bid,@q,@p)", con, tran);
                        cmdDetail.Parameters.AddWithValue("@oid", orderId);
                        cmdDetail.Parameters.AddWithValue("@bid", r["BookId"]);
                        cmdDetail.Parameters.AddWithValue("@q", r["Quantity"]);
                        cmdDetail.Parameters.AddWithValue("@p", r["Price"]);
                        cmdDetail.ExecuteNonQuery();
                    }

                    tran.Commit();
                    Session["Cart"] = null;
                    lblMessage.Text = "✅ Đặt hàng thành công!";
                }
                catch
                {
                    tran.Rollback();
                    lblMessage.Text = "❌ Có lỗi khi đặt hàng!";
                }
            }
        }
    }
}
