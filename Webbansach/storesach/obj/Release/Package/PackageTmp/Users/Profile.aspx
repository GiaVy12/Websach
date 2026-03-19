<%@ Page Title="Thông tin cá nhân" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="storesach.Users.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">👤 Thông tin cá nhân</h2>
    
    <div class="card p-4 shadow-sm" style="max-width:500px;">
        <div class="mb-3">
            <label>Tên đăng nhập</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Họ và tên</label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Số điện thoại</label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Địa chỉ</label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Mật khẩu mới (để trống nếu không đổi)</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-2 text-success"></asp:Label>
    </div>
</asp:Content>
