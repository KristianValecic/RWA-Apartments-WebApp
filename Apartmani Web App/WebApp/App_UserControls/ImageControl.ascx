<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageControl.ascx.cs" Inherits="WebApp.App_UserControls.ImageControl" %>

<input type="hidden" id="ImgID" runat="server" value="<%# Eval(nameof(Lib.Models.Picture.ID)) %>">

<div class="m-4">
    <%--style="padding: 5px 10px;"--%>
    <asp:Label ID="ImgName" runat="server" Text="<%# Eval(nameof(Lib.Models.Picture.Name)) %>"></asp:Label>

    <div class="m-1">
        <asp:Image class="imgApart" runat="server" ImageUrl="<%# Eval(nameof(Lib.Models.Picture.Base64ForHtml)) %>"
            Style="height: 250px;" />
    </div>

    <asp:Button ID="deleteImg" runat="server" Text="Delete" class="btn btn-danger" OnClick="deleteImg_Click" />

    <asp:Label ID="lblRep" runat="server" Text="<%# Eval(nameof(Lib.Models.Picture.IsRepresentative))%>"></asp:Label>

</div>
