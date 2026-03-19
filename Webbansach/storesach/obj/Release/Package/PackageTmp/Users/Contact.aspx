<%@ Page Title="Liên hệ" Language="C#" MasterPageFile="~/MasterPages/SiteUser.master"
    AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="storesach.Users.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center mb-5">
        <h2>📩 Liên hệ với chúng tôi</h2>
        <p class="lead">Bạn có góp ý hay câu hỏi? Hãy gửi cho chúng tôi nhé.</p>
    </div>
    <div class="row">
        <div class="col-md-6">
            <p><strong>Địa chỉ:</strong> 123 Đường ABC, Quận 1, TP.HCM</p>
            <p><strong>Hotline:</strong> 0123 456 789</p>
            <p><strong>Email:</strong> support@storesach.com</p>
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-2" Placeholder="Họ tên"></asp:TextBox>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" Placeholder="Email"></asp:TextBox>
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control mb-2" Rows="4" Placeholder="Nội dung"></asp:TextBox>
            <asp:Button ID="btnSend" runat="server" Text="Gửi" CssClass="btn btn-primary" OnClick="btnSend_Click" />
            <asp:Label ID="lblContact" runat="server" CssClass="d-block mt-2 text-success"></asp:Label>
        </div>
    </div>
</asp:Content>
