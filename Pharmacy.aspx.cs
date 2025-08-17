using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pharmacy : System.Web.UI.Page
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
      FillPharmacy();
      ClearForm();
    }
  }

  void FillPharmacy()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT MedicineID, MedicineName, Category, Quantity, Price FROM Pharmacy", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);

      gvPharmacy.DataSource = dt;
      gvPharmacy.DataBind();
    }
    catch (Exception ex)
    {
      ShowAlert("Error loading pharmacy items: " + ex.Message);
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void btnSaveMedicine_Click(object sender, EventArgs e)
  {
    try
    {
      if (!IsValidForm()) return;

      if (con.State == ConnectionState.Closed)
        con.Open();

      if (btnSaveMedicine.Text == "Save")
      {
        cmd = new SqlCommand(@"INSERT INTO Pharmacy 
                    (MedicineName, Category, Quantity, Price) 
                    VALUES (@MedicineName, @Category, @Quantity, @Price)", con);
      }
      else if (btnSaveMedicine.Text == "Update")
      {
        cmd = new SqlCommand(@"UPDATE Pharmacy SET 
                    MedicineName = @MedicineName, Category = @Category, 
                    Quantity = @Quantity, Price = @Price 
                    WHERE MedicineID = @MedicineID", con);

        cmd.Parameters.AddWithValue("@MedicineID", ViewState["MedicineID"]);
      }

      cmd.Parameters.AddWithValue("@MedicineName", txtMedicineName.Text.Trim());
      cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedValue);
      cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
      cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());

      int rowsAffected = cmd.ExecuteNonQuery();
      if (rowsAffected > 0)
      {
        ShowAlert(btnSaveMedicine.Text == "Save" ? "Medicine saved successfully!" : "Medicine updated!");
        ClearForm();
        FillPharmacy();
      }
      else
      {
        ShowAlert(btnSaveMedicine.Text == "Save" ? "Insert failed." : "Update failed.");
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

  protected void gvPharmacy_SelectedIndexChanged(object sender, EventArgs e)
  {
    int selectedIndex = gvPharmacy.SelectedIndex;
    if (selectedIndex < 0 || selectedIndex >= gvPharmacy.Rows.Count)
      return;

    string medicineID = gvPharmacy.DataKeys[selectedIndex].Value.ToString();
    ViewState["MedicineID"] = medicineID;

    if (con.State == ConnectionState.Closed)
      con.Open();

    SqlCommand getDetailsCmd = new SqlCommand("SELECT * FROM Pharmacy WHERE MedicineID = @MedicineID", con);
    getDetailsCmd.Parameters.AddWithValue("@MedicineID", medicineID);
    SqlDataReader reader = getDetailsCmd.ExecuteReader();
    if (reader.Read())
    {
      txtMedicineName.Text = reader["MedicineName"].ToString();
      cmbCategory.SelectedValue = reader["Category"].ToString();
      txtQuantity.Text = reader["Quantity"].ToString();
      txtPrice.Text = reader["Price"].ToString();
    }
    reader.Close();
    con.Close();

    btnSaveMedicine.Text = "Update";
  }

  private void ClearForm()
  {
    txtMedicineName.Text = "";
    cmbCategory.SelectedIndex = 0;
    txtQuantity.Text = "";
    txtPrice.Text = "";
    btnSaveMedicine.Text = "Save";
    ViewState["MedicineID"] = null;
  }

  private bool IsValidForm()
  {
    if (string.IsNullOrWhiteSpace(txtMedicineName.Text) ||
        cmbCategory.SelectedIndex == 0 ||
        string.IsNullOrWhiteSpace(txtQuantity.Text) ||
        string.IsNullOrWhiteSpace(txtPrice.Text))
    {
      ShowAlert("Please fill in all required fields.");
      return false;
    }
    return true;
  }

  private void ShowAlert(string message)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", message.Replace("'", "\\'")), true);
  }
}
