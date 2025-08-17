<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Rooms.aspx.cs" Inherits="Rooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Room Management</h1>
                </div>
            </div>
        </div>
    </div>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Add New Room</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label>Room Number:</label>
                                <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Room Type:</label>
                                <asp:DropDownList ID="cmbRoomType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- Select Room Type --" Value="" />
                                    <asp:ListItem Text="ICU" Value="ICU" />
                                    <asp:ListItem Text="General" Value="General" />
                                    <asp:ListItem Text="Maternity" Value="Maternity" />
                                    <asp:ListItem Text="Surgery" Value="Surgery" />
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="btnSaveRoom" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveRoom_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Rooms Table</h3>
                        </div>
                        <asp:GridView ID="gvRooms" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="RoomID" OnSelectedIndexChanged="gvRooms_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="RoomID" HeaderText="ID" />
                                <asp:BoundField DataField="RoomNumber" HeaderText="Room Number" />
                                <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" HeaderText="Action" ButtonType="Button" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>


