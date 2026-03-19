using System;
using System.Data;
using System.Data.SqlClient;

namespace storesach.Users
{
    public partial class Books : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["StoreSachConn"].ConnectionString;

        
        static int pageSize = 12;   // mỗi trang 8 sách
        static int currentPage = 1;
        static int totalRecords = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                currentPage = 1;
                LoadCategories();

                // Nếu có từ khóa từ search bar trên master
                string keyword = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    txtSearch.Text = keyword;
                    LoadBooks(keyword, 0);
                }
                else
                {
                    LoadBooks();
                }
            }
        }


        void LoadCategories()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryId, Name FROM Categories", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Tất cả", "0"));
            }
        }

        void LoadBooks(string search = "", int categoryId = 0)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string countSql = "SELECT COUNT(*) FROM Books WHERE 1=1";
                if (!string.IsNullOrEmpty(search))
                    countSql += " AND (Title LIKE @search OR Author LIKE @search)";
                if (categoryId > 0)
                    countSql += " AND CategoryId=@cid";

                SqlCommand countCmd = new SqlCommand(countSql, con);
                if (!string.IsNullOrEmpty(search))
                    countCmd.Parameters.AddWithValue("@search", "%" + search + "%");
                if (categoryId > 0)
                    countCmd.Parameters.AddWithValue("@cid", categoryId);

                con.Open();
                totalRecords = (int)countCmd.ExecuteScalar();
                con.Close();

                int offset = (currentPage - 1) * pageSize;
                string sql = @"SELECT * FROM Books WHERE 1=1";
                if (!string.IsNullOrEmpty(search))
                    sql += " AND (Title LIKE @search OR Author LIKE @search)";
                if (categoryId > 0)
                    sql += " AND CategoryId=@cid";
                sql += " ORDER BY BookId DESC OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY";

                SqlCommand cmd = new SqlCommand(sql, con);
                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                if (categoryId > 0)
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@size", pageSize);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptAllBooks.DataSource = dt;
                rptAllBooks.DataBind();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                lblPage.Text = $"Trang {currentPage}/{(totalPages == 0 ? 1 : totalPages)}";

                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = currentPage < totalPages;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadBooks(txtSearch.Text.Trim(), int.Parse(ddlCategory.SelectedValue));
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadBooks(txtSearch.Text.Trim(), int.Parse(ddlCategory.SelectedValue));
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1) currentPage--;
            LoadBooks(txtSearch.Text.Trim(), int.Parse(ddlCategory.SelectedValue));
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (currentPage < totalPages) currentPage++;
            LoadBooks(txtSearch.Text.Trim(), int.Parse(ddlCategory.SelectedValue));
        }
    }
}
