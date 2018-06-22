using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mvs_rpc;
using System.Reflection;

namespace test_rpc
{
    class Program
    {
        static void Main(string[] args)
        {
            RPC rpc = new RPC();
            MethodInfo method = typeof(RPC).GetMethod(args[0]);
            if(method != null)
            {
                int count = method.GetParameters().Count();
                List<String> paramesrs = new List<String>(args.ToList().Skip(1).Take(args.Count()-1));
                for(int i = paramesrs.Count(); i < count;i++)         
                    paramesrs.Add(null);
                
                var ret =  method.Invoke(rpc, paramesrs.Take(count).ToArray());

                if(ret!= null)
                {
                    String strResult = JsonExtension.ObjectToJson(ret);
                    strResult.Replace("/n", "/r/n");
                    Console.WriteLine(strResult);
                }
            }

        }
    }
}
