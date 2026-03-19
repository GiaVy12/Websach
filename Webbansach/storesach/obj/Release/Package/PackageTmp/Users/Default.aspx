<%@ Page Title="Trang chủ" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="storesach.Users.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Banner -->
    <div class="p-5 mb-4 bg-light rounded-3 shadow-sm">
        <div class="container-fluid py-5">
            <h1 class="display-5 fw-bold">Chào mừng đến với Kho Sách 📚</h1>
            <p class="col-md-8 fs-4">Khám phá kho tàng sách phong phú – từ văn học, kỹ năng sống đến kinh doanh, công nghệ.</p>
            <a href="#banchay" class="btn btn-primary btn-lg">Xem sách bán chạy</a>
        </div>
    </div>

    <!-- Sách mới nhất -->
    <h2 class="mb-4">📖 Sách mới</h2>
    <div class="row mb-5">
        <asp:Repeater ID="rptNewBooks" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src='<%# Eval("Image") %>' class="card-img-top" style="height:320px; object-fit:cover;" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Title") %></h5>
                            <p class="text-danger fw-bold"><%# Eval("Price", "{0:N0}") %> VNĐ</p>
                            <a href='../Users/BookDetail.aspx?id=<%# Eval("BookId") %>' class="btn btn-sm btn-outline-primary">Đọc thêm</a>
                            <a href='../Users/Cart.aspx?add=<%# Eval("BookId") %>' class="btn btn-sm btn-success">Thêm giỏ</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Sách bán chạy -->
    <h3 class="mt-5 mb-3">🔥 Sách bán chạy</h3>
<div class="row" id="bestSeller">
    <asp:Repeater ID="rptBestSeller" runat="server">
        <ItemTemplate>
            <div class="col-md-3 mb-4">
                <div class="card h-100 shadow-sm">
                    <img src='<%# Eval("Image") %>' class="card-img-top" style="height:420px; object-fit:cover;" />
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("Title") %></h5>
                        <p class="card-text text-muted">Tác giả: <%# Eval("Author") %></p>
                        <p class="fw-bold text-danger"><%# String.Format("{0:N0} VND", Eval("Price")) %></p>
                        <a href='<%# "/Users/BookDetail.aspx?id=" + Eval("BookId") %>' class="btn btn-primary btn-sm">Đọc thêm</a>
                        <a href='../Users/Cart.aspx?add=<%# Eval("BookId") %>' class="btn btn-sm btn-success">Thêm giỏ</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
</asp:Content>
