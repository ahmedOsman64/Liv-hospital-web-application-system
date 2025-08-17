<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Patients.aspx.cs" Inherits="Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0">Patient Registration</h1>
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
                <h3 class="card-title">Add New Patient</h3>
              </div>
              <div class="card-body">
                  <div class="form-group">
                    <label for="txtfullname">Full Name:</label>
                    <asp:TextBox ID="txtfullname" class="form-control" placeholder="Enter Full Name" runat="server"></asp:TextBox>
                  </div>

                  <div class="form-group">
                    <label for="txtDateOfBirth">Date of Birth:</label>
                    <asp:TextBox ID="txtDateOfBirth" class="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                  </div>

                  <div class="form-group">
                    <label for="cmbGender">Gender:</label>
                    <asp:DropDownList ID="cmbGender" class="form-control" runat="server">
                        <asp:ListItem Text="Select Gender" Value="" />
                        <asp:ListItem Text="Male" Value="Male" />
                        <asp:ListItem Text="Female" Value="Female" />
                    </asp:DropDownList>
                  </div>

                  <div class="form-group">
                    <label for="txtPhone">Phone:</label>
                    <asp:TextBox ID="txtPhone" class="form-control" placeholder="Enter Phone No" runat="server"></asp:TextBox>
                  </div>

                  <div class="form-group">
                    <label for="txtAddress">Address:</label>
                    <asp:TextBox ID="txtAddress" class="form-control" placeholder="Enter Address" runat="server"></asp:TextBox>
                  </div>

                  <div class="form-group">
                    <label for="cmbBloodType">Blood Type:</label>
                    <asp:DropDownList ID="cmbBloodType" class="form-control" runat="server">
                        <asp:ListItem Text="Select Blood Type" Value="" />
                        <asp:ListItem Text="A+" Value="A+" />
                        <asp:ListItem Text="A-" Value="A-" />
                        <asp:ListItem Text="B+" Value="B+" />
                        <asp:ListItem Text="B-" Value="B-" />
                        <asp:ListItem Text="O+" Value="O+" />
                        <asp:ListItem Text="O-" Value="O-" />
                        <asp:ListItem Text="AB+" Value="AB+" />
                        <asp:ListItem Text="AB-" Value="AB-" />
                    </asp:DropDownList>
                  </div>
              </div>

              <div class="card-footer">
                <asp:Button ID="btnAction" class="btn btn-primary" runat="server" Text="Save" OnClick="btnAction_Click" />
              </div>
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col-12">
            <div class="card">
              <div class="card-header">
                <h3 class="card-title">Patients Table</h3>
                <div class="card-tools">
                  <div class="input-group input-group-sm" style="width: 150px;">
                    <input type="search" name="table_search" class="form-control float-right" placeholder="Search">
                    <div class="input-group-append">
                      <button type="submit" class="btn btn-default">
                        <i class="fas fa-search"></i>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-body table-responsive p-0">
                <asp:GridView ID="gvUsers" class="table table-hover text-nowrap" runat="server" Height="25px" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" Width="580px">
                    <Columns>
                        <asp:CommandField ButtonType="Button" ControlStyle-cssclass="btn btn-primary" HeaderText="Action" SelectText="Edit" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
              </div>
            </div>
          </div>
        </div>
      </div>
  </section>
</asp:Content>
