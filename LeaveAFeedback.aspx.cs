using DoctorAppointment.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoctorAppointment
{
    public partial class LeaveAFeedback : System.Web.UI.Page
    {
        string strConnection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllFeedbacks();
            }
        }

        private void GetAllFeedbacks()
        {
            try
            {

                SqlConnection con = new SqlConnection(strConnection);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string loginQuery = "select * from dbo.ease_feedbacks;";

                SqlCommand cmd = new SqlCommand(loginQuery, con);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Feedback> feedbacks = new List<Feedback>();

                if(dr.HasRows)
                {
                    while (dr.Read())
                    {
                        feedbacks.Add(new Feedback(Convert.ToInt32(dr["feedback_id"].ToString()), dr["member_email"].ToString(), dr["feedback_content"].ToString()));
                    }
                }

                GridView1.DataSource = feedbacks;
                GridView1.DataBind();

                con.Close();

            }
            catch(SqlException sqlEx)
            {
                Response.Write("<script>alert('" + sqlEx.Message + "')</script>");
            }

        }

        private bool AddFeedBack(string emailAddres, string feedback)
        {
            try
            {

                SqlConnection con = new SqlConnection(strConnection);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string insertFeedback = "insert into dbo.ease_feedbacks (member_email, feedback_content) values('" + emailAddres + "','" + feedback + "')";

                SqlCommand cmd = new SqlCommand(insertFeedback, con);

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
            catch (SqlException sqlEx)
            {
                Response.Write("<script>alert('" + sqlEx.Message + "')</script>");
            }

            return false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string feedBackContent = TextBox1.Text;
            string email = (string)Session["email"] as string;

            if(feedBackContent == "" || email == "")
            {
                Response.Write("<script>alert('Please write feedback to submit!')</script>");
                return;
            }

            bool addFeedback = AddFeedBack(email,feedBackContent);

            if(addFeedback)
            {
                Response.Write("<script>alert('Feedback added!!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Failed to submit feedback!')</script>");
            }
        }
    }
}