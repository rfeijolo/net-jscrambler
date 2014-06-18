using System;

namespace JScrambler.Client
{
    public class Project
    {
        public string Id { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ReceivedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public int JsFiles { get; set; }
    }
}