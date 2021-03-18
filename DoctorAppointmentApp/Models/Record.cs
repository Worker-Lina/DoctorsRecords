using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentApp.Models
{
    public class Record : Entity
    {
        public Guid DoctorId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RecordDateTimeStart { get; set; }
        public DateTime RecordDateTimeEnd { get; set; }

    }
}
