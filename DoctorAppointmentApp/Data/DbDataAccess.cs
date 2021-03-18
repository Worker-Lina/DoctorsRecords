using DoctorAppointmentApp.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DoctorAppointmentApp.Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;


        public DbDataAccess()
        {
            factory = DbProviderFactories.GetFactory("RegistrationAppProvider");

            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationService.Configuration["dataAccessConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public abstract void Insert(T entity);
        public abstract ICollection<T> Select(T entity);
        public abstract void Update(T entity);

    }
}
