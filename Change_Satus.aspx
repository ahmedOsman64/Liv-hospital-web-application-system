<%@ Page Title="" Language="C#" MasterPageFile="~/Mainpage.master" AutoEventWireup="true" CodeFile="Change_Satus.aspx.cs" Inherits="Change_Satus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0">Change Status </h1>
          </div><!-- /.col -->
          
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>

  
  <section class="content">
      <div class="container-fluid">
        <!-- Small boxes (Stat box) -->

        <section class="content">
      <div class="container-fluid">
        <!-- Small boxes (Stat box) -->

        <div class="row">
          <div class="col-md-12">

            <div class="card card-primary">
              <div class="card-header">
                <h3 class="card-title">Change User Status</h3>
              </div>
              <!-- /.card-header -->
              <!-- form start -->
                <div class="card-body">
                  <div class="form-group">
                    <label for="exampleInputEmail1">Select Phone No:</label>
<asp:DropDownList ID="ddlphone" class="Select form-control" runat="server"></asp:DropDownList>


                        </div>

                  
                  <div class="form-group">
                    <label for="exampleInputPassword1">Select Status:</label>
                    <asp:RadioButton ID="rbtnactive" Text="Active" runat="server" />
                    <asp:RadioButton ID="rbtnblock" Text="Block" runat="server" />

                 </div>

                  
                  
                </div>
                <!-- /.card-body -->


                <div class="card-footer">
<asp:Button ID="btnchagestatus" class="btn btn-primary" runat="server" Text="Change user Status" OnClick="btnchagestatus_Click" />
                </div>
            </div>

            </div>
          </div>

        <div class="row">
          <div class="col-12">
            <div class="card">
              <div class="card-header">
                <h3 class="card-title">Change Status Table</h3>

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
<asp:GridView ID="gvchangestatus" class="table table-hover text-nowrap" runat="server" Height="29px" OnSelectedIndexChanged="gvchangestatus_SelectedIndexChanged" >
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

        <div class="row">
          <div class="col-md-12">
            </div>
          </div>


        </div>
    </section>

</asp:Content>

