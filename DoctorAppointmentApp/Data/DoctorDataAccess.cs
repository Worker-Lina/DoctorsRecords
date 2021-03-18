using DoctorAppointmentApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentApp.Data
{
    public class DoctorDataAccess : DbDataAccess<Doctor>
    {
        public override void Insert(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public override ICollection<Doctor> Select(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Doctor> SelectAllDoctors()
        {
            var selectSqlScript = "Select * from doctors";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var doctors = new List<Doctor>();

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        doctors.Add(new Doctor
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            FullName = dataReader["FullName"].ToString(),
                            Phone = dataReader["Phone"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            SpecializationId = Guid.Parse(dataReader["SpecializationId"].ToString()),
                            ScheduleId = Guid.Parse(dataReader["ScheduleId"].ToString())
                        });
                    }

                    return doctors;
                }
            }
        }

        public Specialization GetSpecialization(Guid doctorId)
        {
            var selectSqlScript = $"select * from Specialization where id ='{doctorId}'";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var specialization = new Specialization();

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        specialization = new Specialization
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            Specialty = dataReader["Specialty"].ToString(),
                        };
                    }
                }
                return specialization;
            }
        }


        public override void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
