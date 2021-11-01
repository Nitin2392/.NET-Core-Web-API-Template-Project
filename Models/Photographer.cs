using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Models
{
    public class Photographer
    {
        public int Id { get; set; }
        public string UID { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        public string Phone_Number { get; set; }
        public string Social_Insurance_Number { get; set; }
        public string Date_Of_Birth { get; set; }
        public EventType Event_Type { get; set; }
        public Address Address { get; set; }
        public CreditCardNumber Credit_Card { get; set; }
        public Subscription Subscription { get; set; }
    }

    public class EventType
    {
        public List<string> Type { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Street_Name { get; set; }
        public string Street_Address { get; set; }
        public string Zip_Code { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class CreditCardNumber
    {
        public string CC_Number { get; set; }
    }

    public class Subscription
    {
        public string Plan { get; set; }
        public string Status { get; set; }
        public string Payment_Method { get; set; }
        public string Term { get; set; }
    }
}
