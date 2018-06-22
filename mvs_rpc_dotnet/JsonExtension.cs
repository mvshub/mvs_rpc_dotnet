using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace mvs_rpc
{
    public static class JsonExtension
    {
        //from object to Json string 
        public static String ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        //from Json string to object
        public static T JsonToObject<T>(String jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));
        }
    }
}
