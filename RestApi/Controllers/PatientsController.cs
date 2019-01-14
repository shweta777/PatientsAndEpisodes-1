using RestApi.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;

namespace RestApi.Controllers
{
    public class PatientsController : ApiController
    {

        private readonly IPatientsService patientsService;
        private readonly IEpisodesService episodesService;
        public PatientsController(IPatientsService patientsService, IEpisodesService episodesService)
        {
            this.patientsService = patientsService;
            this.episodesService = episodesService;
        }
        
        [HttpGet]
        public HttpResponseMessage Get(int patientId)
        {    
            if (patientId != int.MinValue)
            {
                try
                {
                    var patients = this.patientsService.GetPatients();
                    var patient = patients.Where(p => p.PatientId == patientId).FirstOrDefault();
                    if (patient!=null)
                    {
                        patient.Episodes = episodesService.GetEpisodes().Where(e => e.PatientId == patient.PatientId).ToList();
                        return Request.CreateResponse(HttpStatusCode.OK, patient);
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Patient not found with patientId :{ patientId }.");                    
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                } 
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        } 
    }
}