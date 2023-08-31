using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoctorAppointment
{
    public partial class NewPasswordPage : System.Web.UI.Page
    {
        string strConnection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        int Uid;
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            using(SqlConnection con = new SqlConnection(strConnection))
            {
                string GUIDValue = Request.QueryString["Uid"].ToString();
                
                if(GUIDValue != null)
                {
                    string query = "select * from dbo.ease_password_reset where unique_code = '" + GUIDValue + "';";

                    SqlCommand cmd = new SqlCommand(query,con);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    sda.Fill(dt);

                    if(dt.Rows.Count != 0)
                    {
                        Uid = Convert.ToInt32(dt.Rows[0][1]);
                    }
                    else
                    {
                        Response.Write("<script>alert('Password reset link is expired!')</script>");
                    }

                }
                else
                {
                    Response.Redirect("ResetPasswordPage.aspx");
                }
            }

            if (!IsPostBack)
            {
                if(dt.Rows.Count != 0)
                {
                    TextBox2.Visible = true;
                    TextBox3.Visible = true;
                    Button2.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('Password reset link is expired or invalid!')</script>");
                }
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            string password = TextBox2.Text;
            string confirmPass = TextBox3.Text;

            if(password != "" && confirmPass != "" && password == confirmPass)
            {
                using(SqlConnection con = new SqlConnection(strConnection))
                {
                    string updatePassword = "update dbo.ease_members SET password = '" + password + "' where member_id = " + Uid + ";";

                    SqlCommand cmd = new SqlCommand(updatePassword,con);
                    
                    if(con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd.ExecuteNonQuery();

                    string deletePin = "delete from dbo.ease_password_reset where member_id = " + Uid;
                    
                    SqlCommand cmd2 = new SqlCommand(deletePin,con);

                    cmd2.ExecuteNonQuery();

                    Response.Write("<script>alert('Password changed successfully!')</script>");

                }
            }
        }
    }
}