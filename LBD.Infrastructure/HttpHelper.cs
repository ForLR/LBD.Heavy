using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LBD.Infrastructure
{
    public static class HttpHelper
    {
        public static string Post(string url,string data)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Method = "Post";
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = 6000;
                byte[] requestData = Encoding.UTF8.GetBytes(data);
                request.ContentLength = requestData.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(requestData, 0, requestData.Length);
                }
                var response = request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = sr.ReadToEnd();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        public static string Get(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Method = "Get";
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = 6000;
               
                var response = request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = sr.ReadToEnd();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
