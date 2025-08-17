<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Nurses.aspx.cs" Inherits="Nurses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Nurse Registration</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">
            <!-- Nurse Registration Form -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Add New Nurse</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtNurseName">Full Name:</label>
                                <asp:TextBox ID="txtNurseName" runat="server" CssClass="form-control" Placeholder="Enter Full Name" />
                            </div>

                            <div class="form-group">
                                <label for="txtDepartment">Department:</label>
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" Placeholder="e.g. Pediatrics" />
                            </div>

                            <div class="form-group">
                                <label for="cmbGender">Gender:</label>
                                <asp:DropDownList ID="cmbGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Gender" Value="" />
                                    <asp:ListItem Text="Male" Value="Male" />
                                    <asp:ListItem Text="Female" Value="Female" />
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="txtPhone">Phone:</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Enter Phone No" />
                            </div>

                            <div class="form-group">
                                <label for="txtEmail">Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter Email" />
                            </div>

                            <div class="form-group">
                                <label for="txtWorkingHours">Working Hours:</label>
                                <asp:TextBox ID="txtWorkingHours" runat="server" CssClass="form-control" Placeholder="e.g. 9 AM - 5 PM" />
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnNurseAction" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnNurseAction_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Nurses Table -->
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Nurses Table</h3>
                        </div>
                        <asp:GridView ID="gvNurses" runat="server" AutoGenerateColumns="False" CssClass="table table-hover text-nowrap" OnSelectedIndexChanged="gvNurses_SelectedIndexChanged" DataKeyNames="NurseID" >
                            <Columns>
                                <asp:BoundField DataField="NurseID" HeaderText="ID" />
                                <asp:BoundField DataField="NurseName" HeaderText="Name" />
                                <asp:BoundField DataField="Department" HeaderText="Department" />
                                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="WorkingHours" HeaderText="Working Hours" />
                                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" HeaderText="Action" ControlStyle-CssClass="btn btn-primary" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
