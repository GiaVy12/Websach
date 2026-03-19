<%@ Page Title="Giỏ hàng" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="storesach.Users.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">🛒 Giỏ hàng của bạn</h2>

    <!-- Bảng giỏ hàng -->
    <asp:GridView ID="gvCart" runat="server" CssClass="table table-bordered text-center align-middle"
        AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand" ShowHeaderWhenEmpty="true"
        EmptyDataText="Giỏ hàng trống. Hãy thêm sản phẩm!">
        <Columns>
            <asp:BoundField DataField="BookId" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Tên sách" />
            <asp:BoundField DataField="Quantity" HeaderText="Số lượng" />
            <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} VNĐ" />
            <asp:BoundField DataField="Total" HeaderText="Thành tiền" DataFormatString="{0:N0} VNĐ" />
            <asp:ButtonField CommandName="Remove" Text="❌ Xóa" ButtonType="Button" />
        </Columns>
    </asp:GridView>

    <div class="d-flex justify-content-between mt-3">
        <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold fs-5 text-danger"></asp:Label>
        <asp:Button ID="btnCheckout" runat="server" Text="💳 Thanh toán"
            CssClass="btn btn-success px-4" OnClick="btnCheckout_Click" />
    </div>

    <!-- Form thanh toán, ẩn mặc định -->
    <asp:Panel ID="pnlCheckout" runat="server" CssClass="card mt-4 shadow-sm" Visible="false">
        <div class="card-body">
            <h4 class="mb-3">💳 Thông tin thanh toán</h4>

            <div class="mb-3">
                <label class="form-label">Họ và tên</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Số điện thoại</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Địa chỉ giao hàng</label>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
            </div>

            <div class="mb-3">
                <label class="form-label">Phương thức thanh toán</label>
                <asp:RadioButtonList ID="rblPayment" runat="server">
                    <asp:ListItem Value="COD" Selected="True">Thanh toán khi nhận hàng (COD)</asp:ListItem>
                    <asp:ListItem Value="Bank">Chuyển khoản ngân hàng</asp:ListItem>
                    <asp:ListItem Value="Momo">Ví điện tử Momo</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <asp:Button ID="btnConfirm" runat="server" Text="Xác nhận đặt hàng"
                CssClass="btn btn-primary" OnClick="btnConfirm_Click" />
        </div>
    </asp:Panel>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-success d-block mt-3"></asp:Label>
</asp:Content>
