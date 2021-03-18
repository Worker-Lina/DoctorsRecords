using DoctorAppointmentApp.Data;
using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorAppointmentApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var nowDay = new DateTime(2021, 3, 19, 8, 0, 0);
            var fullTimeToRecords = new List<DayRecords>
            {
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay,
                    RecordsDateTimeEnd = nowDay.AddMinutes(30)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(30),
                    RecordsDateTimeEnd = nowDay.AddMinutes(60)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(60),
                    RecordsDateTimeEnd = nowDay.AddMinutes(90)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(90),
                    RecordsDateTimeEnd = nowDay.AddMinutes(120)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(120),
                    RecordsDateTimeEnd = nowDay.AddMinutes(150)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(150),
                    RecordsDateTimeEnd = nowDay.AddMinutes(180)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(180),
                    RecordsDateTimeEnd = nowDay.AddMinutes(210)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(210),
                    RecordsDateTimeEnd = nowDay.AddMinutes(240)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(240),
                    RecordsDateTimeEnd = nowDay.AddMinutes(270)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(270),
                    RecordsDateTimeEnd = nowDay.AddMinutes(300)
                },
                new DayRecords
                {
                    RecordsDateTimeStart = nowDay.AddMinutes(300),
                    RecordsDateTimeEnd = nowDay.AddMinutes(330)
                },
            };

            InitConfiguration();
            var user = new User
            {
                Id = new Guid("6b017401-fd94-4710-8bf8-f9e0b40803a2"),
                Login = "alina12",
                Password = "1212",
                FullName = "Человечкова Алина Кадаржановна",
                IIN = "12345678987"
            };

            Console.WriteLine("1.понедельник\n2.вторник\n3.среда\n4.четверг\n5.пятница");
            Console.Write("Введите день недели для записи: ");
            var day = Console.ReadLine();
            Day dayId = new Day();
            switch (day)
            {
                case "1":
                    dayId.Id = new Guid("d0e91b3a-9ead-4c96-b69e-82b8420c8b4f");
                    break;

            }

            var dayRecords = new List<DayRecords>();
            using (var recordDataAccess = new RecordDataAccess())
            {
                dayRecords = recordDataAccess.GetDayRecords(dayId.Id).ToList();
            }

            Console.WriteLine("Занятое время");
            foreach (var rec in dayRecords)
            {
                fullTimeToRecords.Remove(rec);
            }

            var record = new Record();
            record.Id = Guid.NewGuid();
            record.UserId = user.Id;

            Console.WriteLine("Введите номер врача: ");
            var doctors = new List<Doctor>();
            int chetchik = 1;
            using (var doctorDataAccess = new DoctorDataAccess())
            {
                doctors = doctorDataAccess.SelectAllDoctors().ToList();
                foreach (var doctor in doctors)
                {
                    doctor.Specialization = doctorDataAccess.GetSpecialization(doctor.SpecializationId);
                    Console.WriteLine($"{chetchik++}. {doctor.FullName} {doctor.Specialization.Specialty}");
                }
            }

            var doctorNum = int.Parse(Console.ReadLine());
            switch (doctorNum)
            {
                case 1:
                    record.DoctorId = doctors[doctorNum - 1].Id;
                    break;
            }



            Console.WriteLine("Введите время для записи");
            int i = 1;
            foreach(var dayrecord in fullTimeToRecords)
            {
                Console.WriteLine($"{i++}. {dayrecord.RecordsDateTimeStart.TimeOfDay} | {dayrecord.RecordsDateTimeEnd.TimeOfDay}");
            }
            var timeToRecords = int.Parse(Console.ReadLine());
            switch (timeToRecords)
            {
                case 1:
                    record.RecordDateTimeStart = fullTimeToRecords[timeToRecords - 1].RecordsDateTimeStart;
                    record.RecordDateTimeEnd = fullTimeToRecords[timeToRecords - 1].RecordsDateTimeEnd;
                    break;
            }

            using(var recorddata=new RecordDataAccess())
            {
                recorddata.Insert(record);
            }

        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
