<%@ Page Title="Quản lý đơn hàng" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.master"
    AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="storesach.Admin.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>🛒 Quản lý Đơn hàng</h2>

    <!-- Bộ lọc theo ngày -->
    <div class="mb-3">
        <asp:Label ID="lblFrom" runat="server" Text="Từ ngày: "></asp:Label>
        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control d-inline w-auto" TextMode="Date"></asp:TextBox>
        <asp:Label ID="lblTo" runat="server" Text="Đến ngày: "></asp:Label>
        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control d-inline w-auto" TextMode="Date"></asp:TextBox>
        <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary" Text="Lọc" OnClick="btnFilter_Click" />
        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnReset_Click" />
    </div>

    <!-- Danh sách đơn hàng -->
    <asp:GridView ID="gvOrders" runat="server" CssClass="table table-bordered"
    AutoGenerateColumns="False" DataKeyNames="OrderId"
    OnSelectedIndexChanged="gvOrders_SelectedIndexChanged"
    OnRowDeleting="gvOrders_RowDeleting">
    <Columns>
        <asp:BoundField DataField="OrderId" HeaderText="Mã đơn" />
        <asp:BoundField DataField="UserId" HeaderText="Mã KH" />
        <asp:BoundField DataField="OrderDate" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Total" HeaderText="Tổng tiền" DataFormatString="{0:N0} VNĐ" />
        <asp:CommandField ShowSelectButton="True" SelectText="👁 Xem" />
        <asp:CommandField ShowDeleteButton="True" DeleteText="❌ Xóa" />
    </Columns>
</asp:GridView>

<asp:Label ID="lblMessage" runat="server" CssClass="text-success d-block mt-2"></asp:Label>


    <!-- Thông tin khách hàng -->
<h3 class="mt-4">Thông tin Khách hàng</h3>
<asp:DetailsView ID="dvCustomer" runat="server" CssClass="table table-bordered"
    AutoGenerateRows="False" GridLines="None">
    <Fields>
        <asp:BoundField DataField="FullName" HeaderText="Họ tên" />
        <asp:BoundField DataField="Username" HeaderText="Tài khoản" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Phone" HeaderText="Điện thoại" />
        <asp:BoundField DataField="Address" HeaderText="Địa chỉ" />
    </Fields>
</asp:DetailsView>


    <!-- Chi tiết đơn hàng -->
    <h3 class="mt-4">Chi tiết đơn hàng</h3>
    <asp:GridView ID="gvOrderDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Tên sách" />
            <asp:BoundField DataField="Quantity" HeaderText="SL" />
            <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} VNĐ" />
            <asp:BoundField DataField="Total" HeaderText="Thành tiền" DataFormatString="{0:N0} VNĐ" />
        </Columns>
    </asp:GridView>
</asp:Content>
