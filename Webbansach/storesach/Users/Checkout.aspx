<%@ Page Title="Thanh toán" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="storesach.Users.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <h3 class="mb-3">🛒 Thanh toán</h3>
            <div class="card shadow-sm">
                <div class="card-body">
                    <div class="mb-3">
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Họ tên người nhận"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="Địa chỉ giao hàng"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Số điện thoại"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnOrder" runat="server" CssClass="btn btn-success w-100" Text="Xác nhận đặt hàng" OnClick="btnOrder_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3 d-block"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
