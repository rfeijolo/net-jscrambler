using System;
using System.Configuration;

namespace JScrambler.Client.Configuration
{
    public class CredentialsElement : ConfigurationElement
    {
        [ConfigurationProperty("accessKey", IsRequired = true)]        
        public String AccessKey
        {
            get
            {
                return (String)this["accessKey"];
            }
            set
            {
                this["accessKey"] = value;
            }
        }

        [ConfigurationProperty("secretKey", IsRequired = true)]
        public String SecretKey
        {
            get
            {
                return (String)this["secretKey"];
            }
            set
            {
                this["secretKey"] = value;
            }
        }
    }
}