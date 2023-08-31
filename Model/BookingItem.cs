using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorAppointment.Model
{
    public class BookingItem
    {
        public int ID { get; set; }
        public DateTime BookingDateTime { get; set; }
        public string MemberEmailAddress { get; set; }
        public int ClinicLocationId { get; set; }
        public decimal BookingFee { get; set; }

        public BookingItem(int id, DateTime dateTime,string memberEmailAddress,int clinic, decimal fee)
        {
            ID = id;
            BookingDateTime = dateTime;
            BookingFee = fee;
            MemberEmailAddress = memberEmailAddress;
            ClinicLocationId = clinic;
        }

    }
}