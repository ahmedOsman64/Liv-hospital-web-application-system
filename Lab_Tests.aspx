<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Lab_Tests.aspx.cs" Inherits="Lab_Tests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Lab Test Management</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">

            <!-- Form to Add / Edit Lab Test -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Add / Edit Lab Test</h3>
                        </div>
                        <div class="card-body">

                            <asp:HiddenField ID="hfLabTestID" runat="server" />

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
                                <label>Test Name:</label>
                                <asp:TextBox ID="txtTestName" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Test Date:</label>
                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control" TextMode="Date" />
                            </div>
                            <div class="form-group">
                                <label>Status:</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Pending" Value="Pending" />
                                    <asp:ListItem Text="Completed" Value="Completed" />
                                    <asp:ListItem Text="Cancelled" Value="Cancelled" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Result:</label>
                                <asp:TextBox ID="txtResult" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>

                            <asp:Button ID="btnSaveLabTest" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveLabTest_Click" />

                        </div>
                    </div>
                </div>
            </div>

            <!-- Lab Test Table -->
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Lab Tests Table</h3>
                        </div>
                        <asp:GridView ID="gvLabTests" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="LabTestID" OnSelectedIndexChanged="gvLabTests_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="LabTestID" HeaderText="ID" />
                                <asp:BoundField DataField="PatientName" HeaderText="Patient" />
                                <asp:BoundField DataField="DoctorName" HeaderText="Doctor" />
                                <asp:BoundField DataField="TestName" HeaderText="Test" />
                                <asp:BoundField DataField="TestDate" HeaderText="Date" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="Result" HeaderText="Result" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" HeaderText="Action" ButtonType="Button" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </section>
</asp:Content>
