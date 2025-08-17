using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Lab_Tests : System.Web.UI.Page
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
      LoadLabTests();
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

  void LoadLabTests()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      string query = @"
                SELECT LT.LabTestID, P.FullName AS PatientName, D.DoctorName, 
                       LT.TestName, LT.TestDate, LT.Status, LT.Result
                FROM Lab_Tests LT
                JOIN Patients P ON LT.PatientID = P.PatientID
                JOIN Doctors D ON LT.DoctorID = D.DoctorID";

      da = new SqlDataAdapter(query, con);
      DataTable dt = new DataTable();
      da.Fill(dt);

      gvLabTests.DataSource = dt;
      gvLabTests.DataBind();
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void btnSaveLabTest_Click(object sender, EventArgs e)
  {
    if (ddlPatient.SelectedIndex == 0 || ddlDoctor.SelectedIndex == 0 || string.IsNullOrWhiteSpace(txtTestDate.Text) || string.IsNullOrWhiteSpace(txtTestName.Text))
    {
      ShowMessage("Please fill in all required fields.");
      return;
    }

    DateTime testDate;
    if (!DateTime.TryParse(txtTestDate.Text.Trim(), out testDate))
    {
      ShowMessage("Please enter a valid Test Date.");
      return;
    }

    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      if (string.IsNullOrEmpty(hfLabTestID.Value))  // Insert mode
      {
        SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO Lab_Tests (PatientID, DoctorID, TestName, TestDate, Status, Result)
                    VALUES (@PatientID, @DoctorID, @TestName, @TestDate, @Status, @Result)", con);

        insertCmd.Parameters.AddWithValue("@PatientID", ddlPatient.SelectedValue);
        insertCmd.Parameters.AddWithValue("@DoctorID", ddlDoctor.SelectedValue);
        insertCmd.Parameters.AddWithValue("@TestName", txtTestName.Text.Trim());
        insertCmd.Parameters.AddWithValue("@TestDate", testDate);
        insertCmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
        insertCmd.Parameters.AddWithValue("@Result", txtResult.Text.Trim());

        int rows = insertCmd.ExecuteNonQuery();
        if (rows > 0)
        {
          ShowMessage("Lab test saved successfully.");
          ClearForm();
          LoadLabTests();
        }
        else
        {
          ShowMessage("Failed to save lab test.");
        }
      }
      else  // Update mode
      {
        SqlCommand updateCmd = new SqlCommand(@"
                    UPDATE Lab_Tests
                    SET PatientID = @PatientID,
                        DoctorID = @DoctorID,
                        TestName = @TestName,
                        TestDate = @TestDate,
                        Status = @Status,
                        Result = @Result
                    WHERE LabTestID = @LabTestID", con);

        updateCmd.Parameters.AddWithValue("@PatientID", ddlPatient.SelectedValue);
        updateCmd.Parameters.AddWithValue("@DoctorID", ddlDoctor.SelectedValue);
        updateCmd.Parameters.AddWithValue("@TestName", txtTestName.Text.Trim());
        updateCmd.Parameters.AddWithValue("@TestDate", testDate);
        updateCmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
        updateCmd.Parameters.AddWithValue("@Result", txtResult.Text.Trim());
        updateCmd.Parameters.AddWithValue("@LabTestID", Convert.ToInt32(hfLabTestID.Value));

        int rows = updateCmd.ExecuteNonQuery();
        if (rows > 0)
        {
          ShowMessage("Lab test updated successfully.");
          ClearForm();
          LoadLabTests();
          btnSaveLabTest.Text = "Save";
          hfLabTestID.Value = "";
        }
        else
        {
          ShowMessage("Failed to update lab test.");
        }
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

  protected void gvLabTests_SelectedIndexChanged(object sender, EventArgs e)
  {
    GridViewRow row = gvLabTests.SelectedRow;

    hfLabTestID.Value = gvLabTests.DataKeys[row.RowIndex].Value.ToString();

    txtTestName.Text = row.Cells[4].Text;
    DateTime testDate;
    if (DateTime.TryParse(row.Cells[5].Text, out testDate))
      txtTestDate.Text = testDate.ToString("yyyy-MM-dd");
    else
      txtTestDate.Text = "";

    ddlStatus.SelectedValue = row.Cells[6].Text;
    txtResult.Text = row.Cells[7].Text;

    ddlPatient.ClearSelection();
    ListItem patientItem = ddlPatient.Items.FindByText(row.Cells[1].Text);
    if (patientItem != null) patientItem.Selected = true;

    ddlDoctor.ClearSelection();
    ListItem doctorItem = ddlDoctor.Items.FindByText(row.Cells[2].Text);
    if (doctorItem != null) doctorItem.Selected = true;

    btnSaveLabTest.Text = "Update";
  }

  protected void gvLabTests_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
    gvLabTests.PageIndex = e.NewPageIndex;
    LoadLabTests();
  }

  void ClearForm()
  {
    ddlPatient.SelectedIndex = 0;
    ddlDoctor.SelectedIndex = 0;
    txtTestName.Text = "";
    txtTestDate.Text = "";
    ddlStatus.SelectedIndex = 0;
    txtResult.Text = "";
    hfLabTestID.Value = "";
    btnSaveLabTest.Text = "Save";
  }

  void ShowMessage(string message)
  {
    Response.Write("<script>alert('" + message + "');</script>");
  }
}
