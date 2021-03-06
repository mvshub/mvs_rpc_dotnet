﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using mvs_rpc;

namespace rpc_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RPC rpc = new RPC(System.Configuration.ConfigurationManager.AppSettings["host"]
            , (rpcversion)Enum.Parse(typeof(rpcversion), System.Configuration.ConfigurationManager.AppSettings["version"]));

                Object ret = null;

                if (args.Count() == 0 || args.ElementAt(0) == "help")
                {
                    String method = null;
                    if (args.Count() > 1)
                        method = args.ElementAt(1);

                    ret = rpc.help(method);
                }

                else
                {
                    MethodInfo method = typeof(RPC).GetMethod(args[0]);
                    if (method != null)
                    {
                        int count = method.GetParameters().Count();
                        List<Object> paramesrs = new List<Object>();
                        int index = 1;
                        foreach (var par in method.GetParameters())
                        {
                            if (index < args.Count())
                            {
                                var strPar = args.ElementAt(index++);
                                if (strPar != "null")
                                {
                                    Type underlyingType = Nullable.GetUnderlyingType(par.ParameterType);
                                    paramesrs.Add(Convert.ChangeType(strPar, underlyingType ?? par.ParameterType));
                                    continue;
                                }
                            }

                            paramesrs.Add(null);
                        }

                        ret = method.Invoke(rpc, paramesrs.Take(count).ToArray());

                    }
                    else
                        ret = rpc.test_rpc(args.ToList());

                }

                if (ret != null)
                {
                    String strResult = JsonExtension.ObjectToJson(ret);
                    strResult = System.Text.RegularExpressions.Regex.Unescape(strResult);
                    Console.WriteLine(strResult.ToString());
                }
            }

            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                Console.WriteLine(strOut);
            }
        }
    }
}
