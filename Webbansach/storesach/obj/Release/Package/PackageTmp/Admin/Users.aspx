<%@ Page Title="Quản lý người dùng" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.master"
    AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="storesach.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>👤 Quản lý Người dùng</h2>

   <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped"
    AutoGenerateColumns="False" DataKeyNames="UserId" 
    OnRowDeleting="gvUsers_RowDeleting" OnRowDataBound="gvUsers_RowDataBound">
    <Columns>
        <asp:BoundField DataField="UserId" HeaderText="ID" />
        <asp:BoundField DataField="Username" HeaderText="Tên đăng nhập" />
        <asp:BoundField DataField="FullName" HeaderText="Họ tên" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Phone" HeaderText="Điện thoại" />
        <asp:BoundField DataField="Address" HeaderText="Địa chỉ" />
        <asp:BoundField DataField="Role" HeaderText="Vai trò" />
        <asp:CommandField ShowDeleteButton="True" DeleteText="Xóa" />
    </Columns>
</asp:GridView>

</asp:Content>
