using JScrambler.Client.Configuration;
using NUnit.Framework;
using System.Configuration;

namespace JScrambler.Client.Tests
{
    public class ConfigurationTests
    {
        [Test]
        public void CanReadConfiguration()
        {
            System.Configuration.ConfigurationFileMap fileMap = new ConfigurationFileMap("App.config"); // path to test config file
            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenMappedMachineConfiguration(fileMap);

            var serviceConfig = configuration.GetSection(ServiceSection.SectionName) as ServiceSection;

            Assert.NotNull(serviceConfig);
        }
    }
}