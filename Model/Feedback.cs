namespace DoctorAppointment.Model
{
    public class Feedback
    {

        public int ID { get; set; }
        public string MemberEmail { get; set; }
        public string FeedbackContent { get; set; }

        public Feedback(int iD, string memberEmail, string feedbackContent)
        {
            ID = iD;
            MemberEmail = memberEmail;
            FeedbackContent = feedbackContent;
        }
    }
}