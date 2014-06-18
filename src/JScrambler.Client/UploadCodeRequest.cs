using System;
using System.Collections.Generic;

namespace JScrambler.Client
{
    public class UploadCodeRequest
    {
        public UploadCodeRequest()
        {
            this.Files = new List<string>();
        }

        public List<string> Files { get; set; }

        public string DomainLock { get; set; }

        public DateTime ExpirationDate { get; set; }

        public OptionalParameters OptionalParameters { get; set; }
    }
}