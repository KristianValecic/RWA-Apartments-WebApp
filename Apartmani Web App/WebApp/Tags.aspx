<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="WebApp.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row container p-4 ">
        <div id="listTags" class="row" runat="server">
            <div class="col-md-8">
                <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block w-100"></asp:Label>
                <fieldset class="p-4">
                    <asp:Repeater ID="rptTags" runat="server">
                        <HeaderTemplate>
                            <table id="myTableTags" class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">Type</th>
                                        <th scope="col">Recurrence</th>
                                        <%--<th scope="col">Created At</th>--%>
                                        <th scope="col"></th>

                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval(nameof(Lib.Models.Tag.ID)) %></th>
                                <td><%# Eval(nameof(Lib.Models.Tag.Name)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Tag.TypeOfTag)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Tag.RecurrenceCounter)) %></td>
                                <%--<td><%# Eval(nameof(Lib.Models.Tag.CreatedAt)) %></td>--%>

                                <td>
                                    <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" CommandArgument="<%# Eval(nameof(Lib.Models.Tag.ID)) %>" runat="server"
                                        OnClientClick="validate()">
                                        <%# (int)Eval(nameof(Lib.Models.Tag.RecurrenceCounter)) == 0 ? "Delete" : ""%>
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
            <div class="col-md-2">
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Add" OnClick="Button1_Click" />
            </div>
            <%--<div class="col-md-4">
                <asp:GridView ID="gvAparments" AutoGenerateColumns="false" CssClass="table" runat="server">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Apartment name" />
                    </Columns>
                </asp:GridView>
            </div>--%>
        </div>
        <div id="addTags" class="col-md-8" runat="server">

            <fieldset class="p-4">
                <legend>Edit User</legend>
                <div class="mb-3">
                    <asp:Label ID="lblName" for="txtName" class="form-label" runat="server" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server"
                        ControlToValidate="txtName" Display="Dynamic" ForeColor="Red">* Name is a required field</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblType" for="txtType" class="form-label" runat="server" Text="Type"></asp:Label>
                    <%--<asp:TextBox ID="txtType" class="form-control" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlTagType" class="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="row">
                    <div class="col-md-2 d-flex">
                        <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="Add" OnClick="btnAdd_Click" />
                    </div>
                    <div class="col-md-1 d-flex">
                        <asp:Button ID="btnBack" class="btn btn-secondary" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                    </div>
                </div>
            </fieldset>
        </div>

        <%-- CONFIRAMTION PANEL FOR DELETE --%>
        <%--<asp:Panel ID="panelConfirm" runat="server" align="center" class="bg-opacity-10 bg-dark position-absolute h-100">
            <fieldset Class="w-25 p-4 bg-white mt-5">
                <asp:Label ID="Label1"  runat="server" Text="Are you shure you want to delete the selected tag?"></asp:Label>
            <div align="center" class="mt-1">
                <asp:Button ID="btnConfirm" runat="server" class="btn btn-primary" Text="Yes" OnClick="btnConfirm_Click" />
                <asp:Button ID="btnCancel" runat="server" class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
            </diva>
            </fieldset>
            </asp:Panel> --%>
    </div>

    <%--<script>
        function validate() {

            if (confirm("Press a button!")) {

            } else {
                __doPostBack();
            }
        }
    </script>--%>

</asp:Content>
