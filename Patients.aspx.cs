using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patients : System.Web.UI.Page
{
  DBConnection objcon = new DBConnection();
  SqlConnection con;
  SqlCommand cmd;
  SqlDataAdapter da;

  // Fill Patients GridView
  void fillPatients()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT PatientID, FullName, FORMAT(DateOfBirth, 'dd/MM/yyyy') AS DateOfBirth, Gender, Phone, Address, BloodType, FORMAT(CreatedAt, 'dd/MM/yyyy') AS CreatedAt FROM Patients", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);
      gvUsers.DataSource = dt;
      gvUsers.DataBind();
    }
    catch (Exception ex)
    {
      ShowAlert("Error loading patients: " + ex.Message);
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      con = objcon.GetConnection();
      if (!IsPostBack)
      {
        fillPatients();
        ClearForm();
      }
    }
    catch (Exception ex)
    {
      ShowAlert("Database Connection Error: " + ex.Message);
    }
  }

  protected void btnAction_Click(object sender, EventArgs e)
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      if (!IsValidForm()) return;

      if (btnAction.Text == "Save")
      {
        cmd = new SqlCommand("SELECT ISNULL(MAX(PatientID), 0) + 1 FROM Patients", con);
        int id = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new SqlCommand("INSERT INTO Patients (PatientID, FullName, DateOfBirth, Gender, Address, Phone, BloodType, CreatedAt) VALUES (@Id, @FullName, @DateOfBirth, @Gender, @Address, @Phone, @BloodType, @CreatedAt)", con);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
        cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.ParseExact(txtDateOfBirth.Text.Trim(), "dd/MM/yyyy", null));
        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue);
        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
        cmd.Parameters.AddWithValue("@BloodType", cmbBloodType.SelectedValue);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Today);

        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
          ShowAlert("Patient information has been saved successfully!");
          ClearForm();
        }
        else
        {
          ShowAlert("Error: No data was inserted.");
        }
      }
      else if (btnAction.Text == "Update")
      {
        cmd = new SqlCommand("UPDATE Patients SET FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender, Address = @Address, Phone = @Phone, BloodType = @BloodType WHERE PatientID = @PatientID", con);
        cmd.Parameters.AddWithValue("@PatientID", ViewState["PatientID"]);
        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text.Trim());
        cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.ParseExact(txtDateOfBirth.Text.Trim(), "dd/MM/yyyy", null));
        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue);
        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
        cmd.Parameters.AddWithValue("@BloodType", cmbBloodType.SelectedValue);

        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
          ShowAlert("Patient information has been updated successfully!");
          ClearForm();
        }
        else
        {
          ShowAlert("Error: No data was updated.");
        }
      }

      fillPatients();
    }
    catch (Exception ex)
    {
      ShowAlert("Error: " + ex.Message);
    }
    finally
    {
      if (con != null && con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
  {
    GridViewRow row = gvUsers.SelectedRow;
    ViewState["PatientID"] = row.Cells[1].Text;

    txtfullname.Text = row.Cells[2].Text;

    DateTime dob;
    if (DateTime.TryParseExact(row.Cells[3].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
    {
      txtDateOfBirth.Text = dob.ToString("dd/MM/yyyy");
    }
    else
    {
      txtDateOfBirth.Text = "";
    }

    cmbGender.SelectedValue = row.Cells[4].Text;
    txtPhone.Text = row.Cells[5].Text;
    txtAddress.Text = row.Cells[6].Text;
    cmbBloodType.SelectedValue = row.Cells[7].Text;

    btnAction.Text = "Update";
  }

  private void ShowAlert(string message)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", message.Replace("'", "\\'")), true);
  }

  private void ClearForm()
  {
    txtfullname.Text = "";
    txtDateOfBirth.Text = "";
    cmbGender.SelectedIndex = 0;
    txtAddress.Text = "";
    txtPhone.Text = "";
    cmbBloodType.SelectedIndex = 0;
    btnAction.Text = "Save";
    txtfullname.Focus();
  }

  private bool IsValidForm()
  {
    if (string.IsNullOrWhiteSpace(txtfullname.Text) ||
        string.IsNullOrWhiteSpace(txtDateOfBirth.Text) ||
        string.IsNullOrWhiteSpace(txtPhone.Text) ||
        string.IsNullOrWhiteSpace(txtAddress.Text) ||
        cmbGender.SelectedIndex == 0 ||
        cmbBloodType.SelectedIndex == 0)
    {
      ShowAlert("Please fill all required fields correctly.");
      return false;
    }

    DateTime dob;
    if (!DateTime.TryParseExact(txtDateOfBirth.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
    {
      ShowAlert("Invalid date format. Use dd/MM/yyyy.");
      return false;
    }

    return true;
  }
}
