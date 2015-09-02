using Ionic.Zip;
using RestSharp;
using RestSharp.Contrib;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using JScrambler.Client.Configuration;
using System.Configuration;

namespace JScrambler.Client
{
    public class JScrambler
    {
        private readonly ServiceSection serviceConfig;

        private string ApiUrl 
        {
            get
            {
                return "http" + (this.serviceConfig.ApiPort == 443 ? "s" : "") + "://" + this.serviceConfig.ApiHost + (this.serviceConfig.ApiPort != 80 ? (":" + this.serviceConfig.ApiPort) : "") + "/v" + this.serviceConfig.ApiVersion;
            }
        }

        public JScrambler()
        {
            var section = ConfigurationManager.GetSection(ServiceSection.SectionName);
            serviceConfig = section as ServiceSection;
        }

        public JScrambler(string accessKey, string secretKey)
        {
            this.serviceConfig = this.serviceConfig ?? new ServiceSection();
            this.serviceConfig.Credentials.AccessKey = accessKey;
            this.serviceConfig.Credentials.SecretKey = secretKey;
#if DEBUG
            this.serviceConfig.ApiPort = 80;
#endif
        }

        public JScrambler(string accessKey, string secretKey, string apiHost)
            : this(accessKey, secretKey)
        {
            this.serviceConfig = this.serviceConfig ?? new ServiceSection();
            this.serviceConfig.ApiHost = apiHost;
        }

        public JScrambler(string accessKey, string secretKey, string apiHost, int apiPort)
            : this(accessKey, secretKey, apiHost)
        {
            this.serviceConfig = this.serviceConfig ?? new ServiceSection();
            this.serviceConfig.ApiPort = apiPort;
        }

        /* API operations methods */

        public UploadCodeResult UploadCode(UploadCodeRequest uploadRequest, SortedDictionary<string, string> optionalParameters = null)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            // TODO: define TEMP file name / folder
            parameters.Add("files", "files.zip");
            parameters.Merge(optionalParameters);

            ZipProject(uploadRequest.Files);

            var response = ExecuteRestRequest(Method.POST, "/code.json", parameters);

            // TODO: remove temp file

            return new JsonDeserializer().Deserialize<UploadCodeResult>(response);
        }

        public List<Project> GetInfo()
        {
            var response = ExecuteRestRequest(Method.GET, "/code.json", new SortedDictionary<string, string>());

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var projects = new JsonDeserializer().Deserialize<List<Project>>(response);

                return projects ?? new List<Project>(); // could be an empty list
            }
            else
            {
                return null;
            }
        }

        public ProjectInfoResult GetInfo(string projectId)
        {
            var resourcePath = string.Format("/code/{0}.json", projectId);
            var response = ExecuteRestRequest(Method.GET, resourcePath, new SortedDictionary<string, string>());

            return new JsonDeserializer().Deserialize<ProjectInfoResult>(response);
        }

        public bool DownloadProject(string projectId)
        {
            var resourcePath = string.Format("/code/{0}.zip", projectId);
            var response = ExecuteRestRequest(Method.GET, resourcePath, new SortedDictionary<string, string>());

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string tempFile = string.Format("{0}.zip", projectId);

                try
                {
                    File.WriteAllBytes(tempFile, response.RawBytes);
                }
                catch (IOException ex)
                {
                    throw ex;
                }

                return true;
            }

            return false;
        }

        // TODO: save to output dir
        public string DownloadSourceCode(string projectId, string sourceId, string extension)
        {
            var resourcePath = string.Format("/code/{0}/{1}.{2}", projectId, sourceId, extension);
            var response = ExecuteRestRequest(Method.GET, resourcePath, new SortedDictionary<string, string>());

            // TODO: save file at a specified folder

            // NotFound (401) means still pending processing
            return (response.StatusCode == HttpStatusCode.NotFound) ? null : response.Content;
        }

        public DeleteProjectResult DeleteProject(string projectId)
        {
            var resourcePath = string.Format("/code/{0}.zip", projectId);
            var response = ExecuteRestRequest(Method.DELETE, resourcePath, new SortedDictionary<string, string>());

            return new JsonDeserializer().Deserialize<DeleteProjectResult>(response);
        }

        /* support methods */

        private RestResponse ExecuteRestRequest(Method requestMethod, string resourcePath, SortedDictionary<string, string> parameters)
        {
            var signedData = SignedQuery(requestMethod.ToString(), resourcePath, parameters);
            var resourceUri = requestMethod == Method.POST ? resourcePath : (resourcePath + "?" + signedData);

            var restClient = new RestClient(this.ApiUrl);
            var restRequest = new RestRequest(resourceUri, requestMethod);

            foreach (var item in parameters)
            {
                if (item.Key.StartsWith("file"))
                {
                    continue;
                }

                restRequest.AddParameter(item.Key, item.Value);
                restRequest.AddHeader(item.Key, item.Value);
            }

            // attach zipped file when uploading code
            if (requestMethod == Method.POST && parameters.ContainsKey("file_0"))
            {
                var bytes = GetFileBytes("files.zip"); // TODO: handle temp file

                restRequest.AddFile("file_0", bytes, "files.zip", "application/zip, application/octet-stream");
            }

            return (RestResponse)restClient.Execute(restRequest);
        }

        private void ZipProject(List<string> files)
        {
            var existingFileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files.zip");

            if (File.Exists(existingFileFullPath))
            {
                File.Delete(existingFileFullPath);
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFiles(files);

                zip.Save("files.zip");
            }
        }

        private string SignedQuery(string requestMethod, string resourcePath, SortedDictionary<string, string> parameters, DateTime? timestamp = null)
        {
            return UrlQueryString(SignedParams(requestMethod, resourcePath, parameters, timestamp));
        }

        private SortedDictionary<string, string> SignedParams(string requestMethod, string resourcePath, SortedDictionary<string, string> parameters, DateTime? timestamp = null)
        {
            if (requestMethod.ToUpper().Equals("POST") && parameters.ContainsKey("files"))
            {
                SortedDictionary<string, string> fileParams = ConstructFileParams((string[])parameters["files"].Split(new Char[] { ';' }));
                parameters.Remove("files");
                parameters.Merge<string, string>(fileParams);
            }

            parameters.Add("access_key", this.serviceConfig.Credentials.AccessKey);
            parameters.Add("timestamp", timestamp.ToISO8601String());
            parameters.Add("signature", GenerateHMACSignature(requestMethod, resourcePath, parameters));

            return parameters;
        }

        private SortedDictionary<string, string> ConstructFileParams(string[] files)
        {
            var fileParams = new SortedDictionary<string, string>();
            int fileCount = files.Length;

            for (int i = 0; i < fileCount; i++)
            {
                try
                {
                    fileParams.Add("file_" + i.ToString(), CreateMD5Checksum(files[i]));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return fileParams;
        }

        private string CreateMD5Checksum(string filename)
        {
            var fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileFullPath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower(); 
                }
            }
        }

        private byte[] GetFileBytes(string file)
        {
            var fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            byte[] bytes = File.ReadAllBytes(fileFullPath);

            return bytes;
        }

        private string GenerateHMACSignature(string requestMethod, string resourcePath, SortedDictionary<string, string> parameters)
        {
            string data = HMACSignatureData(requestMethod, resourcePath, parameters);
         
            try
            {
                var encoding = new System.Text.ASCIIEncoding();
                byte[] keyByte = encoding.GetBytes(this.serviceConfig.Credentials.SecretKey);
                byte[] messageBytes = encoding.GetBytes(data);

                using (var hmacsha256 = new HMACSHA256(keyByte))
                {
                    byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

                    return Convert.ToBase64String(hashmessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string HMACSignatureData(string requestMethod, string resourcePath, SortedDictionary<string, string> parameters)
        {
            return requestMethod.ToUpper() + ";" + this.serviceConfig.ApiHost.ToLower() + ";" + resourcePath + ";" + UrlQueryString(parameters);
        }

        private string UrlQueryString(SortedDictionary<string, string> parameters)
        {
            var queryParamsString = string.Empty;

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                if (entry.Key.StartsWith("file_") || entry.Key.Equals("signature"))
                {
                    queryParamsString += UrlEncode(entry.Key) + "=" + entry.Value + "&";
                }
                else
                {
                    if (entry.Key == "name_prefix" || entry.Key == "ignore_files" || entry.Key == "asserts_elimination")
                    {
                        queryParamsString += UrlEncode(entry.Key) + "=" + UrlEncode(entry.Value) + "&";
                    }
                    else
                    {

                        queryParamsString += UrlEncode(entry.Key) + "=" + UrlEncode(entry.Value).ToUpper() + "&";
                    }
                }
            }

            return queryParamsString.Substring(0, queryParamsString.Length - 1);
        }

        private string UrlEncode(string data)
        {
            try
            {
                var encodedStr = HttpUtility.UrlEncode(data, Encoding.UTF8);
                encodedStr = encodedStr.Replace("%7E", "~");
                encodedStr = encodedStr.Replace("+", "%20");
                encodedStr = encodedStr.Replace("*", "%2A");

                return encodedStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}