using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoctorAppointment
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string role = (string) Session["role"] as string;
                if (role != null)
                {
                    role = role.Trim();
                }

                if (string.IsNullOrEmpty(role))
                {
                    // user's booking
                    LinkButton4.Visible = false;

                    // login button
                    LinkButton1.Visible = true;

                    // signup button
                    LinkButton2.Visible = true;

                    // logout button
                    LinkButton3.Visible = false;
                    
                    // logout button
                    LinkButton6.Visible = false;
                    // manage booking button
                    LinkButton9.Visible = false;

                    // my boooking button
                    LinkButton8.Visible = false;

                    // logged in user name
                    LinkButton7.Visible = false;


                    LinkButton10.Visible = false;

                    // admin booking management page
                    LinkButton11.Visible = false;

                    // feedback button
                    LinkButton12.Visible = false;

                }
                else if (role.Equals("user"))
                {

                    LinkButton4.Visible = true;
                    LinkButton8.Visible = true;
                    LinkButton9.Visible = false;
                    LinkButton1.Visible = false;
                    LinkButton7.Visible = true;
                    LinkButton7.Text = "Hello, " + Session["username"];

                    // sign up button
                    LinkButton2.Visible = false;

                    // logout button
                    LinkButton3.Visible = true;

                    // logout button
                    LinkButton6.Visible = true;

                    // book appointment button
                    LinkButton10.Visible = true;

                    // admin booking management page
                    LinkButton11.Visible = false;

                    // feedback on
                    LinkButton12.Visible = true;


                }
                else if (role.Equals("admin"))
                {
                    LinkButton4.Visible = false;

                    // manage bookings button for admin
                    LinkButton9.Visible = true;
                    // login button
                    LinkButton1.Visible = false;
                    // welcome message button
                    LinkButton7.Visible = true;
                    LinkButton7.Text = "Hello, Admin";

                    // logout button
                    LinkButton3.Visible = true;
                    // logout button
                    LinkButton6.Visible = true;

                    // sign up button
                    LinkButton2.Visible = false;

                    LinkButton10.Visible = false;

                    // admin booking management
                    LinkButton11.Visible = true;

                    // feedback on
                    LinkButton12.Visible = true;
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUpPage.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyBookings.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookingManagement.aspx");
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["email"] = "";
            Session["role"] = "";

            // login button
            LinkButton1.Visible = true;

            // signup button
            LinkButton2.Visible = true;

            // logout button
            LinkButton3.Visible = false;

            // logout button
            LinkButton6.Visible = false;
            // manage booking button
            LinkButton9.Visible = false;

            // my boooking button
            LinkButton8.Visible = false;

            // logged in user name
            LinkButton7.Visible = false;

            // user's booking
            LinkButton4.Visible = false;

            LinkButton11.Visible = false;

            // feedback on
            LinkButton12.Visible = false;

            Response.Redirect("HomePage.aspx");

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["email"] = "";
            Session["role"] = "";

            // login button
            LinkButton1.Visible = true;

            // signup button
            LinkButton2.Visible = true;

            // logout button
            LinkButton3.Visible = false;

            // logout button
            LinkButton6.Visible = false;
            // manage booking button
            LinkButton9.Visible = false;

            // my boooking button
            LinkButton8.Visible = false;

            // logged in user name
            LinkButton7.Visible = false;

            // book appointment
            LinkButton10.Visible = false;

            // user's booking
            LinkButton4.Visible = false;

            LinkButton11.Visible = false;

            // feedback on
            LinkButton12.Visible = false;

            Response.Redirect("HomePage.aspx");

        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookAppointmentPage.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyBookings.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookingManagement.aspx");
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveAFeedback.aspx");
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminFeedbackView.aspx");
        }
    }
}