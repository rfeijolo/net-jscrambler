using System;
using System.Configuration;

namespace JScrambler.Client.Configuration
{
    public class ServiceSection : ConfigurationSection
    {
        public const String SectionName = "jscrambler/service";

        [ConfigurationProperty("apiHost", DefaultValue = "api.jscrambler.com", IsRequired = false)]
        public String ApiHost
        {
            get
            {
                return (String)this["apiHost"];
            }
            set
            {
                this["apiHost"] = value;
            }
        }

        [ConfigurationProperty("apiVersion", DefaultValue = "3", IsRequired = false)]
        public int ApiVersion
        {
            get
            {
                return (int)this["apiVersion"];
            }
            set
            {
                this["apiVersion"] = value;
            }
        }

        [ConfigurationProperty("apiPort", DefaultValue = "443", IsRequired = false)]
        public int ApiPort
        {
            get
            {
                return (int)this["apiPort"];
            }
            set
            {
                this["apiPort"] = value;
            }
        }

        [ConfigurationProperty("credentials")]
        public CredentialsElement Credentials
        {
            get
            {
                return (CredentialsElement)this["credentials"];
            }

            set
            {
                this["credentials"] = value; 
            }
        }
    }
}