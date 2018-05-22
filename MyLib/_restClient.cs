using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}

namespace MyLib
{
    public class _restClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        private string _authorization = "";

        public string Authorization
        {
            get { return this._authorization; }
            set { this._authorization = value; }
        }

        private string _authorizationType = "Authorization";
        public string AuthorizationType
        {
            get
            {
                return this._authorizationType;
            }
            set
            {
                this._authorizationType = value;
            }
        }
               

        public _restClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public _restClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public _restClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = "";
        }

        public _restClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = postData;
        }

        List<String> headerList = new List<string>();
        public void _addHeaderRequest(string data)
        {
            headerList.Add(data);
        }

        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public void _setContentType(string contentType)
        {
            this.ContentType = contentType;
        }

        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (Authorization != null && Authorization.Length > 0)
            {
                request.Headers.Add(_authorizationType + ": " + Authorization);
            }

            foreach(string headerValue in headerList)
            {
                request.Headers.Add(headerValue);
            }

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

    }
}
