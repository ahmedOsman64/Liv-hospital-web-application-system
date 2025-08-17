<%@ Page Title="Contact Form" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Contact Us</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">
            <!-- Contact Form -->
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Add New Contact</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtFullName">Full Name:</label>
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group">
                                <label for="txtEmail">Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group">
                                <label for="txtPhone">Phone:</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group">
                                <label for="txtMessage">Message:</label>
                                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Height="200px" />
                            </div>

                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
