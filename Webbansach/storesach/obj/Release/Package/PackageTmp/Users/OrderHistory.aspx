<%@ Page Title="Lịch sử đơn hàng" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="storesach.Users.OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">📜 Lịch sử đơn hàng</h2>
    
    <asp:GridView ID="gvOrders" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" DataKeyNames="OrderId"
        OnSelectedIndexChanged="gvOrders_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="OrderId" HeaderText="Mã đơn" />
            <asp:BoundField DataField="OrderDate" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="Total" HeaderText="Tổng tiền" DataFormatString="{0:N0} VNĐ" />
            <asp:CommandField ShowSelectButton="True" SelectText="Xem chi tiết" />
        </Columns>
    </asp:GridView>

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
