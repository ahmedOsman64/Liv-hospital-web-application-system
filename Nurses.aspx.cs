using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nurses : System.Web.UI.Page
{
  DBConnection objcon = new DBConnection();
  SqlConnection con;
  SqlCommand cmd;
  SqlDataAdapter da;

  protected void Page_Load(object sender, EventArgs e)
  {
    con = objcon.GetConnection();
    if (!IsPostBack)
    {
      FillNurses();
      ClearForm();
    }
  }

  void FillNurses()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT NurseID, NurseName, Department, Phone, Email, WorkingHours, Gender FROM Nurses", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);

      gvNurses.DataSource = dt;
      gvNurses.DataBind();
    }
    catch (Exception ex)
    {
      ShowAlert("Error loading nurses: " + ex.Message);
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void btnNurseAction_Click(object sender, EventArgs e)
  {
    try
    {
      if (!IsValidForm()) return;

      if (con.State == ConnectionState.Closed)
        con.Open();

      if (btnNurseAction.Text == "Save")
      {
        cmd = new SqlCommand(@"INSERT INTO Nurses 
                    (NurseName, Department, Gender, Phone, Email, WorkingHours) 
                    VALUES (@NurseName, @Department, @Gender, @Phone, @Email, @WorkingHours)", con);
      }
      else if (btnNurseAction.Text == "Update")
      {
        cmd = new SqlCommand(@"UPDATE Nurses SET 
                    NurseName = @NurseName, Department = @Department, Gender = @Gender, 
                    Phone = @Phone, Email = @Email, WorkingHours = @WorkingHours 
                    WHERE NurseID = @NurseID", con);

        cmd.Parameters.AddWithValue("@NurseID", ViewState["NurseID"]);
      }

      cmd.Parameters.AddWithValue("@NurseName", txtNurseName.Text.Trim());
      cmd.Parameters.AddWithValue("@Department", txtDepartment.Text.Trim());
      cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue);
      cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
      cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
      cmd.Parameters.AddWithValue("@WorkingHours", txtWorkingHours.Text.Trim());

      int rowsAffected = cmd.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        ShowAlert(btnNurseAction.Text == "Save" ? "Nurse registered successfully!" : "Nurse details updated!");
        ClearForm();
        FillNurses();
      }
      else
      {
        ShowAlert(btnNurseAction.Text == "Save" ? "Insert failed." : "Update failed.");
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

  protected void gvNurses_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = gvNurses.SelectedIndex;
    if (selectedIndex < 0 || selectedIndex >= gvNurses.Rows.Count)
      return;

    string nurseID = gvNurses.DataKeys[selectedIndex].Value.ToString();
    ViewState["NurseID"] = nurseID;

    if (con.State == ConnectionState.Closed)
      con.Open();

    SqlCommand getDetailsCmd = new SqlCommand("SELECT * FROM Nurses WHERE NurseID = @NurseID", con);
    getDetailsCmd.Parameters.AddWithValue("@NurseID", nurseID);
    SqlDataReader reader = getDetailsCmd.ExecuteReader();
    if (reader.Read())
    {
      txtNurseName.Text = reader["NurseName"].ToString();
      txtDepartment.Text = reader["Department"].ToString();
      cmbGender.SelectedValue = reader["Gender"].ToString();
      txtPhone.Text = reader["Phone"].ToString();
      txtEmail.Text = reader["Email"].ToString();
      txtWorkingHours.Text = reader["WorkingHours"].ToString();
    }
    reader.Close();
    con.Close();

    btnNurseAction.Text = "Update";
  }

  private void ClearForm()
  {
    txtNurseName.Text = "";
    txtDepartment.Text = "";
    cmbGender.SelectedIndex = 0;
    txtPhone.Text = "";
    txtEmail.Text = "";
    txtWorkingHours.Text = "";
    btnNurseAction.Text = "Save";
    ViewState["NurseID"] = null;
  }

  private bool IsValidForm()
  {
    if (string.IsNullOrWhiteSpace(txtNurseName.Text) ||
        string.IsNullOrWhiteSpace(txtDepartment.Text) ||
        cmbGender.SelectedIndex == 0 ||
        string.IsNullOrWhiteSpace(txtPhone.Text))
    {
      ShowAlert("Please fill in the required fields.");
      return false;
    }

    return true;
  }

  private void ShowAlert(string message)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", message.Replace("'", "\\'")), true);
  }
}
