<%@ Page Title="Đăng nhập Admin" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="storesach.Admin.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <h3 class="mb-3 text-center">🔐 Admin Login</h3>
            <div class="card shadow-sm">
                <div class="card-body">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control mb-3" Placeholder="Tên đăng nhập"></asp:TextBox>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" Placeholder="Mật khẩu"></asp:TextBox>
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-danger w-100" Text="Đăng nhập" OnClick="btnLogin_Click" />
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="mt-2 d-block"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
