<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  

    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0">Users Registration </h1>
          </div><!-- /.col -->
          
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>

  <section class="content">
      <div class="container-fluid">
        <!-- Small boxes (Stat box) -->

        <div class="row">
          <div class="col-md-12">

            <div class="card card-primary">
              <div class="card-header">
                <h3 class="card-title">Add New User</h3>
              </div>
              <!-- /.card-header -->
              <!-- form start -->
                <div class="card-body">
                  <div class="form-group">
                    <label for="exampleInputEmail1">Full Name:</label>
<asp:TextBox ID="txtfullname" class="form-control" placeholder="Enter Full Name"  runat="server" ></asp:TextBox>
                        </div>

                  
                  <div class="form-group">
                    <label for="exampleInputPassword1">Phone:</label>
<asp:TextBox ID="txtPhone" class="form-control" placeholder="Enter Phone No" runat="server" ></asp:TextBox>
                  </div>

                  <div class="form-group">
                    <label for="exampleInputPassword1">Password:</label>
<asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" placeholder="Enter Password" runat="server"></asp:TextBox>
                  </div>

                  <div class="form-group">
                    <label for="exampleInputPassword1">Confirm Password:</label>
<asp:TextBox ID="txtconfirm" TextMode="Password" class="form-control" placeholder="Enter Confirm Password" runat="server"></asp:TextBox>
                  </div>
                  
                  
                </div>
                <!-- /.card-body -->


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
                <h3 class="card-title">Users Table</h3>

                <div class="card-tools">
                  <div class="input-group input-group-sm" style="width: 150px;">
                    <input type="text" name="table_search" class="form-control float-right" placeholder="Search">

                    <div class="input-group-append">
                      <button type="submit" class="btn btn-default">
                        <i class="fas fa-search"></i>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <!-- /.card-header -->
              <div class="card-body table-responsive p-12">
<asp:GridView ID="gvUsers" class="table table-hover text-nowrap" runat="server" Height="29px" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" >
    <Columns>
        <asp:CommandField ButtonType="Button" ControlStyle-cssclass="btn btn-primary" HeaderText="Action" SelectText="Edit" ShowSelectButton="True" />
    </Columns>
                  </asp:GridView>

               
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>
        </div>



        </div>
    </section>

</asp:Content>


