using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DoctorAppointment
{
    public partial class LoginPage : System.Web.UI.Page
    {
        string strConnection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // User Login
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strConnection);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string loginQuery = "select * from dbo.ease_members where email_address = '" + TextBox1.Text.Trim() + "' AND password = '" + TextBox2.Text.Trim() + "'";

                SqlCommand cmd = new SqlCommand(loginQuery, con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Write("<script>alert('Login Successful! as: " + dr.GetValue(1).ToString() + " ')</script>");
                        // adding a session variable for role, email, and name
                        Session["username"] = dr.GetValue(1).ToString();
                        Session["email"] = dr.GetValue(4).ToString();
                        Session["role"] = dr.GetValue(9).ToString();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credentials!')</script>");
                }

                con.Close();

                Response.Redirect("HomePage.aspx");

            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }

        // User Function
    }
}