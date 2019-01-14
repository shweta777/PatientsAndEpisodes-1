using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestApi.Models;

namespace RestApi.Interfaces
{
    public interface IPatientsService
    {
        IList<Patient> GetPatients();
        IList<Patient> GetPatientsById(int patientId);
    }
}
