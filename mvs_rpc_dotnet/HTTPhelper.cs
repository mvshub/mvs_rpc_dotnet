using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Data;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.WebSockets;
namespace mvs_rpc
{
    
    public static class HTTPHelper
    {

        public static String postRequest(String url, String postdata)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.AllowAutoRedirect = false;

            String responseStr = "";

            try
            {
                StreamWriter requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(postdata);
                requestStream.Close();

                WebResponse response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                    return responseStr;
                }
            }
            catch (Exception e)
            {
                responseStr = e.Message;
            }

            return responseStr;
        }

    }

}
