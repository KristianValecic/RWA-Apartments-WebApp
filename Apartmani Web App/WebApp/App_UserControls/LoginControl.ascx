<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="WebApp.App_UserControls.LoginControl" %>
 
<!-- PANEL PORUKA -->
    <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
        <div class='alert alert-danger' role='alert'>
              Provjerite upisane podatke.
        </div>
    </asp:Panel>
    <!-- // -->

    <asp:Panel ID="PanelForma" runat="server" Visible="True">
        <!-- FORM -->
        
         <fieldset class="container p-4">
             <legend>Login</legend>
              <div class="mb-3">
                <asp:Label ID="lblUsername" class="form-label" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red">* Niste upisali korisničko ime</asp:RequiredFieldValidator>
              </div>
              <div class="mb-3">
                <asp:Label ID="lblPassword" class="form-label" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red">* Niste upisali lozinku</asp:RequiredFieldValidator>
              </div>
             <asp:Button ID="btnLogin" class="btn btn-primary mb-3" runat="server" Text="Submit" OnClick="btnLogin_Click" />
          </fieldset>
    </asp:Panel>
        <!-- // -->