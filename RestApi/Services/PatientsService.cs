using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestApi.Interfaces;
using RestApi.Models;

namespace RestApi.Services
{
    public class PatientsService : IPatientsService
    {
        private IDatabaseContext databaseContext;
        public PatientsService(IDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IList<Patient> GetPatients()
        {
            return this.databaseContext.Patients.ToList();
        }

        public IList<Patient> GetPatientsById(int patientId)
        {
            if (patientId != int.MinValue)
            {
                var patient = this.databaseContext.Patients.Where(x => x.PatientId == patientId).ToList();
                if (patient.Any())
                {
                    var episodes = this.databaseContext.Episodes.Where(x => x.PatientId == patientId).ToList();
                    patient.ForEach(x => x.Episodes = episodes);
                    return patient;
                }
            }

            return null;
        }
    }
}
