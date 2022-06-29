<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Zadatak01.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <%--<form id="form1" runat="server">--%>
        <div class="container p-4"><fieldset>
            <legend>User: <b runat="server" id="bUsername"></b></legend>

            <label>Email:</label>
            <p id="pEmail" runat="server"></p>
            <%--<asp:Label ID="pFname" runat="server" class="col-form-label"></asp:Label>--%>

            <label>City:</label>
            <p id="pCity" runat="server"></p>
            <%--<asp:Label ID="pUsername" runat="server" class="col-form-label"></asp:Label>--%>

            <label>Address:</label>
            <p id="pAddress" runat="server"></p>
            <%--<asp:Label ID="pLname" runat="server" class="col-form-label"></asp:Label>--%>

            <%--<asp:Button ID="btnLogout" class="btn btn-primary" runat="server" Text="Logout" OnClick="btnLogout_Click" />--%>
        </fieldset></div>
    <%--</form>--%>
</asp:Content>
