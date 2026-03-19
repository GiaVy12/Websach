<%@ Page Title="Quản lý sách" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.master"
    AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="storesach.Admin.Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>📚 Quản lý Sách</h2>

    <!-- Form thêm/sửa -->
    <div class="card p-3 mb-4 shadow-sm" style="max-width:600px;">
        <asp:HiddenField ID="hfBookId" runat="server" />

        <div class="mb-3">
            <label>Tên sách</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Tác giả</label>
            <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Giá</label>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Ảnh (URL)</label>
            <asp:TextBox ID="txtImage" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label>Thể loại</label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnSave_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-2 text-success"></asp:Label>
    </div>

    <!-- Danh sách sách -->
    <!-- Danh sách sách -->
<asp:GridView ID="gvBooks" runat="server" CssClass="table table-bordered"
    AutoGenerateColumns="False" DataKeyNames="BookId"
    OnRowCommand="gvBooks_RowCommand">
    <Columns>
        <asp:BoundField DataField="BookId" HeaderText="ID" />
        <asp:BoundField DataField="Title" HeaderText="Tên sách" />
        <asp:BoundField DataField="Author" HeaderText="Tác giả" />
        <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} VNĐ" />
        <asp:BoundField DataField="CategoryName" HeaderText="Thể loại" />

        <asp:ButtonField CommandName="EditBook" Text="✏️ Sửa" ButtonType="Button" />
        <asp:ButtonField CommandName="DeleteBook" Text="🗑️ Xóa" ButtonType="Button" />
    </Columns>
</asp:GridView>
</asp:Content>
