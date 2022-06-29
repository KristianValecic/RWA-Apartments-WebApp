<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Zadatak01.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <!-- FORM -->
    <asp:Panel ID="PanelForma" runat="server" Visible="True">
        <%--<form id="formRegistration" runat="server" class="container p-4" method="post" action="Registration.aspx">--%>
        <fieldset class="container p-4">
            <legend>Add user</legend>
            <asp:Label ID="lblUserAlreadyExists" runat="server" Text="Label" ForeColor="Red" Visible="false" >User already exists</asp:Label>
            <div class="mb-3">
                <asp:Label ID="lblFname" for="txtFname" class="form-label" runat="server" Text="First name"></asp:Label>
                <asp:TextBox ID="txtFname" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFname" Display="Dynamic" ForeColor="Red">* First name is a required field</asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator1"
                    ClientValidationFunction="Username_Validation"
                    runat="server"
                    ControlToValidate="txtFname"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="Username_ServerValidate">* Forbidden first name</asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblLname" for="txtLname" class="form-label" runat="server" Text="Last name"></asp:Label>
                <asp:TextBox ID="txtLname" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLname" Display="Dynamic" ForeColor="Red">* Last name is a required field</asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="CustomValidator2"
                    ClientValidationFunction="Username_Validation"
                    runat="server"
                    ControlToValidate="txtLname"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="Username_ServerValidate">* Forbidden last name</asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblEmail" for="txtEmail" class="form-label" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red">* Email is a required field</asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCity" for="txtCity" class="form-label" runat="server" Text="City"></asp:Label>
                <%--<asp:DropDownList ID="ddlCity" class="form-select" runat="server"></asp:DropDownList>--%>
                <asp:TextBox ID="txtCity" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAdress" Display="Dynamic" ForeColor="Red">
                        * City is a required field</asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblAdress" for="txtAdress" class="form-label" runat="server" Text="Adress"></asp:Label>
                <asp:TextBox ID="txtAdress" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAdress" Display="Dynamic" ForeColor="Red">
                        * Adress is a required field</asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblPassword" for="txtPassword" class="form-label" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red">* passwrod is a required field</asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblConfirmPassword" for="txtConfirmPassword" class="form-label" runat="server" Text="ConfirmPassword"></asp:Label>
                <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red">* password is a required field</asp:RequiredFieldValidator>--%>
                <asp:CompareValidator ID="CompareValidator3" runat="server"
                    ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                    Display="Dynamic"
                    ForeColor="Red">* Passwords have to match</asp:CompareValidator>
            </div>

            <div class="row">
                <div class="col-1">
                    <asp:Button ID="btnPosalji" runat="server" class="btn btn-primary" Text="Add" OnClick="btnPosalji_Click" />
                </div>
                <div class="col-1">
                    <asp:Button ID="btnBack" runat="server" class="btn btn-secondary" Text="Back" OnClick="btnBack_Click" CausesValidation="false"/>
                </div>
            </div>

        </fieldset>

        <%--</form>--%>
    </asp:Panel>
    <!-- // -->

    <!-- PANEL PORUKA -->
    <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
        <div class='alert alert-success' role='alert'>
            Registracija je uspješna.
        </div>
    </asp:Panel>
    <!-- // -->

    <!-- JQUERY -->
    <script src="Scripts/jquery-3.6.0.min.js"></script>

    <!-- BOOTSTRAP -->
    <script src="Scripts/bootstrap.min.js"></script>

    <!-- USERNAME VALIDATION -->
    <script type="text/javascript">
        function Username_Validation(sender, args) {
            if (args.Value == "admin") {
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }
    </script>
</asp:Content>
