using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace DoctorAppointment
{

    public partial class SignUpPage : Page
    {

        // connection string
        readonly string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // signup button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                Response.Write("<script>alert('Member already exists with the given Email Address!')</script>");
            }
            else
            {
                signUpUser();
            }
        }

        private bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string insertQuery = "select * from dbo.ease_members where email_address = '" + TextBox4.Text.Trim() + "';";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();

                Console.WriteLine(dt.Rows.Count);

                if(dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void signUpUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                string insertQuery = "INSERT INTO dbo.ease_members (name,dob,contact,email_address,password,state,city,address,role,pin_code) " +
                    "VALUES (@full_name,@dob,@contact,@email,@password,@state,@city,@address,@role,@pin_code)";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@state", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pin_code", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@address", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@role", "user");


                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Sign Up Successfull! You can now login and Book appointments!')</script>");

            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
        
    }
}