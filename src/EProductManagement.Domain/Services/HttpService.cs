using EProductManagement.Domain.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace EProductManagement.Domain.Services
{
    public class HttpService : IHttpService, IRequestHandler
    {
        public PartyDTO GetPartyInfo(string AccountNumber)
        {

            WebClient client = new WebClient();

            var response = client.OpenRead($"http://176.53.94.136:7090/User/SimplifiedUserByAccountNumber?AccountNumber="+AccountNumber);

            StreamReader reader = new StreamReader(response);
            string result = reader.ReadToEnd();

            PartyDTO party = JsonConvert.DeserializeObject<PartyDTO>(result);

            return party;
        }

        public string CreateGetRequest(Dictionary<string, string> parameters)
        {
            string suffix = string.Empty;

            if (parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    suffix += item.Key + "=" + HttpUtility.UrlEncode(item.Value) + "&";
                }

                suffix = suffix.Substring(0, suffix.Length - 1);
                suffix = "?" + suffix;
            }

            return !string.IsNullOrEmpty(suffix) ? suffix : string.Empty;
        }
        public string CreatePostRequest(Dictionary<string, string> parameters)
        {
            string requestString = string.Empty;
            if (parameters.Count > 0)
            {
                requestString = "{";

                foreach (var item in parameters)
                {
                    if (!string.IsNullOrEmpty(item.Value) && item.Value.Contains("[")) // for arrays
                    {
                        requestString += "\"" + item.Key + "\": " + item.Value + ",";
                        continue;
                    }

                    if (item.Value.Contains("{"))
                    {
                        requestString += "\"" + item.Key + "\": " + item.Value + ",";
                    }
                    else
                    {
                        requestString += "\"" + item.Key + "\": \"" + item.Value + "\",";
                    }
                }

                requestString = requestString.Substring(0, requestString.Length - 1);
                requestString += "}";
            }
            else
            {
                requestString = "{}";
            }

            return requestString;
        }
        public string SendGetRequest(string Url, string Token = null)
        {
            WebClient client = new WebClient();
            client.Headers.Set("Content-Type", "application/json");

            NameValueCollection coll = client.Headers;

            if (Token != "")
            {
                client.Headers.Set("Authorization", Token);
            }

            var response = client.OpenRead(Url);


            StreamReader reader = new StreamReader(response);
            string result = reader.ReadToEnd();

            return result;
        }
        public string SendPostRequest(string Request, string Url, string? Token)
        {
            WebClient client = new WebClient();
            client.Headers.Set("Content-Type", "application/json");

            if (Token != "")
            {
                client.Headers.Set("Authorization", Token);
            }

            byte[] byteArray = client.UploadData(Url, "POST", Encoding.UTF8.GetBytes(Request));
            string result = Encoding.UTF8.GetString(byteArray);

            return result;
        }

        public string CreatePostRequestForObject(object Object)
        {
            var json = JsonConvert.SerializeObject(Object);
            return json.ToString();
        }

        public PartyDTO GetMerchantInfo(string MerchantAccountNumber)
        {
            WebClient client = new WebClient();

            var response = client.OpenRead($"http://176.53.94.136:7090/Merchant/ByAccountNumber?MerchantAccountNumber=" + MerchantAccountNumber);

            StreamReader reader = new StreamReader(response);
            string result = reader.ReadToEnd();

            PartyDTO party = JsonConvert.DeserializeObject<PartyDTO>(result);

            return party;
        }
    }
}
