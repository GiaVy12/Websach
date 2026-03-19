<%@ Page Title="Đăng ký" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="storesach.Users.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5">
            <h3 class="mb-3 text-center">📝 Đăng ký</h3>
            <div class="card shadow-sm">
                <div class="card-body">
                    <div class="mb-3">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Tên đăng nhập"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Mật khẩu"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Họ và tên"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-success w-100 mb-2" Text="Đăng ký" OnClick="btnRegister_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="mt-2 d-block text-danger"></asp:Label>

                    <hr />
                    <p class="text-center">
                        Đã có tài khoản?
                        <a href="Login.aspx" class="btn btn-outline-primary btn-sm">Đăng nhập</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
