using System;
using System.Collections.Generic;

namespace JScrambler.Client
{
    public class UploadCodeResult
    {
        public UploadCodeResult()
        {
            this.Sources = new List<Sources>();
        }

        public string Id { get; set; }
        public string Extension { get; set; }
        public DateTime ReceivedAt { get; set; }
        public List<Sources> Sources { get; set; }
    }
}