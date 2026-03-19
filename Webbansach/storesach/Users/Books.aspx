<%@ Page Title="Tất cả sách" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="storesach.Users.Books" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">📚 Tất cả sách</h2>

    <!-- Thanh tìm kiếm -->
    <div class="input-group mb-4">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Nhập tên sách hoặc tác giả..."></asp:TextBox>
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="btnSearch_Click">🔍 Tìm kiếm</button>
    </div>

    <!-- Danh mục thể loại -->
    <div class="mb-3">
        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
            CssClass="form-select w-auto d-inline-block">
        </asp:DropDownList>
    </div>

    <!-- Hiển thị sách -->
    <div class="row">
        <asp:Repeater ID="rptAllBooks" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src='<%# Eval("Image") %>' class="card-img-top" style="height:320px; object-fit:cover;" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Title") %></h5>
                            <p class="text-muted">Tác giả: <%# Eval("Author") %></p>
                            <p class="text-danger fw-bold"><%# Eval("Price", "{0:N0}") %> VNĐ</p>
                            <a href='BookDetail.aspx?id=<%# Eval("BookId") %>' class="btn btn-sm btn-outline-primary">Xem chi tiết</a>
                            <a href='Cart.aspx?add=<%# Eval("BookId") %>' class="btn btn-sm btn-success">Thêm giỏ</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="d-flex justify-content-center mt-4">
        <asp:Button ID="btnPrev" runat="server" Text="⏮ Trước" CssClass="btn btn-outline-secondary me-2"
            OnClick="btnPrev_Click" />
        <asp:Label ID="lblPage" runat="server" CssClass="align-self-center fw-bold"></asp:Label>
        <asp:Button ID="btnNext" runat="server" Text="Tiếp ⏭" CssClass="btn btn-outline-secondary ms-2"
            OnClick="btnNext_Click" />
    </div>
</asp:Content>
