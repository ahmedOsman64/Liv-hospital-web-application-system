using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Contact : System.Web.UI.Page
{
  // Declare variables for the database connection
  DBConnection objcon = new DBConnection();
  SqlConnection con;
  SqlCommand cmd;

  protected void Page_Load(object sender, EventArgs e)
  {
    con = objcon.GetConnection();
  }

  // Handle form submission
  protected void btnSubmit_Click(object sender, EventArgs e)
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand(@"INSERT INTO Contacts (FullName, Email, Phone, Message) 
                                  VALUES (@FullName, @Email, @Phone, @Message)", con);
      cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
      cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
      cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
      cmd.Parameters.AddWithValue("@Message", txtMessage.Text.Trim());

      int rowsAffected = cmd.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        ShowAlert("Your message has been sent successfully.");
        ClearForm();
      }
      else
      {
        ShowAlert("Message submission failed.");
      }
    }
    catch (Exception ex)
    {
      ShowAlert("Error: " + ex.Message);
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  // Clear form after submission
  private void ClearForm()
  {
    txtFullName.Text = "";
    txtEmail.Text = "";
    txtPhone.Text = "";
    txtMessage.Text = "";
  }

  // Show alert message
  private void ShowAlert(string msg)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", msg.Replace("'", "\\'")), true);
  }
}
