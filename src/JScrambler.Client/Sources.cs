using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JScrambler.Client
{
    public class Sources
    {
        public string Id { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        // --- for ProjectInfoResource
        public int NewSize { get; set; }
        public DateTime ReceivedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string ErrorId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
