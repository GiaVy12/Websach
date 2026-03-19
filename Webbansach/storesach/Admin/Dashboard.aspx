<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="storesach.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-3">📊 Thống kê hệ thống</h2>
    <div class="row">
        <div class="col-md-3">
            <div class="card text-white bg-primary mb-3">
                <div class="card-body">
                    <h5 class="card-title">Tổng số sách</h5>
                    <h3><asp:Label ID="lblBooks" runat="server" /></h3>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-white bg-success mb-3">
                <div class="card-body">
                    <h5 class="card-title">Tổng số đơn hàng</h5>
                    <h3><asp:Label ID="lblOrders" runat="server" /></h3>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-white bg-warning mb-3">
                <div class="card-body">
                    <h5 class="card-title">Khách hàng</h5>
                    <h3><asp:Label ID="lblUsers" runat="server" /></h3>
                </div>
            </div>
        </div>
        <div class="col-md-3">
    <div class="card text-white bg-danger mb-3">
        <div class="card-body">
            <h5 class="card-title">Tổng doanh thu</h5>
            <h3><asp:Label ID="lblRevenue" runat="server" /></h3>
        </div>
    </div>
</div>

    </div>
</asp:Content>
