using DoctorAppointment.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace DoctorAppointment
{
    public partial class AdminBookingManagement : System.Web.UI.Page
    {
        readonly string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox5.Text = "70 €";
            TextBox1.Attributes["min"] = "0";
            if (!IsPostBack)
            {
                AddDataToGridView();
                GetAllClinicsData();
                FillDates();
                GetAllMembers();
                CreateSlots();
            }
        }

        private void GetAllMembers()
        {
            try
            {
                if (DropDownList4.Items.Count == 3)
                {
                    return;
                }

                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "select * from dbo.ease_members where role = 'user';";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList4.Items.Add(new ListItem(dr["email_address"].ToString(), dr["email_address"].ToString()));
                    }
                }
                
                con.Close();
            }
            catch(SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        private void GetAllClinicsData()
        {
            try
            {
                if (DropDownList1.Items.Count == 3)
                {
                    return;
                }

                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "select * from ease_clinics;";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DropDownList1.Items.Add(new ListItem(dr["location_name"].ToString(), dr["clinic_location_id"].ToString()));
                        Console.WriteLine(dr["location_name"].ToString() + " : " + dr["clinic_location_id"].ToString());
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credentials!')</script>");
                }

                con.Close();

            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }

        private void CreateSlots()
        {
            DropDownList3.Items.Clear();

            DateTime startTime = DateTime.Today.AddHours(10); // 10 am
            DateTime endTime = DateTime.Today.AddHours(19);  // 7 pm

            List<DateTime> availableSlots = new List<DateTime>();

            while (startTime <= endTime)
            {
                availableSlots.Add(startTime);
                startTime = startTime.AddHours(1); 
            }

            foreach (DateTime slot in availableSlots)
            {
                string slotText = slot.ToString("hh:mm tt");
                DropDownList3.Items.Add(new ListItem(slotText, slotText));
            }
        }

        private void FillDates()
        {
            if (DropDownList2.Items.Count == 7)
            {
                return;
            }
            else
            {
                // Clear existing items
                DropDownList2.Items.Clear();

                // Get the current date
                DateTime currentDate = DateTime.Now; // Set the time part to 00:00:00

                TimeSpan cutOffToday = new TimeSpan(19, 0, 0);

                if (currentDate.TimeOfDay > cutOffToday)
                {
                    currentDate = currentDate.AddDays(1);
                }

                // Add next day's date for the specified number of days
                for (int i = 0; i < 7; i++)
                {
                    DropDownList2.Items.Add(new ListItem(currentDate.ToString("yyyy-MM-dd"), currentDate.ToString("yyyy-MM-dd")));
                    currentDate = currentDate.AddDays(1); // Add one day
                }
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

                DateTime todayDate = DateTime.Now;
                string insertQuery = "select * from dbo.ease_bookings;";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                con.Close();

                List<BookingItem> MyBookings = new List<BookingItem>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MyBookings.Add(new BookingItem(Convert.ToInt32(dr["booking_id"].ToString()), Convert.ToDateTime(dr["date"].ToString()), dr["member_email"].ToString(), Convert.ToInt32(dr["clinic_location"]), Convert.ToDecimal(dr["booking_fee"])));
                    }
                }

                GridView1.DataSource = MyBookings;
                GridView1.DataBind();

            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        private bool CheckBooking(int clinicID, DateTime dateTime)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string checkBooking = "select * from dbo.ease_bookings where date = '" + dateTime + "' AND clinic_location = " + clinicID + ";";

                SqlCommand cmd = new SqlCommand(checkBooking, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count >= 1)
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
            }

            return false;
        }

        private bool CheckBooking(int bookingID)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string checkBooking = "select * from dbo.ease_bookings where booking_id = " + bookingID;

                SqlCommand cmd = new SqlCommand(checkBooking, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count >= 1)
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
            }

            return false;
        }

        private bool CheckMemberAvailability(string email, DateTime bookingDateAndTime)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string checkBooking = "select * from dbo.ease_bookings where date = '" + bookingDateAndTime + "' AND member_email = '" + email+ "';";

                SqlCommand cmd = new SqlCommand(checkBooking, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count >= 1)
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
            }

            return false;
        }

        private bool CreateBooking(string email, int clinicId, DateTime bookingDateAndTime, decimal fee)
        {

            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string checkBooking = "insert into dbo.ease_bookings (date,clinic_location,member_email,booking_fee) " +
                    "values ('" + bookingDateAndTime + "'," + clinicId + ",'" + email + "'," + fee + ");";

                SqlCommand cmd = new SqlCommand(checkBooking, con);
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
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
            }

            return false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // get the user's email address - from session
            string email = DropDownList4.SelectedValue;
            // get clinic id
            int clinicID = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            // get date
            DateTime dateTime = Convert.ToDateTime(DropDownList2.SelectedValue.ToString());

            DateTime dateTimeNow = DateTime.Now;

            if (DateTime.Compare(dateTime, dateTimeNow) <= 0)
            {
                Response.Write("<script>alert('Please select a date and time slot from the future time (i.e next available time...)!')</script>");
                return;
            }

            // get time slot
            string slot = DropDownList3.SelectedItem.Text.ToString();
            // calculate fee...how ?
            decimal fee = 70;

            // check for existing booking
            DateTime timeSlot = DateTime.ParseExact(slot, "hh:mm tt", null);
            DateTime bookingDateAndTime = dateTime.Date + timeSlot.TimeOfDay;

            bool dateExists = CheckBooking(clinicID, bookingDateAndTime);

            if (dateExists)
            {
                Response.Write("<script>alert('Booking Slot is not available! Try another slot!')</script>");
                return;
            }
            else
            {

                // check if member is available
                bool memberAvailable = CheckMemberAvailability(email, bookingDateAndTime);

                if (memberAvailable)
                {
                    Response.Write("<script>alert('Member is not available for this meeting!')</script>");
                    return;
                }
                else
                {
                    bool bookingConfirmation = CreateBooking(email, clinicID, bookingDateAndTime, fee);
                    if (bookingConfirmation)
                    {
                        string bodyText = "Your appointment is confirmed at the time: " + bookingDateAndTime;
                        bool emailConfirmation = SendEmail("Booking Confirmation - Admin Ease Appointments", email, bodyText);
                        if (emailConfirmation)
                        {
                            Response.Write("<script>alert('Booking is confirmed! Email is sent to the member on their Email Address!')</script>");
                        }
                    }
                }
            }
        }

        private bool SendEmail(string subject, string email, string body)
        {
            // Create a new MimeMessage
            try
            {

                string fromEmail = "girlsleapmanagement@gmail.com";
                string toEmail = email;
                string subjectEmail = subject;
                string bodyEmail = body;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("girlsleapmanagement@gmail.com", "tnsnfcgjdxywrydf"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, bodyEmail)
                {
                    IsBodyHtml = true
                };

                smtpClient.Send(mailMessage);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string ID = TextBox1.Text;

            if(ID.Length == 0)
            {
                Response.Write("<script>alert('Please provide and ID to update!')</script>");
                return;
            }

            int enteredID = Convert.ToInt32(TextBox1.Text);

            if(enteredID == 0)
            {
                Response.Write("<script>alert('Please provide and ID to update!')</script>");
                return;
            }

            // ID is valid, check if it exists in database
            if(!CheckBooking(enteredID))
            {
                Response.Write("<script>alert('Invalid ID provided!')</script>");
                return;
            }

            // get the user's email address - from session
            string email = DropDownList4.SelectedValue;
            // get clinic id
            int clinicID = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            // get date
            DateTime dateTime = Convert.ToDateTime(DropDownList2.SelectedValue.ToString());

            DateTime dateTimeNow = DateTime.Now;

            if (DateTime.Compare(dateTime, dateTimeNow) <= 0)
            {
                Response.Write("<script>alert('Please select a date and time slot from the future time (i.e next available time...)!')</script>");
                return;
            }

            // get time slot
            string slot = DropDownList3.SelectedItem.Text.ToString();

            // check for existing booking
            DateTime timeSlot = DateTime.ParseExact(slot, "hh:mm tt", null);
            DateTime bookingDateAndTime = dateTime.Date + timeSlot.TimeOfDay;

            bool dateExists = CheckBooking(clinicID, bookingDateAndTime);

            if (dateExists)
            {
                Response.Write("<script>alert('Booking Slot is not available! Try another slot!')</script>");
                return;
            }
            else
            {
                // check if member is available
                bool memberAvailable = CheckMemberAvailability(email, bookingDateAndTime);

                if (memberAvailable)
                {
                    Response.Write("<script>alert('Member is not available for this appointment!')</script>");
                    return;
                }
                else
                {
                    bool updateBooking = UpdateBooking(enteredID,email,bookingDateAndTime,clinicID);
                    if (updateBooking)
                    {
                        Response.Write("<script>alert('Booking Updated Successfully!')</script>");
                    }
                }
            }
        }

        private bool UpdateBooking(int id, string email, DateTime bookingDateTime, int clinicID)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string checkBooking = "update dbo.ease_bookings SET member_email = '" + email + "', clinic_location = " + clinicID + ", date = '" + bookingDateTime + "'" +
                    " where booking_id = " + id;

                SqlCommand cmd = new SqlCommand(checkBooking, con);

                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
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
            }

            return false;

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
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}