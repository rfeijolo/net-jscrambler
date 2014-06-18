using System;
using System.Collections.Generic;

namespace JScrambler.Client
{
    public class ProjectInfoResponse
    {
        public ProjectInfoResponse()
        {
            this.Sources = new List<Sources>();
        }

        public string Id { get; set; }
        public string Extension { get; set; }
        public DateTime ReceivedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public List<Sources> Sources { get; set; }
    }
}