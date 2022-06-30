<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="WebApp.Apartments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class=" container p-4">
        <h1 class="h1 mb-3">Apartments</h1>
        <div class="row mb-3" runat="server">
            <div class="col-2">
                <asp:Label ID="Label1" runat="server" Text="City:"></asp:Label>
                <asp:DropDownList ID="ddlFilterCity" CssClass="form-select" runat="server" 
                    OnSelectedIndexChanged="ddlFilterCity_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-2">
                <asp:Label ID="Label2" runat="server" Text="Status:"></asp:Label>
                <asp:DropDownList ID="ddlFilterStatus" CssClass="form-select" runat="server" 
                    OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div id="listTags" class="row" runat="server">
            <div class="col">
                <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block w-100"></asp:Label>
                <fieldset class="p-4">
                    <asp:Repeater ID="rptTags" runat="server">
                        <HeaderTemplate>
                            <table id="myTableAparts" class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">City</th>
                                        <th scope="col">Adults</th>
                                        <th scope="col">Children</th>
                                        <th scope="col">Rooms</th>
                                        <th scope="col">Places</th>
                                        <th scope="col">Status</th>
                                        <th scope="col">Price</th>
                                        <th scope="col"></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval(nameof(Lib.Models.Apartment.ID)) %></th>
                                <td><%# Eval(nameof(Lib.Models.Apartment.Name)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.City)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.MaxAdults)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.MaxChildren)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.TotalRooms)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.totalPlacesCount)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.Status)) %></td>
                                <td><%# Eval(nameof(Lib.Models.Apartment.Price)) %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary"
                                        CommandArgument="<%# Eval(nameof(Lib.Models.Apartment.ID)) %>" runat="server">
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
            <div class="col-1">
                <asp:Button ID="btnAdd" CssClass="btn btn-primary" OnClick="btnAdd_Click" runat="server" Text="Add Apartment" />
            </div>
        </div>
    </div>
</asp:Content>
