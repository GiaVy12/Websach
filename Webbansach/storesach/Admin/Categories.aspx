<%@ Page Title="Quản lý thể loại" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.master"
    AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="storesach.Admin.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>📂 Quản lý Thể loại</h2>

    <div class="mb-3">
        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control w-50 d-inline" Placeholder="Tên thể loại mới"></asp:TextBox>
        <asp:Button ID="btnAdd" runat="server" Text="Thêm" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
    </div>

    <asp:GridView ID="gvCategories" runat="server" CssClass="table table-bordered"
    AutoGenerateColumns="False" DataKeyNames="CategoryId" 
    OnRowDeleting="gvCategories_RowDeleting" OnRowDataBound="gvCategories_RowDataBound">
    <Columns>
        <asp:BoundField DataField="CategoryId" HeaderText="ID" />
        <asp:BoundField DataField="Name" HeaderText="Tên thể loại" />
        <asp:CommandField ShowDeleteButton="True" DeleteText="Xóa" />
    </Columns>
</asp:GridView>

<asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>

</asp:Content>
