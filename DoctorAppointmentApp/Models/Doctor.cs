using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentApp.Models
{
    public class Doctor : Entity
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CabinetNumber { get; set; }

        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
