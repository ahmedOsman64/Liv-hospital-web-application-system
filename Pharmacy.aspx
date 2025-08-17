<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Pharmacy.aspx.cs" Inherits="Pharmacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Pharmacy Management</h1>
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
                            <h3 class="card-title">Add New Medicine</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label>Medicine Name:</label>
                                <asp:TextBox ID="txtMedicineName" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Category:</label>
                                <asp:DropDownList ID="cmbCategory" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- Select Category --" Value="" />
                                    <asp:ListItem Text="Tablet" Value="Tablet" />
                                    <asp:ListItem Text="Syrup" Value="Syrup" />
                                    <asp:ListItem Text="Injection" Value="Injection" />
                                    <asp:ListItem Text="Ointment" Value="Ointment" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Quantity:</label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="form-group">
                                <label>Price:</label>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>
                            <asp:Button ID="btnSaveMedicine" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveMedicine_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Pharmacy Inventory</h3>
                        </div>
                        <asp:GridView ID="gvPharmacy" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="MedicineID" OnSelectedIndexChanged="gvPharmacy_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="MedicineID" HeaderText="ID" />
                                <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name" />
                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="Price" HeaderText="Price ($)" DataFormatString="{0:F2}" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Edit" HeaderText="Action" ButtonType="Button" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
