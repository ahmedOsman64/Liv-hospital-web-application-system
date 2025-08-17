using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Appointments : System.Web.UI.Page
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
      LoadPatients();
      LoadDoctors();
      LoadAppointments();
    }
  }

  void LoadPatients()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT PatientID, FullName FROM Patients", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);

      ddlPatient.DataSource = dt;
      ddlPatient.DataTextField = "FullName";
      ddlPatient.DataValueField = "PatientID";
      ddlPatient.DataBind();
      ddlPatient.Items.Insert(0, new ListItem("-- Select Patient --", ""));
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  void LoadDoctors()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT DoctorID, DoctorName FROM Doctors", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);

      ddlDoctor.DataSource = dt;
      ddlDoctor.DataTextField = "DoctorName";
      ddlDoctor.DataValueField = "DoctorID";
      ddlDoctor.DataBind();
      ddlDoctor.Items.Insert(0, new ListItem("-- Select Doctor --", ""));
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  void LoadAppointments()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      string query = @"
                SELECT A.AppointmentID, P.FullName AS PatientName, D.DoctorName, A.AppointmentDate, A.Reason
                FROM Appointments A
                JOIN Patients P ON A.PatientID = P.PatientID
                JOIN Doctors D ON A.DoctorID = D.DoctorID";

      da = new SqlDataAdapter(query, con);
      DataTable dt = new DataTable();
      da.Fill(dt);

      gvAppointments.DataSource = dt;
      gvAppointments.DataBind();
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void btnSaveAppointment_Click(object sender, EventArgs e)
  {
    if (ddlPatient.SelectedIndex == 0 || ddlDoctor.SelectedIndex == 0 || string.IsNullOrWhiteSpace(txtDate.Text))
    {
      ShowMessage("Please fill in all required fields.");
      return;
    }

    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      SqlCommand insertCmd = new SqlCommand(@"
                INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Reason)
                VALUES (@PatientID, @DoctorID, @AppointmentDate, @Reason)", con);

      insertCmd.Parameters.AddWithValue("@PatientID", ddlPatient.SelectedValue);
      insertCmd.Parameters.AddWithValue("@DoctorID", ddlDoctor.SelectedValue);
      insertCmd.Parameters.AddWithValue("@AppointmentDate", Convert.ToDateTime(txtDate.Text));
      insertCmd.Parameters.AddWithValue("@Reason", txtReason.Text.Trim());

      int rows = insertCmd.ExecuteNonQuery();
      if (rows > 0)
      {
        ShowMessage("Appointment saved successfully.");
        ClearForm();
        LoadAppointments();
      }
      else
      {
        ShowMessage("Failed to save appointment.");
      }
    }
    catch (Exception ex)
    {
      ShowMessage("Error: " + ex.Message);
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void gvAppointments_SelectedIndexChanged(object sender, EventArgs e)
  {
    GridViewRow row = gvAppointments.SelectedRow;
    txtDate.Text = row.Cells[3].Text;
    txtReason.Text = row.Cells[4].Text;

    ddlPatient.SelectedItem.Text = row.Cells[1].Text;
    ddlDoctor.SelectedItem.Text = row.Cells[2].Text;

    ShowMessage("Loaded data into form. Update feature not implemented yet.");
  }

  private void ClearForm()
  {
    ddlPatient.SelectedIndex = 0;
    ddlDoctor.SelectedIndex = 0;
    txtDate.Text = "";
    txtReason.Text = "";
  }

  private void ShowMessage(string msg)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", msg.Replace("'", "\\'")), true);
  }
}
