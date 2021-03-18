using DoctorAppointmentApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentApp.Data
{
    public class RecordDataAccess : DbDataAccess<Record>
    {
        public override void Insert(Record record)
        {
            string insertSqlScript = "insert into Records values (@Id, @DoctorId, @UserId, @RecordDateTimeStart, @RecordDateTimeEnd)";

            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                var idParameter = factory.CreateParameter();
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = record.Id;
                idParameter.ParameterName = "Id";
                command.Parameters.Add(idParameter);

                var doctorIdParameter = factory.CreateParameter();
                doctorIdParameter.DbType = System.Data.DbType.Guid;
                doctorIdParameter.Value = record.DoctorId;
                doctorIdParameter.ParameterName = "DoctorId";
                command.Parameters.Add(doctorIdParameter);

                var userIdParameter = factory.CreateParameter();
                userIdParameter.DbType = System.Data.DbType.Guid;
                userIdParameter.Value = record.Id;
                userIdParameter.ParameterName = "UserId";
                command.Parameters.Add(userIdParameter);

                var recordDateTimeStartParameter = factory.CreateParameter();
                recordDateTimeStartParameter.DbType = System.Data.DbType.Time;
                recordDateTimeStartParameter.Value = record.RecordDateTimeStart;
                recordDateTimeStartParameter.ParameterName = "RecordDateTimeStart";
                command.Parameters.Add(recordDateTimeStartParameter);

                var recordDateTimeEndParameter = factory.CreateParameter();
                recordDateTimeEndParameter.DbType = System.Data.DbType.Time;
                recordDateTimeEndParameter.Value = record.RecordDateTimeEnd;
                recordDateTimeEndParameter.ParameterName = "RecordDateTimeEnd";
                command.Parameters.Add(recordDateTimeEndParameter);

                command.ExecuteNonQuery();
            }                                   
        }


        public override ICollection<Record> Select(Record entity)
        {
            var selectSqlScript = "select * from Records";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            var dataReader = command.ExecuteReader();

            var records = new List<Record>();

            /*while (dataReader.Read())
            {
                records.Add(new User
                {
                    Id = Guid.Parse(dataReader["Id"].ToString().ToString()),
                    
                });
            }*/

            dataReader.Close();
            command.Dispose();

            return records;
        }

        public override void Update(Record entity)
        {
            throw new NotImplementedException();
        }


        public ICollection<DayRecords> GetDayRecords(Guid dayId)
        {
            var selectSqlScript = $"select r.RecordDateTimeStart, r.RecordDateTimeEnd from DayRecords d join Records r on d.RecordsId = r.Id where DayName = '{dayId}'";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var dayRecords = new List<DayRecords>();

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        dayRecords.Add(new DayRecords
                        {
                            RecordsDateTimeStart = DateTime.Parse(dataReader["RecordDateTimeStart"].ToString()),
                            RecordsDateTimeEnd = DateTime.Parse(dataReader["RecordDateTimeEnd"].ToString()),
                        });
                    }
                    return dayRecords;
                }
            }
        }
    }
}
