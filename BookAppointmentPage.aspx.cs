using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using DoctorAppointment.Model;
using System.Net;
using System.Net.Mail;


namespace DoctorAppointment
{
    public partial class BookAppointmentPage : Page
    {

        readonly string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox4.Text = "70 €";
            if (!IsPostBack)
            {
                // get 3 clinics details
                GetAllClinicsData();
                GetClinicSlots();
                // add next 7 days date in the dropdown
                FillDates();
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
            catch(Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        private void FillDates()
        {
            if(DropDownList3.Items.Count == 7)
            {
                return;
            }
            else
            {
                // Clear existing items
                DropDownList3.Items.Clear();

                // Get the current date
                DateTime currentDate = DateTime.Now; // Set the time part to 00:00:00

                TimeSpan cutOffToday = new TimeSpan(19, 0, 0);

                if(currentDate.TimeOfDay > cutOffToday)
                {
                    currentDate = currentDate.AddDays(1);
                }

                // Add next day's date for the specified number of days
                for (int i = 0; i < 7; i++)
                {
                    DropDownList3.Items.Add(new ListItem(currentDate.ToString("yyyy-MM-dd"), currentDate.ToString("yyyy-MM-dd")));
                    currentDate = currentDate.AddDays(1); // Add one day
                }
            }
        }

        private void GetAllClinicsData()
        {
            try
            {
                if(DropDownList1.Items.Count == 3)
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

        private void GetClinicSlots()
        {

            try
            {
                if(DropDownList1.SelectedValue != "" && DropDownList1.SelectedValue != null)
                {
                    int clinicID = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

                    SqlConnection con = new SqlConnection(strCon);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "select * from ease_bookings where clinic_location = " + clinicID + " AND CAST(date as DATE) = '" + DropDownList3.SelectedValue + "';";

                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    int rows = dt.Rows.Count;

                    List<BookingItem> list = new List<BookingItem>();

                    if(dt.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            list.Add(new BookingItem(Convert.ToInt32(dr["booking_id"].ToString()), Convert.ToDateTime(dr["date"].ToString()), dr["member_email"].ToString(), Convert.ToInt32(dr["clinic_location"]), Convert.ToDecimal(dr["booking_fee"])));
                        }
                    }

                    CreateSlots();

                    con.Close();
                }
            }
            catch(SqlException  ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        private void CreateSlots()
        {
            DropDownList2.Items.Clear();

            DateTime startTime = DateTime.Today.AddHours(10); // 10 am
            DateTime endTime = DateTime.Today.AddHours(19);  // 7 pm

            // all slots
            List<DateTime> availableSlots = new List<DateTime>();

            // created 10 slots with difference of 1 hour
            while (startTime <= endTime)
            {
                availableSlots.Add(startTime);
                startTime = startTime.AddHours(1); // Add 1 hour
            }

            foreach(DateTime slot in availableSlots)
            {
                string slotText = slot.ToString("hh:mm tt");
                DropDownList2.Items.Add(new ListItem(slotText, slotText));
            }
        }

        private bool CreateBooking(string email, int clinicId,  DateTime bookingDateAndTime, decimal fee)
        {

            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string checkBooking = "insert into dbo.ease_bookings (date,clinic_location,member_email,booking_fee) " +
                    "values ('"+ bookingDateAndTime +"',"+clinicId+",'"+email+"',"+fee+");";

                SqlCommand cmd = new SqlCommand(checkBooking, con);
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if(i > 0)
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

                if(dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

            return false;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetClinicSlots();
        }

        protected void SignUpButton_Click(object sender, EventArgs e)
        {
            // get the user's email address - from session
            string email = (string) Session["email"] as string;
            // get clinic id
            int clinicID = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            // get date
            DateTime dateTime = Convert.ToDateTime(DropDownList3.SelectedValue.ToString());

            DateTime dateTimeNow = DateTime.Now;

            if (DateTime.Compare(dateTime, dateTimeNow) <= 0)
            {
                Response.Write("<script>alert('Please select a date and time slot from the future time (i.e next available time...)!')</script>");
                return;
            }

            // get time slot
            string slot = DropDownList2.SelectedItem.Text.ToString();
            // calculate fee...how ?
            decimal fee = 70;

            // check for existing booking
            DateTime timeSlot = DateTime.ParseExact(slot, "hh:mm tt", null);
            DateTime bookingDateAndTime = dateTime.Date + timeSlot.TimeOfDay;

            bool dateExists = CheckBooking(clinicID, bookingDateAndTime);

            if (dateExists)
            {
                Response.Write("<script>alert('Booking Slot is not available! Try another slot!')</script>");
            }
            else
            {
                bool bookingCreated = CreateBooking(email, clinicID, bookingDateAndTime, fee);
                if (bookingCreated)
                {
                    // send email
                    string bodyText = "Your appointment is confirmed at the time: " + bookingDateAndTime;

                    bool successEmail = SendEmail("Successfull Booking",email,bodyText);
                    if(successEmail)
                    {
                        Response.Write("<script>alert('Booking is complete! An email is sent for your confirmation!!')</script>");
                    }
                }
            }
        }
        
    }
}