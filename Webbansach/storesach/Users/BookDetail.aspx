<%@ Page Title="Chi tiết sách" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="storesach.Users.BookDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <asp:Image ID="imgBook" runat="server" CssClass="img-fluid rounded shadow-sm" />
        </div>
        <div class="col-md-8">
            <h2><asp:Label ID="lblTitle" runat="server" /></h2>
            <p><strong>Tác giả:</strong> <asp:Label ID="lblAuthor" runat="server" /></p>
            <p><strong>Giá:</strong> <asp:Label ID="lblPrice" runat="server" CssClass="text-danger fw-bold" /></p>
            <p><strong>Tồn kho:</strong> <asp:Label ID="lblStock" runat="server" /></p>
            <asp:Button ID="btnAddToCart" runat="server" CssClass="btn btn-success mt-3" Text="🛒 Thêm vào giỏ" OnClick="btnAddToCart_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-2 text-success"></asp:Label>
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-12">
            <div class="card">
                <div class="card-header bg-light">
                    📖 Đọc trước
                </div>
                <div class="card-body">
                    <asp:Literal ID="litPreview" runat="server" Mode="Encode"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
