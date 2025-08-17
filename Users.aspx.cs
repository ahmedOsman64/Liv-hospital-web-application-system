using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
  DBConnection objcon = new DBConnection();
  SqlConnection con;
  SqlCommand cmd;
  SqlDataAdapter da;

  void fillUsers()
  {

    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();
      cmd = new SqlCommand("Select Userid, FullName, Phone, Password, Status, cast(Dor as varchar(10)) as DateRegister from Users ;", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);
      gvUsers.DataSource = dt;
      gvUsers.DataBind();


    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }


  protected void Page_Load(object sender, EventArgs e)
  {

    try
    {
      con = objcon.GetConnection(); // Ensure GetConnection() returns a valid SqlConnection
      fillUsers();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('Database Connection Error: " + ex.Message + "');</script>");
    }

  }

  protected void btnAction_Click(object sender, EventArgs e)
    {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      // ============ Input Validations ============

      // Full Name Validation
      if (string.IsNullOrWhiteSpace(txtfullname.Text))
      {
        Response.Write("<script>alert('Please enter your full name.');</script>");
        txtfullname.Focus();
        return;
      }

      // Phone Number Validation
      string phoneInput = txtPhone.Text.Trim();
      if (string.IsNullOrEmpty(phoneInput) || !phoneInput.All(char.IsDigit) || phoneInput.Length < 2 || phoneInput.Length > 15)
      {
        Response.Write("<script>alert('Invalid phone number. Please enter a number with 2-15 digits.');</script>");
        txtPhone.Focus();
        return;
      }
      long phoneNumber = Convert.ToInt64(phoneInput);

      // Password Validation
      if (string.IsNullOrWhiteSpace(txtPassword.Text))
      {
        Response.Write("<script>alert('Please enter a password.');</script>");
        txtPassword.Focus();
        return;
      }

      // Confirm Password Validation
      if (txtPassword.Text.Trim() != txtconfirm.Text.Trim())
      {
        Response.Write("<script>alert('Passwords do not match. Please try again.');</script>");
        txtconfirm.Focus();
        return;
      }

      // ========== Check if Phone Number Already Exists ==========
      if (btnAction.Text == "Save")
      {
        cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Phone = @Phone", con);
        cmd.Parameters.AddWithValue("@Phone", phoneNumber);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        if (count > 0) // If the phone number already exists
        {
          Response.Write("<script>alert('This phone number is already registered. Please use a different number.');</script>");
          txtPhone.Focus();
          txtPhone.Text = "";
          return;
        }
      }

      // ========== Database Insertion ==========
      if (btnAction.Text == "Save")
      {
        // Generate the next User ID
        cmd = new SqlCommand("SELECT ISNULL(MAX(Userid), 0) + 1 FROM Users", con);
        int id = Convert.ToInt32(cmd.ExecuteScalar());

        // Insert user data using parameterized query
        cmd = new SqlCommand("INSERT INTO Users (Userid, FullName, Phone, Password, Status, Dor) VALUES (@Id, @FullName, @Phone, @Password, @Status, @Dor)", con);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
        cmd.Parameters.AddWithValue("@Phone", phoneNumber);
        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
        cmd.Parameters.AddWithValue("@Status", "Active");
        cmd.Parameters.AddWithValue("@Dor", DateTime.Today);

        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
          Response.Write("<script>alert('User Information Has Been Saved Successfully!');</script>");
          txtfullname.Text = "";
          txtPhone.Text = "";
          txtPassword.Text = "";
          txtconfirm.Text = "";
          txtfullname.Focus();
        }
        else
        {
          Response.Write("<script>alert('Error: No data was inserted.');</script>");
        }
      }
      else if (btnAction.Text == "Update")
      {
        if (Session["Userid"] != null) // Ensure we have a UserId
        {
          int userId = Convert.ToInt32(Session["Userid"]); // Use Session for UserId

          // Update user data using parameterized query
          cmd = new SqlCommand("UPDATE Users SET FullName=@FullName, Phone=@Phone, Password=@Password WHERE Userid=@UserId", con);
          cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
          cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
          cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
          cmd.Parameters.AddWithValue("@UserId", userId); // Use session for UserId
          int rowsAffected = cmd.ExecuteNonQuery();
          if (rowsAffected > 0)
          {
            Response.Write("<script>alert('User Information Has Been Updated Successfully!');</script>");
            txtfullname.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            txtconfirm.Text = "";
            txtfullname.Focus();
            btnAction.Text = "Save"; // Reset button text to Save after update
          }
          else
          {
            Response.Write("<script>alert('Error: No data was updated.');</script>");
          }
        }
        else
        {
          Response.Write("<script>alert('Error: User ID not found.');</script>");
        }
      }

      fillUsers(); // Refresh user list after save/update
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
    }
    finally
    {
      if (con != null && con.State == ConnectionState.Open)
        con.Close();
    }

  }


  protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      Session["Userid"] = gvUsers.SelectedRow.Cells[1].Text;
      txtfullname.Text = gvUsers.SelectedRow.Cells[2].Text;
      txtPhone.Text = gvUsers.SelectedRow.Cells[3].Text;

      btnAction.Text = "Update";

    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }

  }
}
