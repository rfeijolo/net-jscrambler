JScrambler Client for .NET
==========================

**ATTENTION**: THIS IS AN BETA VERSION. 

API
---
All of the API operations are wrapped in a static facade with the following properties/methods:

### Client
```csharp
var jscrambler = new JScrambler("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY");
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
