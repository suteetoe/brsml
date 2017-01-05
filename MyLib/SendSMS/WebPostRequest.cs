﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MyLib.SendSMS
{
    public class WebPostRequest
    {
        WebRequest theRequest;
        HttpWebResponse theResponse;
        ArrayList theQueryData;
        public HttpStatusCode _lastStatusCode;
        public WebPostRequest(string url)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            theRequest = WebRequest.Create(url);
            theRequest.Method = "POST";
            theQueryData = new ArrayList();
        }

        public void Add(string key, string value)
        {
            theQueryData.Add(String.Format("{0}={1}", key, value));
        }

        public string GetResponse()
        {
            // Set the encoding type
            theRequest.ContentType = "application/x-www-form-urlencoded";

            // Build a string containing all the parameters
            string Parameters = String.Join("&", (String[])theQueryData.ToArray(typeof(string)));
            theRequest.ContentLength = Parameters.Length;

            // We write the parameters into the request
            StreamWriter sw = new StreamWriter(theRequest.GetRequestStream());
            sw.Write(Parameters);
            sw.Close();

            // Execute the query
            theResponse = (HttpWebResponse)theRequest.GetResponse();
            _lastStatusCode = theResponse.StatusCode;
            StreamReader sr = new StreamReader(theResponse.GetResponseStream());

            return sr.ReadToEnd();
        }
    }    
}
