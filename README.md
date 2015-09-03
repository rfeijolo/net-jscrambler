JScrambler Client for .NET
==========================

**ATTENTION**: THIS IS AN BETA VERSION. 

History
-------
**2014-06-28**

* Add initialization from configuration;
* Update documentation.

**2014-06-18**

* Initial beta release.

API
---
All of the API operations are wrapped in a static facade with the following properties/methods:

### Client

#### Installation

** Manual **

* Build from source;
* Add JScrambler.Client.dll to your references;
* Install dependencies `DotNetZip` and `RestSharp` using nuget console.


** Using nuget console **

Soon.

#### Configuration (optional)

This `App.test.config` can be found in the test project root.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  
  <configSections>
    <sectionGroup name="jscrambler">
      <section
        name="service"
        type="JScrambler.Client.Configuration.ServiceSection, JScrambler.Client"
        allowLocation="false"
        allowDefinition="Everywhere" />
    </sectionGroup>
  </configSections>

  <jscrambler>
    <service 
      apiHost="api.jscrambler.com" 
      apiVersion="3" 
      apiPort="80"> <!--use 443 for production--> 
      <credentials
        accessKey="YOUR_ACCESS_KEY" 
        secretKey="YOUR_SECRET_KEY" />
    </service>
  </jscrambler>

</configuration>
```

#### Initialization

```csharp
// init reading application configuration settings
var jscrambler = new JScrambler();

// init supplying access/secret keys
var jscrambler = new JScrambler("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY");

// init supplying access/secret keys and API host
var jscrambler = new JScrambler("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY", "api.jscrambler.com");

// init supplying access/secret keys, API host/port
var jscrambler = new JScrambler("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY", "api.jscrambler.com", 80);
```

### Available methods

* UploadCode
* GetInfo (project or list of projects)
* DownloadProject
* DownloadSourceCode
* DeleteProject

TODO
---
* Complete unit tests
* Fix failing unit tests
* Create predefined set's of templates
* Refactor `DownloadSourceCode`
* Publish nuget package
* WIKI with complete examples
* Return Task and use async
