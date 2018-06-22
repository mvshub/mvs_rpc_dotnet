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
    public class RPCException : ApplicationException
    {
        public RPCException(String msg)
            : base(msg)
        {
        }
    }

    public partial class RPC
    {
        public String help()
        {
            return getResult<String>("help", null);
        }

        public T getResult<T>(String method, List<String> parameters)
        {
            JsonRPCResponse rpc_res = null;
            try
            {
                rpc_res = postRequest(formatPostField(method, parameters));
                if (rpc_res.CODE == 0)
                {
                    if (typeof(T).IsValueType || typeof(T) == typeof(String))
                        return (T)Convert.ChangeType(rpc_res.DATA, typeof(T));
                    else
                        return JsonExtension.JsonToObject<T>(rpc_res.DATA);
                }
                else
                    throw new RPCException(rpc_res.DATA);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public String formatPostField(String method, List<String> parameters)
        {
            JsonRPCReqHeader helper = new JsonRPCReqHeader();
            helper.id = 47;
            helper.jsonrpc = "2.0";
            helper.parameters = new List<String>();
            helper.method = method;
            helper.parameters = parameters;
            
            return JsonExtension.ObjectToJson(helper);
        }

        public JsonRPCResponse postRequest(String postData)
        {
            String strRes = HTTPHelper.postRequest(url_, postData);
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
                else if (root["result"].Type == JTokenType.String
                    || root["result"].Type == JTokenType.Array 
                    || root["result"].Type == JTokenType.Object)
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

        private String url_ = "http://127.0.0.1:8820/rpc/v2";
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
    }


    public class JsonRPCReqHeader
    {
        public Int32 id { get; set; }
        public String jsonrpc { get; set; }
        public String method { get; set; }

        [DataMember(Name = "params")]
        public List<String> parameters { get; set; }
    }

    public class JsonRPCResponse
    {
        public Int32 CODE { get; set; }
        public String DATA { get; set; }
    }

    public class output_point
    {
        public String hash { get; set; }
        public UInt32 index { get; set; }
    }
    public enum attachment_type
    {
        attachment_etp,
        attachment_etp_award,
        attachment_asset,
        attachment_message,
        attachment_did,
        attachment_asset_cert,
        attachment_asset_mit
    };
    public class attachment
    {
        public attachment_type type { get; set; }
        public String todid { get; set; }
        public String fromdid { get; set; }
        public Int32 attach { get; set; }

    }
    public class input
    {
        public output_point previous_output { get; set; }
        public String script { get; set; }
        public UInt32 sequence { get; set; }
        public String address { get; set; }
    }
    public class output
    {
        public UInt64 value { get; set; }
        public String script { get; set; }

        public attachment attachment { get; set; }
        public String address { get; set; }
        public Int32 index { get; set; }
        public Int32 lockd_height_range { get; set; }
        public Boolean own { get; set; }
    }
    public class transaction
    {
        public String hash { get; set; }
        public UInt64 height { get; set; }
        public List<input> inputs { get; set; }
        public List<output> outputs { get; set; }
        public Int32 lock_time { get; set; }
        public Int32 version { get; set; }
        public String raw { get; set; }
    }
    public class blockheader
    {
        public String nonce { get; set; }
        public String hash { get; set; }
        public String bits { get; set; }
        public String previous_block_hash { get; set; }
        public Int32 number { get; set; }
        public Int32 transaction_count { get; set; }
        public Int32 version { get; set; }
        public String mixhash { get; set; }
        public Int32 time_stamp { get; set; }
        public String merkle_tree_hash { get; set; }
    }
    public class block
    {
        public String raw { get; set; }
        public blockheader header { get; set; }
        public List<transaction> transactions { get; set; }
    }

    public class account
    {
        public String mnemonic { get; set; }
        public String default_address { get; set; }
    }
    
    public class address
    {
        public String address_type { get; set; }
        public Boolean is_valid { get; set; }
        public String message { get; set; }
        public Boolean test_net { get; set; }

    }

    public class accountEx
    {
        public String mnemonic { get; set; }
        public List<String> addresses { get; set; }
        public String name { get; set; }
        public Int32 hd_index { get; set; }

    }
    public class accountstatus
    {
        public String name { get; set; }
        public String status { get; set; }
    }

    public class accountresult
    {
        public Int32 user_status { get; set; }
        public String default_address { get; set; }
        public String mnemonic { get; set; }
        public Int32 address_count { get; set; }
    }

    public class info
    {
        public String database { get;set;}
        public String difficulty { get; set; }
        public UInt64 hash_rate { get; set; }
        public UInt64 height { get; set; }
        public Boolean is_mining { get; set; }
        public UInt64 network_assets_count { get; set; }
        public UInt64 peers { get; set; }
        public UInt64 protocol_version { get; set; }
        public Boolean testnet { get; set; }
        public UInt64 wallet_account_count { get; set; }
        public String wallet_version { get; set; }
    }

    public class mining_info
    {
        public String difficulty { get; set; }
        public UInt64 height { get; set; }
        public Boolean is_mining { get; set; }
        public UInt64 rate { get; set; }
    }

    public class balances
    {
        public UInt64 total_available { get; set; }
        public UInt64 total_confirmed { get; set; }
        public UInt64 total_frozen { get; set; }
        public UInt64 total_received { get; set; }
        public UInt64 total_unspent { get; set; }

    }

    public class balance
    {
        public String address { get; set; }
        public UInt64 available { get; set; }
        public UInt64 confirmed { get; set; }
        public UInt64 frozen { get; set; }
        public UInt64 received { get; set; }
        public UInt64 unspent { get; set; }
    }


    public class transactionEx
    {
        public String direction { get; set; }
        public String hash { get; set; }
        public Int64 height { get; set; }
        public Int64 timestamp { get; set; }
        public List<input> inputs { get; set; }
        public List<output> outputs { get; set; } 
    }

    public class transactions
    {
        public Int32 current_page { get; set; }
        public Int32 total_page { get; set; }
        public Int32 transaction_count { get; set; }
        [DataMember(Name = "transactions")]
        public List<transactionEx> transactions_ { get; set; }
    }

    public class asset
    {
        public String address { get; set; }
        public Int32 decimal_number { get; set; }
        public String description { get; set; }
        public Boolean is_secondaryissue { get; set; }
        public String issuer { get; set; }
        public Int64 maximum_supply { get; set; }
        public Int32 secondaryissue_threshold { get; set; }
        public String status { get; set; }
        public String symbol { get; set; }

        public Int64 locked_quantity { get; set; }
        public Int64 quantity { get; set; }
    }

    public class delasset
    {
        public String operate { get; set; }
        public String result { get; set; }
        public String symbol { get; set; }
    }

    public class assetcert
    {
        public String address { get; set; }
        public String cert { get; set; }
        public String owner { get; set; }
        public String symbol { get; set; }
    }

    public class mulsignature
    {
        public String address { get; set; }
        public String description { get; set; }
        public Int32 index { get; set; }
        public Int32 m { get; set; }
        public Int32 n { get; set; }
        public String multisig_script { get; set; }
        public List<String> public_keys { get; set; }
        public String self_publickey { get; set; }

    }

    public class rawtx
    {
        public String hash { get; set; }
        public String hex { get; set; }
    }

    public class did
    {
        public String address { get; set; }
        public String status { get; set; }
        public String symbol { get; set; }
    }

    public class didaddress
    {
        public String address { get; set; }
        public String status { get; set; }
    }
    public class didhistoryaddress
    {
        public String did { get; set; }
        public List<didaddress> addresses { get; set; }
    }

    public class mit
    {
        public String address { get; set; }
        public String content { get; set; }
        public Int64 height { get; set; }
        public String status { get; set; }
        public String symbol { get; set; }
        public Int64 time_stamp { get; set; }
        public String to_did { get; set; }
    }

}
