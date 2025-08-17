<%@ Page Title="Doctor Registration" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Doctors.aspx.cs" Inherits="Doctors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Doctor Registration</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">
            <!-- Doctor Registration Form -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Add New Doctor</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtDoctorName">Full Name:</label>
                                <asp:TextBox ID="txtDoctorName" runat="server" CssClass="form-control" Placeholder="Enter Full Name" />
                            </div>

                            <div class="form-group">
                                <label for="txtSpecialization">Specialization:</label>
                                <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" Placeholder="e.g. Cardiologist" />
                            </div>

                            <div class="form-group">
                                <label for="txtExperience">Years of Experience:</label>
                                <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" Placeholder="e.g. 10" />
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
                                <label for="txtNationality">Nationality:</label>
                                <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" Placeholder="e.g. Somali" />
                            </div>

                            <div class="form-group">
                                <label for="txtWorkingHours">Working Hours:</label>
                                <asp:TextBox ID="txtWorkingHours" runat="server" CssClass="form-control" Placeholder="e.g. 8 AM - 4 PM" />
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnDoctorAction" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnDoctorAction_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Doctors Table -->
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Doctors Table</h3>
                        </div>
                        <asp:GridView ID="gvDoctors" runat="server" AutoGenerateColumns="False" CssClass="table table-hover text-nowrap" Width="100%" OnSelectedIndexChanged="gvDoctors_SelectedIndexChanged" DataKeyNames="DoctorID">
    <Columns>
        <asp:BoundField DataField="DoctorID" HeaderText="ID" />
        <asp:BoundField DataField="DoctorName" HeaderText="Name" />
        <asp:BoundField DataField="Specialization" HeaderText="Specialization" />
        <asp:BoundField DataField="Phone" HeaderText="Phone" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Nationality" HeaderText="Nationality" />
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
