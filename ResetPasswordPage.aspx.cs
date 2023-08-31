using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using DoctorAppointment.Model;
using System.Net.Mail;


namespace DoctorAppointment
{
    public partial class ResetPasswordPage : System.Web.UI.Page
    {

        readonly string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string emailAddress = TextBox1.Text;
            emailAddress = emailAddress.Trim();

            if(emailAddress.Length == 0 )
            {
                Response.Write("<script>alert('Please provide your email address to reset password!')</script>");
                return;
            }

            User userExists = GetUserByEmail(emailAddress);

            if (userExists == null)
            {
                Response.Write("<script>alert('Member with the provided email address does not exist!')</script>");
                return;
            }

            // create a GUID for the link and add it to database

            string MyGUID = Guid.NewGuid().ToString();

            // add guid with id to the new table
            bool addGUID = AddGUIDToTable(MyGUID, userExists.Id);

            if (addGUID)
            {
                string UrlLink = "https://localhost:44303/NewPasswordPage.aspx?Uid=" + MyGUID;

                string bodyText = "You can reset your password by clicking on the link : " + UrlLink;

                bool sendEmail = SendEmail("Password Reset", userExists.Email, bodyText);

                if (sendEmail)
                {
                    Response.Write("<script>alert('An Email is sent to your email address! Follow the instructions to reset your password!')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Error creating password reset request! Try again later.')</script>");
            }
        }

        private bool AddGUIDToTable(string Guid, int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string guidAddQuery = "insert into dbo.ease_password_reset (member_id,unique_code) values(" + id + ",'" + Guid + "');";

                SqlCommand cmd = new SqlCommand(guidAddQuery, con);
                
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
            catch(SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return false;
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
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return false;
            }
        }

        private User GetUserByEmail(string email)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string loginQuery = "select * from dbo.ease_members where email_address = '" + email + "';";

                SqlCommand cmd = new SqlCommand(loginQuery, con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Session["username"] = dr.GetValue(1).ToString();
                        Session["email"] = dr.GetValue(4).ToString();
                        Session["role"] = dr.GetValue(9).ToString();

                        return new User(Convert.ToInt32(dr.GetValue(0)), dr["name"].ToString(), dr["email_address"].ToString(), dr["password"].ToString(),
                            dr["contact"].ToString(), dr["address"].ToString(), dr["city"].ToString(), dr["state"].ToString(), dr["pin_code"].ToString(),
                            Convert.ToDateTime(dr["dob"].ToString()));
                    }
                }
                else
                {
                    return null;
                }
                con.Close();

            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return null;
            }

            return null;

        }
    }
}