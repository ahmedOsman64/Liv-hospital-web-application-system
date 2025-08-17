// Rooms.aspx.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Rooms : System.Web.UI.Page
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
      FillRooms();
      ClearForm();
    }
  }

  void FillRooms()
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("SELECT * FROM Rooms", con);
      da = new SqlDataAdapter(cmd);
      DataTable dt = new DataTable();
      da.Fill(dt);

      gvRooms.DataSource = dt;
      gvRooms.DataBind();
    }
    catch (Exception ex)
    {
      ShowAlert("Error loading rooms: " + ex.Message);
    }
    finally
    {
      if (con.State == ConnectionState.Open)
        con.Close();
    }
  }

  protected void btnSaveRoom_Click(object sender, EventArgs e)
  {
    try
    {
      string roomNumber = txtRoomNumber.Text.Trim();
      string roomType = cmbRoomType.SelectedValue;

      // Validation: Check for empty fields
      if (string.IsNullOrWhiteSpace(roomNumber) || string.IsNullOrEmpty(roomType))
      {
        ShowAlert("Please fill all required fields.");
        return;
      }

      if (con.State == ConnectionState.Closed)
        con.Open();

      // Optional: Prevent duplicate RoomNumber
      SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Rooms WHERE RoomNumber = @RoomNumber" +
                                           (btnSaveRoom.Text == "Update" ? " AND RoomID != @RoomID" : ""), con);
      checkCmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
      if (btnSaveRoom.Text == "Update")
      {
        checkCmd.Parameters.AddWithValue("@RoomID", ViewState["RoomID"]);
      }

      int count = (int)checkCmd.ExecuteScalar();
      if (count > 0)
      {
        ShowAlert("A room with this number already exists.");
        return;
      }

      // Insert or Update
      if (btnSaveRoom.Text == "Save")
      {
        cmd = new SqlCommand("INSERT INTO Rooms (RoomNumber, RoomType) VALUES (@RoomNumber, @RoomType)", con);
      }
      else
      {
        cmd = new SqlCommand("UPDATE Rooms SET RoomNumber = @RoomNumber, RoomType = @RoomType WHERE RoomID = @RoomID", con);
        cmd.Parameters.AddWithValue("@RoomID", ViewState["RoomID"]);
      }

      cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
      cmd.Parameters.AddWithValue("@RoomType", roomType);

      int result = cmd.ExecuteNonQuery();
      if (result > 0)
      {
        ShowAlert(btnSaveRoom.Text == "Save" ? "Room added successfully!" : "Room updated successfully!");
        ClearForm();
        FillRooms();
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

  protected void gvRooms_SelectedIndexChanged(object sender, EventArgs e)
  {
    string id = gvRooms.SelectedDataKey.Value.ToString();
    ViewState["RoomID"] = id;

    if (con.State == ConnectionState.Closed)
      con.Open();

    cmd = new SqlCommand("SELECT * FROM Rooms WHERE RoomID = @RoomID", con);
    cmd.Parameters.AddWithValue("@RoomID", id);

    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      txtRoomNumber.Text = dr["RoomNumber"].ToString();
      cmbRoomType.SelectedValue = dr["RoomType"].ToString();
      btnSaveRoom.Text = "Update";
    }
    dr.Close();
    con.Close();
  }

  private void ClearForm()
  {
    txtRoomNumber.Text = "";
    cmbRoomType.SelectedIndex = 0;
    btnSaveRoom.Text = "Save";
    ViewState["RoomID"] = null;
  }

  private void ShowAlert(string message)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", message.Replace("'", "\\'")), true);
  }
}
