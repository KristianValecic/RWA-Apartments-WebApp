<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="AddApartment.aspx.cs" Inherits="WebApp.AddApartment" %>

<%@ Register Src="~/App_UserControls/ImageControl.ascx" TagPrefix="uc1" TagName="ImageControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container">
        <div class="row p-2">
            <h1 id="headerName" class=" h1 mb-3" runat="server">Add Apartment</h1>
            <div id="ApartmentExistsValidation" class="alert alert-danger" visible="false" runat="server">
                <p>This apartment already exists</p>
            </div>
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label ID="lblName" class="form-label" runat="server" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red">
                        * Name is a required field</asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblOwner" class="form-label" runat="server" Text="Owner"></asp:Label>
                    <asp:TextBox ID="txtOwner" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOwner" Display="Dynamic" ForeColor="Red">
                        * Owner is a required field</asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col mb-3">
                        <asp:Label ID="lblStatus" class="form-label" runat="server" Text="Status"></asp:Label>
                        <%--<asp:TextBox ID="txtStatus" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlStatuses" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col mb-3">
                        <asp:Label ID="lblPrice" class="form-label" runat="server" Text="Price"></asp:Label>
                        <asp:TextBox ID="txtPrice" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPrice" Display="Dynamic" ForeColor="Red">
                            * Price is a required field</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 mb-3">
                        <asp:Label ID="lblCity" class="form-label" runat="server" Text="City"></asp:Label>
                        <%--<asp:TextBox ID="txtCity" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-6 mb-3">
                        <asp:Label ID="lblAddress" class="form-label" runat="server" Text="Address"></asp:Label>
                        <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ForeColor="Red">
                            * Adress is a required field</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col mb-3">
                        <asp:Label ID="lblMaxChildren" class="form-label" runat="server" Text="Max children"></asp:Label>
                        <asp:TextBox ID="txtMaxChildren" class="form-control" runat="server" Type="number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMaxChildren" Display="Dynamic" ForeColor="Red">
                            * Max Children is a required field</asp:RequiredFieldValidator>
                    </div>
                    <div class="col mb-3">
                        <asp:Label ID="lblMaxAdults" class="form-label" runat="server" Text="Max adults"></asp:Label>
                        <asp:TextBox ID="txtMaxAdults" class="form-control" runat="server" Type="number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMaxAdults" Display="Dynamic" ForeColor="Red">
                            * Max Adults is a required field</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col mb-3">
                        <asp:Label ID="lblRooms" class="form-label" runat="server" Text="Rooms"></asp:Label>
                        <asp:TextBox ID="txtRooms" class="form-control" runat="server" Type="number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRooms" Display="Dynamic" ForeColor="Red">
                            * Rooms is a required field</asp:RequiredFieldValidator>
                    </div>
                    <div class="col mb-3">
                        <asp:Label ID="lblBeachDistance" class="form-label" runat="server" Text="Beach distance"></asp:Label>
                        <asp:TextBox ID="txtBeachDistance" class="form-control" runat="server" Type="number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBeachDistance" Display="Dynamic" ForeColor="Red">
                            * Beach distance is a required field</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row p-2">
                    <div class="col-8"></div>
                    <div class="col-2">
                        <asp:Button ID="btnBack" class="btn btn-secondary" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                    </div>
                    <asp:Button ID="btnAddApart" class="col-2 btn btn-primary" runat="server" Text="Add" OnClick="btnAddApart_Click" />
                </div>
            </div>
            <div class="col">

                <div class="p-2">
                    <%--<asp:Label ID="Label1" runat="server" Text="Tags"></asp:Label>--%>
                    <fieldset>
                        <legend>Tags</legend>
                        <asp:ListBox ID="lsTags" CssClass="form-control mb-3" runat="server"
                            OnSelectedIndexChanged="lsTags_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                        <div class="row mb-3">
                            <div class="col-7">
                                <asp:DropDownList ID="ddlAllTags" CssClass=" form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Button ID="btnAddTag" CssClass=" btn btn-primary" OnClick="btnAddTag_Click" runat="server"
                                    Text="Add tag" CausesValidation="false" />
                            </div>
                            <div class="col-2">
                                <%--Ovo se pokaze samo kada je selektan tag iz lise--%>
                                <asp:Button ID="btnDeleteTag" CssClass=" btn btn-danger" runat="server" Text="Delete tag"
                                    Visible="false" OnClick="btnDeleteTag_Click" CausesValidation="false" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblTagValidation" runat="server" Visible="false" ForeColor="Red">Apartment already has that tag.</asp:Label>
                        </div>
                    </fieldset>
                    <div class="row">
                        <div id="pnlReserved" class="container p-3" runat="server">
                            <fieldset>
                                <legend>Reservations</legend>
                                <%--<asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                                <asp:TextBox ID="txtUsername" CssClass="form-control mb-3" runat="server"></asp:TextBox>

                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>--%>
                                <asp:ListBox ID="lsReservations" CssClass="form-control mb-3" runat="server"
                                    OnSelectedIndexChanged="lsReservations_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                <div class="row mb-1">
                                    <div class="col-1">
                                        <asp:Label ID="lblReservationName" runat="server" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7">
                                        <asp:TextBox ID="txtReservationName" CssClass="form-control " runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtReservationName"
                                            ForeColor="red" ValidationGroup="reservationValidation">
                                            Required field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button ID="btnAddReservation" CssClass=" btn btn-primary" OnClick="btnAddReservation_Click"
                                            runat="server" Text="Add reservation" ValidationGroup="reservationValidation" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-1">
                                        <asp:Label ID="Label3" runat="server" Text="From"></asp:Label>
                                    </div>
                                    <div class="col-7">
                                        <asp:TextBox ID="txtFromDate" CssClass="form-control " runat="server" type="date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFromDate"
                                            ForeColor="red" ValidationGroup="reservationValidation">Required field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-3">
                                        <asp:Button ID="btnDeleteReservation" CssClass=" btn btn-danger" runat="server" Text="Delete reservation"
                                            Visible="false" OnClick="btnDeleteReservation_Click" CausesValidation="false" />
                                    </div>
                                    <div class="row">
                                        <div class="col-1">
                                            <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                                        </div>
                                        <div class="col-7">

                                            <asp:TextBox ID="txtToDate" CssClass="form-control " runat="server" type="date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtToDate"
                                                ForeColor="red" ValidationGroup="reservationValidation">Required field</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblReservaionValidation" runat="server" Visible="false" ForeColor="Red">Reservation already exists.</asp:Label>
                                </div>
                        </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-2 mt-3 ">
                    <asp:Button ID="btnChangeImg" class="btn btn-secondary" runat="server" Text="Change" />
                </div>
            </div>
            <div class="w-50">
                <div>
                    <asp:Label ID="lblImgName" class="form-label" runat="server" Text="Picture name"></asp:Label>
                    <asp:TextBox ID="txtImgName" CssClass="form-control " runat="server" ValidationGroup="ImgValidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtImgName"
                        ForeColor="red" ValidationGroup="ImgValidation">
                        Name is sa required field</asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label ID="lblIsRepresentative" class="form-label" runat="server" Text="Is representative"></asp:Label>
                    <asp:CheckBox ID="cbIsRepresentative" class="form-check-labe" runat="server" />
                </div>
                <div class="mb-4 mt-3">
                    <asp:FileUpload ID="FileUpload" runat="server" />
                    <asp:Button ID="btnAddImg" class="btn btn-secondary" runat="server" Text="Add image" type="file"
                        ValidationGroup="ImgValidation" OnClick="btnAddImg_Click" />

                    <div class="row">
                        <asp:Label ID="lblSelectedFileMessage" runat="server" ForeColor="Red" Visible="false">No file selected</asp:Label>
                        <asp:Label ID="lblSelectedFileError" runat="server" ForeColor="Red" Visible="false">There was an error with the file upload</asp:Label>
                    </div>
                </div>
            </div>
            <div id="divImgs" class="d-flex flex-wrap" runat="server">
                <asp:Repeater ID="rptrImages" runat="server">
                    <ItemTemplate>
                        <uc1:ImageControl runat="server" ID="ImageControl" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
