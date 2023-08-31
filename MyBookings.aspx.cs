using DoctorAppointment.Model;
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
    public partial class MyBookings : System.Web.UI.Page
    {

        readonly string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddDataToGridView();
            }
        }

        private void AddDataToGridView()
        {
            try
            {

                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string email = (string) Session["email"] as string;

                if(email == null || email.Length == 0)
                {
                    return;
                }

                DateTime todayDate = DateTime.Now;
                string insertQuery = "select * from dbo.ease_bookings where " +
                    "member_email = '" + email.Trim() + "' AND date >= CONVERT(DATE,GETDATE());";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();

                List<BookingItem> MyBookings = new List<BookingItem>();

                if(dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MyBookings.Add(new BookingItem(Convert.ToInt32(dr["booking_id"].ToString()), Convert.ToDateTime(dr["date"].ToString()), dr["member_email"].ToString(), Convert.ToInt32(dr["clinic_location"]), Convert.ToDecimal(dr["booking_fee"])));
                    }
                }

                GridView1.DataSource = MyBookings;
                GridView1.DataBind();

            }
            catch(SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }
        
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int delBookingID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

                if (delBookingID == 0)
                {
                    return;
                }

                // delete from database
                string cancelBooking = "delete from dbo.ease_bookings where booking_id = " + delBookingID;
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(cancelBooking, con);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    AddDataToGridView();
                    Response.Write("<script>alert('Booking Cancelled Successfully!')</script>");
                }

                con.Close();
            }
            catch(SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }

       
    }
}