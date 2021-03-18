using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentApp.Models
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string IIN { get; set; }
    }
}
