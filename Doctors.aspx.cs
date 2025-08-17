using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctors : System.Web.UI.Page
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
      FillDoctors();
      ClearForm();
    }
  }

  void FillDoctors()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT DoctorID, DoctorName, Specialization, ExperienceYears, Phone, Email, Nationality, WorkingHours, Gender FROM Doctors", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);

      gvDoctors.DataSource = dt;
      gvDoctors.DataBind();
    }
    catch (Exception ex)
    {
      ShowAlert("Error loading doctors: " + ex.Message);
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void btnDoctorAction_Click(object sender, EventArgs e)
  {
    try
    {
      if (!IsValidForm()) return;

      if (con.State == ConnectionState.Closed)
        con.Open();

      if (btnDoctorAction.Text == "Save")
      {
        cmd = new SqlCommand(@"INSERT INTO Doctors 
                    (DoctorName, Specialization, ExperienceYears, Gender, Phone, Email, Nationality, WorkingHours) 
                    VALUES (@DoctorName, @Specialization, @ExperienceYears, @Gender, @Phone, @Email, @Nationality, @WorkingHours)", con);
      }
      else if (btnDoctorAction.Text == "Update")
      {
        cmd = new SqlCommand(@"UPDATE Doctors SET 
                    DoctorName = @DoctorName, Specialization = @Specialization, ExperienceYears = @ExperienceYears, 
                    Gender = @Gender, Phone = @Phone, Email = @Email, Nationality = @Nationality, 
                    WorkingHours = @WorkingHours 
                    WHERE DoctorID = @DoctorID", con);

        cmd.Parameters.AddWithValue("@DoctorID", ViewState["DoctorID"]);
      }

      cmd.Parameters.AddWithValue("@DoctorName", txtDoctorName.Text.Trim());
      cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
      cmd.Parameters.AddWithValue("@ExperienceYears", txtExperience.Text.Trim());
      cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue);
      cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
      cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
      cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text.Trim());
      cmd.Parameters.AddWithValue("@WorkingHours", txtWorkingHours.Text.Trim());

      int rowsAffected = cmd.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        ShowAlert(btnDoctorAction.Text == "Save" ? "Doctor registered successfully!" : "Doctor details updated!");
        ClearForm();
        FillDoctors();
      }
      else
      {
        ShowAlert(btnDoctorAction.Text == "Save" ? "Insert failed." : "Update failed.");
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

  protected void gvDoctors_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = gvDoctors.SelectedIndex;
    if (selectedIndex < 0 || selectedIndex >= gvDoctors.Rows.Count)
      return;

    string doctorID = gvDoctors.DataKeys[selectedIndex].Value.ToString();
    ViewState["DoctorID"] = doctorID;

    if (con.State == ConnectionState.Closed)
      con.Open();

    SqlCommand getDetailsCmd = new SqlCommand("SELECT * FROM Doctors WHERE DoctorID = @DoctorID", con);
    getDetailsCmd.Parameters.AddWithValue("@DoctorID", doctorID);
    SqlDataReader reader = getDetailsCmd.ExecuteReader();
    if (reader.Read())
    {
      txtDoctorName.Text = reader["DoctorName"].ToString();
      txtSpecialization.Text = reader["Specialization"].ToString();
      txtExperience.Text = reader["ExperienceYears"].ToString();
      cmbGender.SelectedValue = reader["Gender"].ToString();
      txtPhone.Text = reader["Phone"].ToString();
      txtEmail.Text = reader["Email"].ToString();
      txtNationality.Text = reader["Nationality"].ToString();
      txtWorkingHours.Text = reader["WorkingHours"].ToString();
    }
    reader.Close();
    con.Close();

    btnDoctorAction.Text = "Update";
  }

  private void ClearForm()
  {
    txtDoctorName.Text = "";
    txtSpecialization.Text = "";
    txtExperience.Text = "";
    cmbGender.SelectedIndex = 0;
    txtPhone.Text = "";
    txtEmail.Text = "";
    txtNationality.Text = "";
    txtWorkingHours.Text = "";
    btnDoctorAction.Text = "Save";
    ViewState["DoctorID"] = null;
  }

  private bool IsValidForm()
  {
    if (string.IsNullOrWhiteSpace(txtDoctorName.Text) ||
        string.IsNullOrWhiteSpace(txtSpecialization.Text) ||
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
