<%@ Page Title="Đăng nhập" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="storesach.Users.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <h3 class="mb-3 text-center">🔑 Đăng nhập</h3>
            <div class="card shadow-sm">
                <div class="card-body">
                    <div class="mb-3">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Tên đăng nhập"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Mật khẩu"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100 mb-2" Text="Đăng nhập" OnClick="btnLogin_Click" />
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="mt-2 d-block"></asp:Label>

                    <hr />
                    <p class="text-center">
                        Chưa có tài khoản? 
                        <a href="Register.aspx" class="btn btn-outline-success btn-sm">Đăng ký ngay</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
