using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Moq;
using RestApi.Controllers;
using RestApi.Interfaces;
using RestApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestApi.UnitTests
{
    [TestClass]
    public class GetPatientTests
    {
        private Mock<IPatientsService> patientsService;
        private Mock<IEpisodesService> episodesService;
        private PatientsController patientsController;
        

        [TestMethod]
        public void GetPatientsTest()
        {
            episodesService = new Mock<IEpisodesService>();
            episodesService.Setup(service => service.GetEpisodes()).Returns(new List<Episode>()
            {
                new Episode
                {
                    AdmissionDate = new DateTime(2014, 11, 12),
                    Diagnosis = "Irritation of inner ear",
                    DischargeDate = new DateTime(2014, 11, 27),
                    EpisodeId = 1,
                    PatientId = 1
                },
                new Episode
                {
                    AdmissionDate = new DateTime(2015, 3, 20),
                    Diagnosis = "Sprained wrist",
                    DischargeDate = new DateTime(2015, 4, 2),
                    EpisodeId = 2,
                    PatientId = 1
                },
                new Episode
                {
                    AdmissionDate = new DateTime(2015, 11, 12),
                    Diagnosis = "Stomach cramps",
                    DischargeDate = new DateTime(2015, 11, 14),
                    EpisodeId = 3,
                    PatientId = 1
                },
                new Episode
                {
                    AdmissionDate = new DateTime(2015, 4, 18),
                    Diagnosis = "Laryngitis",
                    DischargeDate = new DateTime(2015, 5, 26),
                    EpisodeId = 4,
                    PatientId = 2
                },
                new Episode
                {
                    AdmissionDate = new DateTime(2015, 6, 2),
                    Diagnosis = "Athlete's foot",
                    DischargeDate = new DateTime(2015, 6, 13),
                    EpisodeId = 5,
                    PatientId = 2
                }
            });

            patientsService = new Mock<IPatientsService>();
            patientsService.Setup(service => service.GetPatients()).Returns(new List<Patient>()
            {
                new Patient
                {
                    DateOfBirth = new DateTime(1972, 10, 27),
                    FirstName = "Millicent",
                    PatientId = 1,
                    LastName = "Hammond",
                    NhsNumber = "1111111111"
                },
                new Patient
                {
                    DateOfBirth = new DateTime(1987, 2, 14),
                    FirstName = "Bobby",
                    PatientId = 2,
                    LastName = "Atkins",
                    NhsNumber = "2222222222"
                },
                new Patient
                {
                    DateOfBirth = new DateTime(1991, 12, 4),
                    FirstName = "Xanthe",
                    PatientId = 3,
                    LastName = "Camembert",
                    NhsNumber = "3333333333"
                }
            });

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:65534/patients/0/episodes");
            var route = config.Routes.MapHttpRoute("Default", "patients/{patientId}/episodes");
            patientsController = new PatientsController(patientsService.Object, episodesService.Object) { Request = request };
            patientsController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var patientId = 0;
            var result = patientsController.Get(patientId);
            Assert.IsNotNull(result.StatusCode);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            Assert.AreEqual(((System.Net.Http.ObjectContent)result.Content).Value, $"Patient not found with patientId :{patientId}.");

            patientId = 1;
            result = patientsController.Get(patientId);
            Assert.IsNotNull(result.StatusCode);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual((((System.Net.Http.ObjectContent)result.Content).Value as Patient).PatientId, patientId);

            patientId = 2;
            result = patientsController.Get(patientId);
            Assert.IsNotNull(result.StatusCode);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual((((System.Net.Http.ObjectContent)result.Content).Value as Patient).PatientId, patientId);

            patientId = int.MinValue;
            result = patientsController.Get(patientId);
            Assert.IsNotNull(result.StatusCode);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
