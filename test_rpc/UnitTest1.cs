using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using mvs_rpc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading;

namespace test_rpc
{
    [TestClass]
    public class UnitTest1
    {
        String url = "http://10.10.10.159:8820";

        [TestMethod]
        public void test_mit()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) =>
            {
                file.WriteLine(str);
            };

            try
            {

                String strName = "test" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address = JObject.Parse(ret)["addresses"][0].ToString();

                //register did need at least 1 etp==10e8,issue asset need at least 10 etp==10e9
                String didsymbol = strName + ".identity";
                rpc.send("lxf", "123", address, Convert.ToUInt64(Math.Pow(10, 9)));
                mining(rpc, 1);
                rpc.registerdid(strName, "123456", address, didsymbol);
                mining(rpc, 2);

                String mitsymbol = strName + ".mit";
                fun("registermit");
                fun(rpc.registermit(strName, "123456", didsymbol, mitsymbol));
                mining(rpc, 1);

                fun("listmits");
                fun(rpc.listmits(strName, "123456"));

                fun("listmits");
                fun(rpc.listmits());

                fun("getmit");
                fun(rpc.getmit());

                fun("getmit");
                fun(rpc.getmit(mitsymbol));

                fun("transfermit");
                fun(rpc.transfermit(strName, "123456", "BLACKHOLE", mitsymbol));
                mining(rpc, 1);

                rpc.deleteaccount(strName, "123456", mnemonic.Last());
            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();

        }

        [TestMethod]
        public void test_did()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName = "test" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address = JObject.Parse(ret)["addresses"][0].ToString();

                List<String> addresses = JsonExtension.JsonToObject<List<String>>(rpc.getnewaddress(strName, "123456", 3));

                //register did need at least 1 etp==10e8,issue asset need at least 10 etp==10e9
                String didsymbol = strName + ".identity";
                rpc.send("lxf", "123", address, Convert.ToUInt64(Math.Pow(10, 10)));
                mining(rpc, 1);
                fun("registerdid");
                fun(rpc.registerdid(strName, "123456", address, didsymbol));
                mining(rpc, 2);

                fun("listdids");
                fun(rpc.listdids());

                fun("listdids");
                fun(rpc.listdids(strName, "123456"));


                fun("didsend");
                fun(rpc.didsend(strName, "123456", addresses.First(), 10000001, "didsend ", 100001));
                mining(rpc, 1);

                fun("didsendfrom");
                fun(rpc.didsendfrom(strName, "123456", address, addresses.First(), 10000002, "didsendfrom ", 100002));
                mining(rpc, 1);

                fun("didsendmore");
                fun(rpc.didsendmore(strName, "123456", addresses.Select(addr => addr + ":100000").ToList(), addresses.First(), 100003));
                mining(rpc, 1);

                String assetsymbol = strName + ".ast";
                rpc.createasset(strName, "123456", assetsymbol, didsymbol, 100000, 3, null, didsymbol + "'s asset");
                rpc.issue(strName, "123456", assetsymbol);
                mining(rpc, 1);

                fun("didsendasset");
                fun(rpc.didsendasset(strName, "123456", "BLACKHOLE", assetsymbol, 100));
                mining(rpc, 1);


                rpc.didsendmore(strName, "123456", new List<string> { address + ":100000", addresses.First() + ":100000" }, addresses.First(), 100003);
                mining(rpc, 1);

                fun("didsendassetfrom");
                fun(rpc.didsendassetfrom(strName, "123456", address, "BLACKHOLE", assetsymbol, 100));
                mining(rpc, 10);


                fun("didchangeaddress");
                fun(rpc.didchangeaddress(strName, "123456", addresses.First(), didsymbol));
                mining(rpc, 1);


                fun("getdid");
                fun(rpc.getdid());

                fun("getdid");
                fun(rpc.getdid(didsymbol));

                fun("getdid");
                fun(rpc.getdid(addresses.First()));

                rpc.deleteaccount(strName, "123456", mnemonic.Last());

            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();

        }

        [TestMethod]
        public void test_rawtx()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName = "test" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address = JObject.Parse(ret)["addresses"][0].ToString();

                fun("createrawtx");
                ret = rpc.createrawtx(0, new List<string> { "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo" }
                , new List<string> { address + ":10000" }, null, null, null, "send etp to:" + "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", 100000);
                fun(ret);

                fun("signrawtx");
                ret = rpc.signrawtx("lxf", "123", ret);
                fun(ret);
                String hash = JObject.Parse(ret)["hash"].ToString();
                String rawtx = JObject.Parse(ret)["rawtx"].ToString();

                fun("decoderawtx");
                fun(rpc.decoderawtx(rawtx));

                fun("sendrawtx");
                fun(rpc.sendrawtx(rawtx));
                mining(rpc, 1);

                rpc.deleteaccount(strName, "123456", mnemonic.Last());
            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();


        }

        [TestMethod]
        public void test_asset()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName = "test" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address = JObject.Parse(ret)["addresses"][0].ToString();

                //register did need at least 1 etp==10e8,issue asset need at least 10 etp==10e9
                String didsymbol = strName + ".identity";
                rpc.send("lxf", "123", address, Convert.ToUInt64(Math.Pow(10, 10)));
                mining(rpc, 1);
                rpc.registerdid(strName, "123456", address, didsymbol);
                mining(rpc, 2);

                //
                String assetsymbol = strName + ".ast";
                fun("createasset");
                fun(rpc.createasset(strName, "123456", assetsymbol, didsymbol, 100000, null, null, didsymbol + "'s asset"));

                fun("deletelocalasset");
                fun(rpc.deletelocalasset(strName, "123456", assetsymbol));


                rpc.createasset(strName, "123456", assetsymbol, didsymbol, 100000, 3, null, didsymbol + "'s asset");

                fun("issue");
                fun(rpc.issue(strName, "123456", assetsymbol));
                mining(rpc, 1);

                fun("getaccountasset");
                fun(rpc.getaccountasset(strName, "123456", assetsymbol));

                fun("getaccountcert");
                fun(rpc.getaccountcert(strName, "123456"));

                fun("getaddressasset");
                fun(rpc.getaddressasset(address));

                fun("getaddresscert");
                fun(rpc.getaddresscert(address));

                fun("getasset");
                fun(rpc.getasset(assetsymbol));

                fun("getcert");
                fun(rpc.getcert());

                fun("listassets");
                fun(rpc.listassets(strName, "123456"));

                fun("listcerts");
                fun(rpc.listcerts(strName, "123456"));

                fun("sendasset");
                fun(rpc.sendasset(strName, "123456", "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", assetsymbol, 100));
                fun("sendasset");
                fun(rpc.sendassetfrom(strName, "123456", address, "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", assetsymbol, 100));
                mining(rpc, 1);

                fun("secondaryissue");
                fun(rpc.secondaryissue(strName, "123456", didsymbol, assetsymbol, 10000));
                mining(rpc, 1);

                fun("issuecert");
                String certSymbol = strName + ".cert";
                fun(rpc.issuecert(strName, "123456", didsymbol, certSymbol, "NAMING"));
                mining(rpc, 1);

                fun("transfercert");
                fun(rpc.transfercert(strName, "123456", "BLACKHOLE", certSymbol, "NAMING"));
                mining(rpc, 1);

                fun("burn");
                fun(rpc.burn(strName, "123456", assetsymbol, 10000));
                mining(rpc, 1);

                rpc.deleteaccount(strName, "123456", mnemonic.Last());

            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);

            }

            file.Close();

        }

        [TestMethod]
        public void test_multisigtx()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName1 = "test_1_" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName1, "123456");
                List<String> mnemonic1 = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address1 = JObject.Parse(ret)["addresses"][0].ToString();
                String publickey1 = JObject.Parse(rpc.getpublickey(strName1, "123456", address1))["public_key"].ToString();


                String strName2 = "test_2_" + GetTimestamp().ToString();
                ret = rpc.getnewaccount(strName2, "123456");
                List<String> mnemonic2 = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address2 = JObject.Parse(ret)["addresses"][0].ToString();
                String publickey2 = JObject.Parse(rpc.getpublickey(strName2, "123456", address2))["public_key"].ToString();

                ret = rpc.getnewmultisig(strName1, "123456", 2, 2, publickey1, new List<string> { publickey2 }, strName1 + " and " + strName2 + " multi-sig address");
                String muladdr1 = JObject.Parse(ret)["address"].ToString();
                ret = rpc.getnewmultisig(strName2, "123456", 2, 2, publickey2, new List<string> { publickey1 }, strName1 + " and " + strName2 + " multi-sig address");
                String muladdr2 = JObject.Parse(ret)["address"].ToString();

                rpc.send("lxf", "123", muladdr1, 1000000);


                mining(rpc, 2);

                fun("createmultisigtx:");
                ret = rpc.createmultisigtx(strName1, "123456", muladdr1, "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", 10000);
                fun(ret);

                fun("signmultisigtx:");
                fun(rpc.signmultisigtx(strName2, "123456", ret, null, true));
                mining(rpc, 2);

                fun("deletemultisig:");
                fun(rpc.deletemultisig(strName1, "123456", muladdr1));

                fun("deletemultisig:");
                fun(rpc.deletemultisig(strName2, "123456", muladdr2));

                fun("deleteaccount:");
                fun(rpc.deleteaccount(strName1, "123456", mnemonic1.Last()));

                fun("deleteaccount:");
                fun(rpc.deleteaccount(strName2, "123456", mnemonic2.Last()));

            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();
        }

        [TestMethod]
        public void test_etp()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName = "test_" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                fun("getnewaccount " + strName + " 123456");
                fun(ret);
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address = JObject.Parse(ret)["addresses"][0].ToString();

                fun("getbalance:");
                fun(rpc.getbalance(strName, "123456"));

                fun("listbalance:");
                fun(rpc.getbalance(strName, "123456"));

                fun("send:");
                fun(rpc.send("lxf", "123", address, 10000));

                fun("send fee=1000000:");
                fun(rpc.send("lxf", "123", address, 1000000));

                fun("sendfrom:");
                fun(rpc.sendfrom("lxf", "123", "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", address, 1000000));

                List<String> addresses = JsonExtension.JsonToObject<List<String>>(rpc.getnewaddress(strName, "123456", 2));

                fun("sendmore:");
                fun(rpc.sendmore("lxf", "123", addresses.Select(p => p + ":10000").ToList()));

                fun("sendmore change:");
                fun(rpc.sendmore("lxf", "123", addresses.Select(p => p + ":10000").ToList(), address));

                mining(rpc, 2);
                rpc.deleteaccount(strName, "123456", mnemonic.Last());
            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();


        }

        [TestMethod]
        public void test_transaction()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName = "test_" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                fun("getnewaccount " + strName + " 123456");
                fun(ret);
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();
                String address = JObject.Parse(ret)["addresses"][0].ToString();

                String hash = JObject.Parse(rpc.send("lxf", "123", address, 10000))["hash"].ToString();
                rpc.stopmining();
                mining(rpc, 2);

                fun("gettx:");
                fun(rpc.gettx(hash));

                fun("listtxs:");
                fun(rpc.listtxs(strName, "123456", "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", new Tuple<ulong, ulong>(1000, 1001)));


                fun("deleteaccount:");
                fun(rpc.deleteaccount(strName, "123456", mnemonic.Last()));

            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();
        }

        [TestMethod]
        public void test_blockchain()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                fun("getinfo:");
                fun(rpc.getinfo());

                fun("getheight:");
                fun(rpc.getheight());

                fun("getpeerinfo:");
                fun(rpc.getpeerinfo());

                fun("getmininginfo:");
                fun(rpc.getmininginfo());

                fun("getmemorypool:");
                fun(rpc.getmemorypool());
            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();



        }

        [TestMethod]
        public void test_account()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                String strName = "test_" + GetTimestamp().ToString();
                String ret = rpc.getnewaccount(strName, "123456");
                fun("getnewaccount " + strName + " 123456");
                fun(ret);
                List<String> mnemonic = JObject.Parse(ret)["mnemonic"].ToString().Split(' ').ToList();


                fun("getnewaddress:");
                fun(rpc.getnewaddress(strName, "123456", 2));

                fun("listaddresses:");
                fun(rpc.listaddresses(strName, "123456"));
                fun("validateaddress:");
                fun(rpc.validateaddress("MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo"));
                fun("validateaddress:");
                fun(rpc.validateaddress("35cY636TPTfFW8PxhqH3BNRL54g1T4mbR2"));

                fun("changepasswd:");
                fun(rpc.changepasswd(strName, "123456", "12345678"));
                fun("getaccount:");
                fun(rpc.getaccount(strName, "12345678", mnemonic.Last()));
                fun("deleteaccount:");
                fun(rpc.deleteaccount(strName, "12345678", mnemonic.Last()));
            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();


        }

        [TestMethod]
        public void test_block()
        {
            RPC rpc = new RPC(url); 
            String filePath = Directory.GetCurrentDirectory() + "\\test\\";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += System.Reflection.MethodBase.GetCurrentMethod().Name + ".txt";

            StreamWriter file = new StreamWriter(filePath, false);
            Action<String> fun = (String str) => file.WriteLine(str);

            try
            {
                fun("getblockheader 290203d32fde2338044556319318dc33482767028aaedbf5ae9d2a5fd31d75c5:");
                fun(rpc.getblockheader("290203d32fde2338044556319318dc33482767028aaedbf5ae9d2a5fd31d75c5"));
                fun("getblockheader null 1:");
                fun(rpc.getblockheader(null, 1));

                fun("getblock 290203d32fde2338044556319318dc33482767028aaedbf5ae9d2a5fd31d75c5:");
                fun(rpc.getblock("290203d32fde2338044556319318dc33482767028aaedbf5ae9d2a5fd31d75c5"));
                fun("getblock 1:");
                fun(rpc.getblock("1"));
                fun("getblock 1 json=false:");
                fun(rpc.getblock("1", false));
                fun("getblock 1 tx_json=false:");
                fun(rpc.getblock("1", null, false));

            }
            catch (Exception e)
            {
                String strOut = e.Message;
                if (e.InnerException != null)
                {
                    strOut = e.InnerException.Message;
                }
                fun(strOut);
                Assert.Fail(strOut);
            }

            file.Close();
        }

        public static double GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return ts.TotalMilliseconds;
        }

        public static void mining(RPC rpc, ushort height = 1)
        {
            UInt64 start = Convert.ToUInt64(rpc.getheight());
            UInt64 end = start + height;
            while (true)
            {
                try
                {
                    rpc.startmining("lxf", "123", "MLasJFxZQnA49XEvhTHmRKi2qstkj9ppjo", height);
                    break;
                }
                catch (Exception)
                {
                    rpc.stopmining();
                }
            }
            while (Convert.ToUInt64(rpc.getheight()) < end)
                Thread.Sleep(100);
        }
    }
}
