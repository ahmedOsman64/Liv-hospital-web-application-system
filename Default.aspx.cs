
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class _Default : System.Web.UI.Page
{
  DBConnection objcon = new DBConnection();
  SqlConnection con;
  SqlCommand cmd;
  SqlDataReader dr;
  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      con = objcon.GetConnection(); // Ensure GetConnection() returns a valid SqlConnection

    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('Database Connection Error: " + ex.Message + "');</script>");
    }

  }

  protected void btnLogin_Click(object sender, EventArgs e)
  {
    try
    {
      if (con.State == ConnectionState.Closed)
        con.Open();
      cmd = new SqlCommand("select * from users where phone='" + txtUsername.Text + "' and Password='" + txtPassword.Text + "'", con);
      dr = cmd.ExecuteReader();
      if (dr.Read())
      {
        string st = dr.GetValue(4).ToString();
        string fullname = dr.GetValue(1).ToString();
        int userid = Convert.ToInt32(dr.GetValue(0));

        dr.Close();
        if (st != "Active")
        {
          Response.Write("<Script>alert('Your Account has been blocked');</script>");
          return;
        }
        Session["userid"] = userid.ToString();
        Session["fullname"] = fullname;

        Response.Redirect("Dashboard.aspx", false);

      }
      else
      {
        dr.Close();
        Response.Write("<Script>alert('Invalid Attempt, Try Better Again');</script>");
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('Database Connection Error: " + ex.Message + "');</script>");
    }
  }
}


