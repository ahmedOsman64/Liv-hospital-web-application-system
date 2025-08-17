<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Appointments.aspx.cs" Inherits="Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Appointment Management</h1>
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
                            <h3 class="card-title">Add New Appointment</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label>Patient:</label>
                                <asp:DropDownList ID="ddlPatient" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- Select Patient --" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Doctor:</label>
                                <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- Select Doctor --" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Appointment Date:</label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Reason:</label>
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" />
                            </div>
                            <asp:Button ID="btnSaveAppointment" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveAppointment_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Appointments Table</h3>
                        </div>
                        <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="AppointmentID" OnSelectedIndexChanged="gvAppointments_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="AppointmentID" HeaderText="ID" />
                                <asp:BoundField DataField="PatientName" HeaderText="Patient" />
                                <asp:BoundField DataField="DoctorName" HeaderText="Doctor" />
                                <asp:BoundField DataField="AppointmentDate" HeaderText="Date" SortExpression="AppointmentDate" />
                                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" HeaderText="Action" ButtonType="Button" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
