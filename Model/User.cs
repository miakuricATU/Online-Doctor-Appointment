using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorAppointment.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address{ get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public DateTime DOB { get; set; }

        public User(int id, string name, string email, string password, string phone, string address, string city, string state, string postalCode, DateTime dOB)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            DOB = dOB;
        }
    }
}