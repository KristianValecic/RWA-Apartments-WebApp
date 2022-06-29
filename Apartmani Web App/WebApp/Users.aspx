<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WebApp.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container p-4">
        <h1 calss="h1">Users</h1>
        <h6 class="h6">To delete a user, you have to enter edit...</h6>
            <div class="row mb-3">
                <div class="col-10"></div>
                <asp:Button ID="btnAdd" CssClass="col btn btn-primary" OnClick="btnAdd_Click" runat="server" Text="Add Uaser" />
            </div>
        <div class="row">
            <div >
                <div class="form-group">
                    <div id="listTags" class="row" runat="server">
                        <div class="">
                            <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block w-100"></asp:Label>
                            <fieldset class="p-4">
                                <asp:Repeater ID="rptTags" runat="server">
                                    <HeaderTemplate>
                                        <table id="myTableUsers" class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">#</th>
                                                    <th scope="col">Username</th>
                                                    <th scope="col">Email</th>
                                                    <th scope="col">City</th>
                                                    <th scope="col"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <th scope="row"><%# Eval(nameof(Lib.Models.User.ID)) %></th>
                                            <td><%# Eval(nameof(Lib.Models.User.UserName)) %></td>
                                            <td><%# Eval(nameof(Lib.Models.User.Email)) %></td>
                                            <td><%# Eval(nameof(Lib.Models.User.City)) %></td>
                                            <td>
                                                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary"
                                                    CommandArgument="<%# Eval(nameof(Lib.Models.User.ID)) %>" runat="server">
                                                        Edit
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </fieldset>
                        </div>
                        <%--<div class="col-md-2">
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Add" OnClick="Button1_Click" />
                            </div>--%>
                    </div>
                </div>
                <div id="editUser" class="col-sm-6 p-1" runat="server">
                    <fieldset class="p-4">
                        <legend>Edit User</legend>
                        <asp:Label ID="lblResult" runat="server" CssClass="alert alert-danger d-block w-100"></asp:Label>
                        <div class="mb-3">
                            <asp:Label ID="lblUsername" meta:resourcekey="lblUsername" class="form-label" runat="server" Text="Username"></asp:Label>
                            <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red">
                             * Owner is a required field</asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblEmail" for="txtEmail" class="form-label" runat="server" Text="Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red">
                             * Owner is a required field</asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblCity" for="txtCity" class="form-label" runat="server" Text="City"></asp:Label>
                            <asp:TextBox ID="txtCity" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity" Display="Dynamic" ForeColor="Red">
                             * Owner is a required field</asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblAddress" for="txtAddress" class="form-label" runat="server" Text="Address"></asp:Label>
                            <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ForeColor="Red">
                             * Owner is a required field</asp:RequiredFieldValidator>
                        </div>
                        <div class="row ">
                            <div class="justify-content-between col-md-10">
                                <asp:Button ID="updateUser" class=" btn btn-primary" runat="server" Text="Update" OnClick="updateUser_Click" />
                                <asp:Button ID="btnBack" class=" btn btn-secondary " runat="server" Text="Back" OnClick="btnBack_Click" />
                            </div>
                            <asp:LinkButton ID="btnDelete" class="col-md-2 btn btn-danger " runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="validate()" />
                        </div>
                    </fieldset>
                </div>
            </div>

        </div>
</asp:Content>
