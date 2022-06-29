<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="WebApp.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    
    <div class="container p-4">
        <%--<fieldset>--%>
            <%--<legend>Settings</legend>--%>

            <div class="mb-3">
                <asp:Label ID="lblTheme" class="form-label" runat="server" Text="Theme:"></asp:Label>
                <asp:DropDownList id="ddlTheme" class="form-select" runat="server" autopostback="true" OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged">
                    <asp:ListItem selected="true" Value="0">- Select -</asp:ListItem>
                    <asp:ListItem Value="WhiteTheme">White</asp:ListItem>
                    <asp:ListItem Value="DarkTheme">Dark</asp:ListItem>
                </asp:DropDownList>
            </div>

           <%-- <div class="mb-3">
                <asp:Label ID="lblLang" class="form-label " runat="server" Text="Language:"  meta:resourcekey="lblLang"></asp:Label>
                <asp:DropDownList id="ddlLang" class="form-select" runat="server" autopostback="true" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                    <asp:ListItem selected="true" Value="0">- Select -</asp:ListItem>
                    <asp:ListItem Value="en" meta:resourcekey="liEng">English</asp:ListItem>
                    <asp:ListItem Value="hr" meta:resourcekey="liCro">Croatian</asp:ListItem>
                </asp:DropDownList>
            </div>--%>
        <%--</fieldset>--%>
    </div>

</asp:Content>
