using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestApi.Interfaces;
using RestApi.Models;

namespace RestApi.Services
{
    public class EpisodesService : IEpisodesService
    {
        private IDatabaseContext databaseContext;

        public EpisodesService(IDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IList<Episode> GetEpisodes()
        {
            return this.databaseContext.Episodes.ToList();
        }
    }
}
