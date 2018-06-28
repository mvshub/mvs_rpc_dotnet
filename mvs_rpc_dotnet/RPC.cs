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
    public partial class RPC
    {
        public RPC(String url= "http://127.0.0.1:8820", rpcversion version = rpcversion.v3)
        {
            url_ = url;
            version_ = version;
        }

        public String help(String method=null)
        {
            if(method == null)
                return getResult<String>("help", null);
                
            return getResult<String>(method, new List<string> { "--help"});
            
        }
        public String test_rpc(List<String> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                return getResult<String>(parameters.ElementAt(0), parameters.Skip(1).Take(parameters.Count - 1).ToList());

            return help();
        }

        private T getResult<T>(String method, List<String> parameters)
        {
            JsonRPCResponse rpc_res = null;

            rpc_res = postRequest(formatPostField(method, parameters));
            if (rpc_res.CODE != 0)
                throw new Exception(rpc_res.DATA);

            if (typeof(T).IsValueType || typeof(T) == typeof(String))
                return (T)Convert.ChangeType(rpc_res.DATA, typeof(T));
            else
                return JsonExtension.JsonToObject<T>(rpc_res.DATA);

        }


        private String formatPostField(String method, List<String> parameters)
        {
            JsonRPCReqHeader helper = new JsonRPCReqHeader();
            helper.id = 10000;
            helper.jsonrpc = "2.0";
            helper.parameters = new List<String>();
            helper.method = method;
            if (parameters != null)
                foreach (var row in parameters)
                    helper.parameters.Add(row);

            return JsonExtension.ObjectToJson(helper);
        }


        private JsonRPCResponse postRequest(String postData)
        {
            String strRes = HTTPHelper.postRequest(String.Format("{0}/rpc/{1}", url_, version_.ToString()) , postData);

            JsonRPCResponse response = new JsonRPCResponse() { CODE = 0,DATA = ""};
            JObject root = (JObject)JsonConvert.DeserializeObject(strRes);

            if (root.Type == JTokenType.Object)
            {
                if (root.ContainsKey("error") 
                    && root["error"]["code"].Type == JTokenType.Integer && (int)root["error"]["code"] != 0)
                {
                    response.CODE = (int)root["error"]["code"];
                    response.DATA = root["error"].ToString();
                }
                else if (root.ContainsKey("result"))
                {
                    response.DATA = root["result"].ToString();

                }
                else
                {
                    response.CODE = -1;
                    response.DATA = "parase json failed";
                }
            }
            else
            {
                response.CODE = -1;
                response.DATA = "parase json failed";
            }

            return response;
        }

        private String url_;
        private rpcversion version_;

        public String URL {
            get
            {
                return url_;
            }
            set
            {
                url_ = value;
            }
        }
        public rpcversion VERSION
        {
            get
            {
                return version_;
            }
            set
            {
                version_ = value;
            }
        }
    }

    public enum rpcversion
    {
        v1,
        v2,
        v3
    }


    [DataContract]
    public class JsonRPCReqHeader
    {
        [DataMember(Name = "id")]
        public Int32 id { get; set; }
        [DataMember(Name = "jsonrpc")]
        public String jsonrpc { get; set; }
        [DataMember(Name = "method")]
        public String method { get; set; }
        [DataMember(Name = "params")]
        public List<String> parameters { get; set; }
    }

    public class JsonRPCResponse
    {
        public Int32 CODE { get; set; }
        public String DATA { get; set; }
    }

}
