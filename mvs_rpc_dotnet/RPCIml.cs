using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mvs_rpc
{
    public partial class RPC
    {
        //////////////BLOCK//////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// get block header from hash or height
        /// </summary>
        /// <param name="hash" >The Base16 block hash.</param>
        /// <param name="height">The block height.</param>
        /// <returns></returns>
        public String getblockheader(String hash = null, UInt32? height = null)
        {
            List<String> parameters = new List<String>() { };

            if (hash != null) parameters.AddRange(new List<String> { "--hash", hash.ToString() });
            if (height != null) parameters.AddRange(new List<String> { "--height", height.ToString() });
            return getResult<String>("getblockheader", parameters);
        }


        /// <summary>
        /// get block detail from hash or height
        /// </summary>
        /// <param name="HASH_OR_HEIGH">block hash or block height</param>
        /// <param name="json">Json/Raw format, default is '--json=true'.</param>
        /// <param name="tx_json">Json/Raw format for txs, default is '--tx_json=true'.</param>
        /// <returns>rawtx when json = false , txs use rawtx when tx_json = false </returns>
        public String getblock(String HASH_OR_HEIGH, Boolean? json = null, Boolean? tx_json = null)
        {
            List<String> parameters = new List<String>() { HASH_OR_HEIGH };
            if (json != null) parameters.AddRange(new List<String> { "--json", json.ToString() });
            if (tx_json != null) parameters.AddRange(new List<String> { "--tx_json", json.ToString() });


            return getResult<String>("getblock", parameters);
        }
        //////////////BLOCK END//////////////////////////////////////////////////////////////////////////////////

        //////////////ACCOUNT//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// add account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="language">Options are 'en', 'es', 'ja', 'zh_Hans', 'zh_Hant' and 'any', defaults to 'en'.</param>
        /// <returns>account detail</returns>
        public String getnewaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String language = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            if (language != null) parameters.AddRange(new List<String> { "--language", language.ToString() });
            return getResult<String>("getnewaccount", parameters);
        }

        /// <summary>
        /// add new addresses 
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="number">The number of addresses to be generated, defaults to 1.</param>
        /// <returns> new addresses list</returns>
        public String getnewaddress(String ACCOUNTNAME, String ACCOUNTAUTH, UInt32? number = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            if (number != null) parameters.AddRange(new List<String> { "--number", number.ToString() });
            return getResult<String>("getnewaddress", parameters);
        }


        /// <summary>
        /// list addresses of account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns>all of address</returns>
        public String listaddresses(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };


            return getResult<String>("listaddresses", parameters);
        }


        /// <summary>
        /// vaild address
        /// </summary>
        /// <param name="PAYMENT_ADDRESS">PAYMENT_ADDRESS(std::string): "Valid payment address.</param>
        /// <returns></returns>
        public String validateaddress(String PAYMENT_ADDRESS)
        {
            List<String> parameters = new List<String>() { PAYMENT_ADDRESS };

            return getResult<String>("validateaddress", parameters);
        }

        /// <summary>
        /// import account from mnemonic
        /// </summary>
        /// <param name="WORD">The set of words that that make up the mnemonic</param>
        /// <param name="accountname">Account name required.</param>
        /// <param name="password">Account password(authorization) required.</param>
        /// <param name="language">The language identifier of the dictionary of the mnemonic. Options are 'en', 'es', 'ja', 'zh_Hans', 'zh_Hant' and 'any', defaults to 'any'.</param>
        /// <param name="hd_index">The HD index for the account.</param>
        /// <returns></returns>
        public String importaccount(String WORD, String accountname, String password, String language = null, UInt32? hd_index = null)
        {
            List<String> parameters = new List<String>() { WORD, "--accountname", accountname.ToString(), "--password", password.ToString() };

            if (language != null) parameters.AddRange(new List<String> { "--language", language.ToString() });
            if (hd_index != null) parameters.AddRange(new List<String> { "--hd_index", hd_index.ToString() });

            return getResult<String>("importaccount", parameters);
        }


        /// <summary>
        /// import account from keyfile
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="FILE">key file path.</param>
        /// <returns></returns>
        public String importkeyfile(String ACCOUNTNAME, String ACCOUNTAUTH, String FILE)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FILE };

            return getResult<String>("importkeyfile", parameters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="LASTWORD">The last word of your master private-key phrase.</param>
        /// <param name="DESTINATION">The keyfile storage path to.</param>
        /// <param name="data">If specified, the keyfile content will be append to the report, rather than to local file specified by DESTINATION.</param>
        /// <returns></returns>
        public String dumpkeyfile(String ACCOUNTNAME, String ACCOUNTAUTH, String LASTWORD, String DESTINATION = null, Boolean? data = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, LASTWORD, DESTINATION };
            if (data == true) parameters.Add("--data");

            return getResult<String>("dumpkeyfile", parameters);
        }


        /// <summary>
        /// change password of account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="password">The new password.</param>
        /// <returns></returns>
        public String changepasswd(String ACCOUNTNAME, String ACCOUNTAUTH, String password)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, "--password", password.ToString() };

            return getResult<String>("changepasswd", parameters);
        }


        /// <summary>
        /// delete account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="LASTWORD">The last word of your private-key phrase.</param>
        /// <returns></returns>
        public String deleteaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String LASTWORD)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, LASTWORD };

            return getResult<String>("deleteaccount", parameters);
        }


        /// <summary>
        /// get account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="LASTWORD">The last word of your backup words.</param>
        /// <returns>account detail</returns>
        public String getaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String LASTWORD)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, LASTWORD };


            return getResult<String>("getaccount", parameters);
        }
        //////////////ACCOUNT END//////////////////////////////////////////////////////////////////////////////////

        //////////////BLOCKCHAIN//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// shutdown the service
        /// </summary>
        /// <param name="ADMINNAME">admin name.</param>
        /// <param name="ADMINAUTH">admin password/authorization.</param>
        /// <returns></returns>
        public String shutdown(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });

            return getResult<String>("shutdown", parameters);
        }

 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADMINNAME">Administrator required.</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns></returns>
        public String getinfo(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });

            return getResult<String>("getinfo", parameters);
        }


        /// <summary>
        /// get block height
        /// </summary>
        /// <param name="ADMINNAME">Administrator required.</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns></returns>
        public String getheight(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });


            return getResult<String>("getheight", parameters);
        }


        /// <summary>
        /// get connect peers info
        /// </summary>
        /// <param name="ADMINNAME">Administrator required.</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns>peer list</returns>
        public String getpeerinfo(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });

            return getResult<String>("getpeerinfo", parameters);
        }


        /// <summary>
        /// get mining info of service
        /// </summary>
        /// <param name="ADMINNAME">Administrator required.</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns></returns>
        public String getmininginfo(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });

            return getResult<String>("getmininginfo", parameters);
        }

        /// <summary>
        /// start mining
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="address">The mining target address. Defaults to empty, means a new address will be generated.</param>
        /// <param name="number">The number of mining blocks, useful for testing. Defaults to 0, means no limit.</param>
        /// <returns></returns>
        public String startmining(String ACCOUNTNAME, String ACCOUNTAUTH, String address = null, UInt16? number = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            if (address != null) parameters.AddRange(new List<String> { "--address", address.ToString() });
            if (number != null) parameters.AddRange(new List<String> { "--number", number.ToString() });
            return getResult<String>("startmining", parameters);
        }


        /// <summary>
        /// stopm ining
        /// </summary>
        /// <param name="ADMINNAME">Administrator required.(when administrator_required in mvs.conf is set true)</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns></returns>
        public String stopmining(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });

            return getResult<String>("stopmining", parameters);
        }

        /// <summary>
        /// getwork
        /// </summary>
        /// <param name="ADMINNAME">Administrator required.(when administrator_required in mvs.conf is set true)</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns></returns>
        public String getwork(String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null || ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });

            return getResult<String>("getwork", parameters);
        }


        /// <summary>
        /// add or ban node 
        /// </summary>
        /// <param name="NODEADDRESS">The target node address[x.x.x.x:port].</param>
        /// <param name="ADMINNAME">admin name</param>
        /// <param name="ADMINAUTH">admin password/authorization.</param>
        /// <param name="operation">The operation[ add|ban ] to the target node address. default: add.</param>
        /// <returns></returns>
        public String addnode(String NODEADDRESS, String ADMINNAME = null, String ADMINAUTH = null, String operation = null)
        {
            List<String> parameters = new List<String>() { NODEADDRESS };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });


            if (operation != null) parameters.AddRange(new List<String> { "--operation", operation.ToString() });
            return getResult<String>("addnode", parameters);
        }


        /// <summary>
        /// set mining account and payment address
        /// </summary>
        /// <param name="ACCOUNTNAME"> Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="PAYMENT_ADDRESS">the payment address of this account.</param>
        /// <returns></returns>
        public String setminingaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String PAYMENT_ADDRESS)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, PAYMENT_ADDRESS };


            return getResult<String>("setminingaccount", parameters);
        }

        /// <summary>
        /// submit work
        /// </summary>
        /// <param name="NONCE">nonce. without leading 0x</param>
        /// <param name="HEADERHASH">header hash. with leading 0x</param>
        /// <param name="MIXHASH">mix hash. with leading 0x</param>
        /// <returns></returns>
        public String submitwork(String NONCE, String HEADERHASH, String MIXHASH)
        {
            List<String> parameters = new List<String>() { NONCE, HEADERHASH, MIXHASH };


            return getResult<String>("submitwork", parameters);
        }


        /// <summary>
        /// get memorypool of transactions
        /// </summary>
        /// <param name="json">Json format or Raw format, default is Json(true).</param>
        /// <param name="ADMINNAME">Administrator required.(when administrator_required in mvs.conf is set true)</param>
        /// <param name="ADMINAUTH">Administrator password required.</param>
        /// <returns>transactions list</returns>
        public String getmemorypool(Boolean? json = null, String ADMINNAME = null, String ADMINAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ADMINNAME != null && ADMINAUTH != null)
                parameters.AddRange(new List<String> { ADMINNAME, ADMINAUTH });


            if (json != null) parameters.AddRange(new List<String> { "--json", json.ToString() });
            return getResult<String>("getmemorypool", parameters);
        }
        //////////////BLOCKCHAIN END//////////////////////////////////////////////////////////////////////////////////

        //////////////ETP//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// get balance of account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns></returns>
        public String getbalance(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<String>("getbalance", parameters);
        }

        /// <summary>
        /// list balances under the account addresses 
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="nozero">Defaults to false.</param>
        /// <param name="greater_equal">Greater than ETP bits.</param>
        /// <param name="lesser_equal">Lesser than ETP bits.</param>
        /// <returns></returns>
        public String listbalances(String ACCOUNTNAME, String ACCOUNTAUTH, Boolean? nozero = null, UInt64? greater_equal = null, UInt64? lesser_equal = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            if (nozero != null && nozero == true) parameters.Add("--nozero");
            if (greater_equal != null) parameters.AddRange(new List<String> { "--greater_equal", greater_equal.ToString() });
            if (lesser_equal != null) parameters.AddRange(new List<String> { "--lesser_equal", lesser_equal.ToString() });
            return getResult<String>("listbalances", parameters);
        }


        /// <summary>
        /// get etp of the address
        /// </summary>
        /// <param name="PAYMENT_ADDRESS">The payment address. If not specified the address is read from STDIN.</param>
        /// <returns></returns>
        public String getaddressetp(String PAYMENT_ADDRESS)
        {
            List<String> parameters = new List<String>() { PAYMENT_ADDRESS };


            return getResult<String>("getaddressetp", parameters);
        }


        /// <summary>
        /// deposit etp under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="AMOUNT">ETP integer bits.</param>
        /// <param name="address">The deposit target address.</param>
        /// <param name="deposit">Deposits support [7, 30, 90, 182, 365] days. defaluts to 7 days</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns></returns>
        public String deposit(String ACCOUNTNAME, String ACCOUNTAUTH, UInt64 AMOUNT, String address = null, UInt16? deposit = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, AMOUNT.ToString() };

            if (address != null) parameters.AddRange(new List<String> { "--address", address.ToString() });
            if (deposit != null) parameters.AddRange(new List<String> { "--deposit", deposit.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("deposit", parameters);
        }


        /// <summary>
        /// send etp to address
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TOADDRESS">Send to this address</param>
        /// <param name="AMOUNT">"ETP integer bits.</param>
        /// <param name="memo">Attached memo for this transaction.</param>
        /// <param name="fee">Transaction fee. defaults to 10000 etp bits</param>
        /// <returns>transcation</returns>
        public String send(String ACCOUNTNAME, String ACCOUNTAUTH, String TOADDRESS, UInt64 AMOUNT, String memo = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TOADDRESS, AMOUNT.ToString() };

            if (memo != null) parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("send", parameters);
        }


        /// <summary>
        /// send etp from address
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="FROMADDRESS">Send from this address</param>
        /// <param name="TOADDRESS">Send to this address</param>
        /// <param name="AMOUNT">ETP integer bits.</param>
        /// <param name="memo">The memo to descript transaction</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transcation</returns>
        public String sendfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROMADDRESS, String TOADDRESS, UInt64 AMOUNT, String memo = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROMADDRESS, TOADDRESS, AMOUNT.ToString() };

            if (memo != null) parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("sendfrom", parameters);
        }

        /// <summary>
        /// Send etp to multiple addresses
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH"> Account password(authorization) required.</param>
        /// <param name="receivers">Send to [address:etp_bits].</param>
        /// <param name="mychange">Mychange to this address</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transcation</returns>
        public String sendmore(String ACCOUNTNAME, String ACCOUNTAUTH, List<String> receivers, String mychange = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            foreach (var i in receivers)
                parameters.AddRange(new List<String> { "--receivers", i.ToString() });

            if (mychange != null) parameters.AddRange(new List<String> { "--mychange", mychange.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("sendmore", parameters);
        }

        //////////////ETP END//////////////////////////////////////////////////////////////////////////////////

        //////////////TRANSCATION//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// get transaction
        /// </summary>
        /// <param name="HASH">The Base16 transaction hash of the transaction to get.</param>
        /// <param name="json">Json/Raw format, default is '--json=true'.</param>
        /// <returns>transaction or rawtx</returns>
        public String gettx(String HASH, Boolean? json = null)
        {
            List<String> parameters = new List<String>() { HASH };
            if (json != null)
                parameters.Add("--json");

            return getResult<String>("gettx", parameters);
        }


        /// <summary>
        /// list transcations
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="address">Address</param>
        /// <param name="height">Get tx according height eg: -e start-height:end-height will return tx between [start-height, end-height)</param>
        /// <param name="symbol">Asset symbol.</param>
        /// <param name="limit">Transaction count per page.</param>
        /// <param name="index">Page index.</param>
        /// <returns>transcations</returns>
        public String listtxs(String ACCOUNTNAME, String ACCOUNTAUTH, String address = null, Tuple<UInt64, UInt64> height = null, String symbol = null, UInt64? limit = null, UInt64? index = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            if (address != null) parameters.AddRange(new List<String> { "--address", address.ToString() });
            if (height != null) parameters.AddRange(new List<String> { "--height", String.Format("{0}:{1}", height.Item1, height.Item2) });
            if (symbol != null) parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            if (limit != null) parameters.AddRange(new List<String> { "--limit", limit.ToString() });
            if (index != null) parameters.AddRange(new List<String> { "--index", index.ToString() });
            return getResult<String>("listtxs", parameters);
        }
        //////////////TRANSCATION END//////////////////////////////////////////////////////////////////////////////////

        //////////////ASSET //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// create asset
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="symbol">The asset symbol, global uniqueness, only supports UPPER-CASE alphabet and dot(.), eg: CHENHAO.LAPTOP, dot separates prefix 'CHENHAO', It's impossible to create any asset named with 'CHENHAO' prefix, but this issuer.</param>
        /// <param name="issuer">Issue must be specified as a DID symbol.</param>
        /// <param name="volume">The asset maximum supply volume, with unit of integer bits.</param>
        /// <param name="rate">The percent threshold value when you secondary issue.Defaults to 0. 0, not allowed to secondary issue;-1,  the asset can be secondary issue freely;[1, 100], the asset can be secondary issue when own percentage greater than or equal to this value.</param>
        /// <param name="decimalnumber">The asset amount decimal number, defaults to 0.</param>
        /// <param name="description">The asset data chuck, defaults to empty string.</param>
        /// <returns></returns>
        public String createasset(String ACCOUNTNAME, String ACCOUNTAUTH, String symbol, String issuer, UInt64 volume, Int32? rate = null, UInt32? decimalnumber = null, String description = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, "--symbol", symbol.ToString(), "--issuer", issuer.ToString(), "--volume", volume.ToString() };

            if (rate != null) parameters.AddRange(new List<String> { "--rate", rate.ToString() });
            if (decimalnumber != null) parameters.AddRange(new List<String> { "--decimalnumber", decimalnumber.ToString() });
            if (description != null) parameters.AddRange(new List<String> { "--description", description.ToString() });
            return getResult<String>("createasset", parameters);
        }



        /// <summary>
        /// delete local asset
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="symbol">The asset symbol/name. Global unique.</param>
        /// <returns></returns>
        public String deletelocalasset(String ACCOUNTNAME, String ACCOUNTAUTH, String symbol)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, "--symbol", symbol.ToString() };

            return getResult<String>("deletelocalasset", parameters);
        }

        /// <summary>
        /// issue asset
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="SYMBOL">The asset symbol, global uniqueness, only supports UPPER-CASE alphabet and dot(.)</param>
        /// <param name="model">The token offering model by block height. 
        /// TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
        /// LQ - Locked Quantity each period;
        /// LP - Locked Period, numeber of how many blocks;
        /// UN - Unlock Number, number of how many LPs;
        /// eg: 
        ///         TYPE=1;LQ=9000;LP=60000;UN=3  
        ///         TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
        ///     defaults to disable.
        /// </param>
        /// <param name="fee">The fee of tx. minimum is 10 etp.</param>
        /// <returns>transcation</returns>
        public String issue(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL, String model = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, SYMBOL };

            if (model != null) parameters.AddRange(new List<String> { "--model", model.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("issue", parameters);
        }



        /// <summary>
        /// get assets under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="SYMBOL">Asset symbol.</param>
        /// <returns>assets</returns>
        public String getaccountasset(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            if (SYMBOL != null)
                parameters.Add(SYMBOL);

            return getResult<String>("getaccountasset", parameters);
        }


        /// <summary>
        /// get certs under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="SYMBOL">Cert symbol.</param>
        /// <returns>certs</returns>
        public String getaccountcert(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, "--cert" };
            if (SYMBOL != null)
                parameters.Add(SYMBOL);

            return getResult<String>("getaccountasset", parameters);
        }


        /// <summary>
        /// get assets under the address
        /// </summary>
        /// <param name="ADDRESS">address</param>
        /// <returns>assets</returns>
        public String getaddressasset(String ADDRESS)
        {
            List<String> parameters = new List<String>() { ADDRESS };

            return getResult<String>("getaddressasset", parameters);
        }


        /// <summary>
        /// get certs under the address
        /// </summary>
        /// <param name="ADDRESS">address</param>
        /// <returns>certs</returns>
        public String getaddresscert(String ADDRESS)
        {
            List<String> parameters = new List<String>() { ADDRESS, "--cert" };

            return getResult<String>("getaddressasset", parameters);
        }



        /// <summary>
        /// get asset by symbol
        /// </summary>
        /// <param name="SYMBOL">Asset symbol. If not specified, will show whole network asset symbols.</param>
        /// <returns>assets</returns>
        public String getasset(String SYMBOL=null)
        {
            List<String> parameters = new List<String>() {  };
            if (SYMBOL != null)
                parameters.Add(SYMBOL);

            return getResult<String>("getasset", parameters);
        }
 
        /// <summary>
        /// get cert by symbol
        /// </summary>
        /// <param name="SYMBOL">Cert symbol. If not specified, will show whole network cert symbols.</param>
        /// <returns>certs</returns>
        public String getcert(String SYMBOL=null)
        {
            List<String> parameters = new List<String>() { };
            if (SYMBOL != null)
                parameters.Add(SYMBOL);
            parameters.Add("--cert");

            return getResult<String>("getasset", parameters);
        }


        /// <summary>
        /// list assets under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns>assets</returns>
        public String listassets(String ACCOUNTNAME = null, String ACCOUNTAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ACCOUNTNAME != null && ACCOUNTAUTH != null)
                parameters.AddRange(new List<String> { ACCOUNTNAME, ACCOUNTAUTH });

            return getResult<String>("listassets", parameters);
        }


        /// <summary>
        /// list certs under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns>certs</returns>
        public String listcerts(String ACCOUNTNAME = null, String ACCOUNTAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ACCOUNTNAME != null && ACCOUNTAUTH != null)
                parameters.AddRange(new List<String> { ACCOUNTNAME, ACCOUNTAUTH });
            parameters.Add("--cert");

            return getResult<String>("listassets", parameters);
        }

        /// <summary>
        /// send asset from address 
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="FROMADDRESS">From address</param>
        /// <param name="TOADDRESS">Target address</param>
        /// <param name="SYMBOL">Asset symbol</param>
        /// <param name="AMOUNT">Asset integer bits. see asset decimal_number</param>
        /// <param name="model">The token offering model by block height. 
        /// TYPE=1 - fixed quantity model; TYPE=2 - specify parameters;
        /// LQ - Locked Quantity each period;
        /// LP - Locked Period, numeber of how many blocks;
        /// UN - Unlock Number, number of how many LPs;
        /// eg: 
        ///         TYPE=1;LQ=9000;LP=60000;UN=3  
        ///         TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
        ///     defaults to disable.
        /// </param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String sendassetfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROMADDRESS, String TOADDRESS, String SYMBOL, UInt64 AMOUNT, String model = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROMADDRESS, TOADDRESS, SYMBOL, AMOUNT.ToString() };

            if (model != null) parameters.AddRange(new List<String> { "--model", model.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("sendassetfrom", parameters);
        }


        /// <summary>
        /// send asset 
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="ADDRESS">From address</param>
        /// <param name="SYMBOL">Asset symbol</param>
        /// <param name="AMOUNT">Asset integer bits. see asset decimal_number</param>
        /// <param name="model">The token offering model by block height. 
        /// TYPE=1 - fixed quantity model; TYPE=2 - specify parameters;
        /// LQ - Locked Quantity each period;
        /// LP - Locked Period, numeber of how many blocks;
        /// UN - Unlock Number, number of how many LPs;
        /// eg: 
        ///         TYPE=1;LQ=9000;LP=60000;UN=3  
        ///         TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
        ///     defaults to disable.
        /// </param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String sendasset(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS, String SYMBOL, UInt64 AMOUNT, String model = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS, SYMBOL, AMOUNT.ToString() };

            if (model != null) parameters.AddRange(new List<String> { "--model", model.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("sendasset", parameters);
        }

        /// <summary>
        /// secondary issue asset
        /// </summary>
        /// <param name="ACCOUNTNAME"> Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TODID">target did to check and issue asset, fee from and mychange to the address of this did too.</param>
        /// <param name="SYMBOL">issued asset symbol</param>
        /// <param name="VOLUME">The volume of asset, with unit of integer bits.</param>
        /// <param name="model">The token offering model by block height.
        /// TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
        /// LQ - Locked Quantity each period;
        /// LP - Locked Period, numeber of how many blocks;
        /// UN - Unlock Number, number of how many LPs;
        /// eg: 
        ///         TYPE=1;LQ=9000;LP=60000;UN=3  
        ///         TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
        ///     defaults to disable.
        /// </param>
        /// <param name="fee">The fee of tx. default_value 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String secondaryissue(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, UInt64 VOLUME, String model = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL, VOLUME.ToString() };

            if (model != null) parameters.AddRange(new List<String> { "--model", model.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("secondaryissue", parameters);
        }


        /// <summary>
        /// issue cert
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TODID">The DID will own this cert.</param>
        /// <param name="SYMBOL">Asset Cert Symbol/Name.</param>
        /// <param name="CERT">Asset cert type name can be: ISSUE: cert of issuing asset, generated by issuing asset and used in secondaryissue asset.  DOMAIN: cert of domain, generated by issuing asset, the symbol is same as asset symbol(if it does not contain dot) or the prefix part(that before the first dot) of asset symbol. NAMING: cert of naming right of domain. The owner of domain cert can issue this type of cert by issuecert with symbol like “domain.XYZ”(domain is the symbol of domain cert).</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String issuecert(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, String CERT, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL, CERT };

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("issuecert", parameters);
        }


        /// <summary>
        /// transfer cert to did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TODID">Target did</param>
        /// <param name="SYMBOL">Asset cert symbol</param>
        /// <param name="CERT">Asset cert type name. eg. ISSUE, DOMAIN or NAMING</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String transfercert(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, String CERT, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL, CERT };

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("transfercert", parameters);
        }


        /// <summary>
        /// burn asset
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="SYMBOL">The asset will be burned</param>
        /// <param name="AMOUNT">Asset integer bits. see asset decimal_number</param>
        /// <returns>transaction</returns>
        public String burn(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL, UInt64 AMOUNT)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, SYMBOL, AMOUNT.ToString() };


            return getResult<String>("burn", parameters);
        }

        //////////////ASSET END //////////////////////////////////////////////////////////////////////////////////


        //////////////MULTISIGNATURE //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// create multisignature transaction
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="FROMADDRESS">Send from this address, must be a multi-signature script address.</param>
        /// <param name="TOADDRESS">Send to this address</param>
        /// <param name="AMOUNT">ETP integer bits.</param>
        /// <param name="symbol">asset name, not specify this option for etp tx</param>
        /// <param name="type_">Transaction type, defaults to 0. 0 -- transfer etp, 3 -- transfer asset</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>raw transaction</returns>
        public String createmultisigtx(String ACCOUNTNAME, String ACCOUNTAUTH, String FROMADDRESS, String TOADDRESS, UInt64 AMOUNT, String symbol = null, UInt16? type_ = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROMADDRESS, TOADDRESS, AMOUNT.ToString() };

            if (symbol != null) parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            if (type_ != null) parameters.AddRange(new List<String> { "--type_", type_.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("createmultisigtx", parameters);
        }


        /// <summary>
        /// get public key of address
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="ADDRESS">Address.</param>
        /// <returns>public key</returns>
        public String getpublickey(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS };


            return getResult<String>("getpublickey", parameters);
        }


        /// <summary>
        /// sign multisignature transaction
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH"> Account password(authorization) required.</param>
        /// <param name="TRANSACTION">The input Base16 transaction to sign.</param>
        /// <param name="selfpublickey">The private key of this public key will be used to sign.</param>
        /// <param name="broadcast">Broadcast the tx if it is fullly signed, disabled by default.</param>
        /// <returns>raw transaction</returns>
        public String signmultisigtx(String ACCOUNTNAME, String ACCOUNTAUTH, String TRANSACTION, String selfpublickey = null, Boolean? broadcast = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TRANSACTION };
            if (broadcast == true) parameters.Add("--broadcast");
            if (selfpublickey != null) parameters.AddRange(new List<String> { "--selfpublickey", selfpublickey.ToString() });
            return getResult<String>("signmultisigtx", parameters);
        }


        /// <summary>
        /// delete multisignature address
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="ADDRESS">The multisig script corresponding address.</param>
        /// <returns></returns>
        public String deletemultisig(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS };


            return getResult<String>("deletemultisig", parameters);
        }

        /// <summary>
        /// list multisignature address under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns>multisignature addresses</returns>
        public String listmultisig(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<String>("listmultisig", parameters);
        }


        /// <summary>
        /// create multisignature address
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="signaturenum">Account multisig signature number.</param>
        /// <param name="publickeynum">Account multisig public key number.</param>
        /// <param name="selfpublickey">the public key belongs to this account.</param>
        /// <param name="publickey">cosigner public key used for multisig</param>
        /// <param name="description">multisig record description.</param>
        /// <returns>new multisignature address</returns>
        public String getnewmultisig(String ACCOUNTNAME, String ACCOUNTAUTH, UInt16 signaturenum, UInt16 publickeynum, String selfpublickey, List<String> publickey, String description = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            parameters.AddRange(new List<String> { "--signaturenum", signaturenum.ToString() });
            parameters.AddRange(new List<String> { "--publickeynum", publickeynum.ToString() });
            parameters.AddRange(new List<String> { "--selfpublickey", selfpublickey.ToString() });
            foreach (var i in publickey)
            {
                parameters.AddRange(new List<String> { "--publickey", i.ToString() });
            }
            if (description != null) parameters.AddRange(new List<String> { "--description", description.ToString() });
            return getResult<String>("getnewmultisig", parameters);
        }

        //////////////MULTISIGNATURE END//////////////////////////////////////////////////////////////////////////////////

        //////////////RAWTX//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// create raw transaction
        /// </summary>
        /// <param name="type_">Transaction type. 0 -- transfer etp, 1 -- deposit etp, 3 -- transfer asset</param>
        /// <param name="senders">Send from addresses</param>
        /// <param name="receivers">Send to [address:amount]. amount is asset number if sybol option specified</param>
        /// <param name="symbol">asset name, not specify this option for etp tx</param>
        /// <param name="deposit">Deposits support [7, 30, 90, 182, 365] days. defaluts to 7 days</param>
        /// <param name="mychange">Mychange to this address, includes etp and asset change</param>
        /// <param name="message">Message/Information attached to this transaction</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>raw transaction</returns>
        public String createrawtx(UInt16 type_, List<String> senders, List<String> receivers, String symbol = null, UInt16? deposit = null, String mychange = null, String message = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { "--type", type_.ToString() };

            foreach (var i in senders)      
                parameters.AddRange(new List<String> { "--senders", i.ToString() });
            
            foreach (var i in receivers)
                parameters.AddRange(new List<String> { "--receivers", i.ToString() });
            
            if (symbol != null) parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            if (deposit != null) parameters.AddRange(new List<String> { "--deposit", deposit.ToString() });
            if (mychange != null) parameters.AddRange(new List<String> { "--mychange", mychange.ToString() });
            if (message != null) parameters.AddRange(new List<String> { "--message", message.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("createrawtx", parameters);
        }


        /// <summary>
        /// send raw transaction
        /// </summary>
        /// <param name="TRANSACTION">The input Base16 transaction to broadcast.</param>
        /// <param name="fee">The max tx fee. default_value 10 etp</param>
        /// <returns>raw transaction</returns>
        public String sendrawtx(String TRANSACTION, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { TRANSACTION };

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("sendrawtx", parameters);
        }

        /// <summary>
        /// sign raw transaction
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TRANSACTION">The input Base16 transaction to sign.</param>
        /// <returns>raw transaction</returns>
        public String signrawtx(String ACCOUNTNAME, String ACCOUNTAUTH, String TRANSACTION)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TRANSACTION };


            return getResult<String>("signrawtx", parameters);
        }


        /// <summary>
        /// decode raw transaction
        /// </summary>
        /// <param name="TRANSACTION">The input Base16 transaction to sign.</param>
        /// <returns>transaction</returns>
        public String decoderawtx(String TRANSACTION)
        {
            List<String> parameters = new List<String>() { TRANSACTION };


            return getResult<String>("decoderawtx", parameters);
        }

        //////////////RAWTX END//////////////////////////////////////////////////////////////////////////////////

        //////////////DID//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// register did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="ADDRESS">The address will be bound to, can change to other addresses later.</param>
        /// <param name="SYMBOL">The symbol of global unique MVS Digital Identity Destination/Index, supports alphabets/numbers/(“@”, “.”, “_”, “-“), case-sensitive, maximum length is 64.</param>
        /// <param name="fee">The fee of tx. defaults to 1 etp.</param>
        /// <returns>transaction</returns>
        public String registerdid(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS, String SYMBOL, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS, SYMBOL };

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("registerdid", parameters);
        }

        /// <summary>
        /// did change address
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TOADDRESS">Target address</param>
        /// <param name="DIDSYMBOL">Did symbol</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String didchangeaddress(String ACCOUNTNAME, String ACCOUNTAUTH, String TOADDRESS, String DIDSYMBOL, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TOADDRESS, DIDSYMBOL };

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("didchangeaddress", parameters);
        }

        /// <summary>
        /// send asset to address\did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TO_">Asset receiver did/address.</param>
        /// <param name="ASSET">Asset MST symbol.</param>
        /// <param name="AMOUNT">Asset integer bits. see asset decimal_number </param>
        /// <param name="model">The token offering model by block height. 
        /// TYPE=1 - fixed quantity model; TYPE=2 - specify parameters;
        /// LQ - Locked Quantity each period;
        /// LP - Locked Period, numeber of how many blocks;
        /// UN - Unlock Number, number of how many LPs;
        /// eg: 
        ///         TYPE=1;LQ=9000;LP=60000;UN=3  
        ///         TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
        ///     defaults to disable.
        /// </param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String didsendasset(String ACCOUNTNAME, String ACCOUNTAUTH, String TO_, String ASSET, UInt64 AMOUNT, String model = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TO_, ASSET, AMOUNT.ToString() };

            if (model != null) parameters.AddRange(new List<String> { "--model", model.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("didsendasset", parameters);
        }

        /// <summary>
        /// send asset from address\did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="FROM_">From did/address</param>
        /// <param name="TO_">Target did/address</param>
        /// <param name="SYMBOL">Asset MST symbol.</param>
        /// <param name="AMOUNT">Asset integer bits. see asset decimal_number </param>
        /// <param name="model">The token offering model by block height. 
        /// TYPE=1 - fixed quantity model; TYPE=2 - specify parameters;
        /// LQ - Locked Quantity each period;
        /// LP - Locked Period, numeber of how many blocks;
        /// UN - Unlock Number, number of how many LPs;
        /// eg: 
        ///         TYPE=1;LQ=9000;LP=60000;UN=3  
        ///         TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
        ///     defaults to disable.
        ///     </param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String didsendassetfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROM_, String TO_, String SYMBOL, UInt64 AMOUNT, String model = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROM_, TO_, SYMBOL, AMOUNT.ToString() };

            if (model != null) parameters.AddRange(new List<String> { "--model", model.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("didsendassetfrom", parameters);
        }

        /// <summary>
        /// list dids under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns></returns>
        public String listdids(String ACCOUNTNAME = null, String ACCOUNTAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ACCOUNTNAME != null && ACCOUNTAUTH != null)
                parameters.AddRange( new List<String> { ACCOUNTNAME, ACCOUNTAUTH });

            return getResult<String>("listdids", parameters);
        }

        /// <summary>
        /// send etp to address or did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TO_">Send to this did/address</param>
        /// <param name="AMOUNT">ETP integer bits.</param>
        /// <param name="memo">Attached memo for this transaction.</param>
        /// <param name="fee">Transaction fee. defaults to 10000 etp bits</param>
        /// <returns>transaction</returns>
        public String didsend(String ACCOUNTNAME, String ACCOUNTAUTH, String TO_, UInt64 AMOUNT, String memo = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TO_, AMOUNT.ToString() };

            if (memo != null) parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("didsend", parameters);
        }

        /// <summary>
        /// send  etp from address/did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="FROM_">Send from this did/address</param>
        /// <param name="TO_">Send to this did/address</param>
        /// <param name="AMOUNT">ETP integer bits.</param>
        /// <param name="memo">Attached memo for this transaction.</param>
        /// <param name="fee">Transaction fee. defaults to 10000 etp bits</param>
        /// <returns>transaction</returns>
        public String didsendfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROM_, String TO_, UInt64 AMOUNT, String memo = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROM_, TO_, AMOUNT.ToString() };

            if (memo != null) parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("didsendfrom", parameters);
        }


        /// <summary>
        /// send etp to multiple address/did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="receivers">Send to [did/address:etp_bits].</param>
        /// <param name="mychange">Mychange to this did/address</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String didsendmore(String ACCOUNTNAME, String ACCOUNTAUTH, List<String> receivers, String mychange = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            foreach (var i in receivers)
                parameters.AddRange(new List<String> { "--receivers", i.ToString() });

            if (mychange != null) parameters.AddRange(new List<String> { "--mychange", mychange.ToString() });
            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("didsendmore", parameters);
        }

        /// <summary>
        /// get did history addresses from did or address
        /// </summary>
        /// <param name="didOrAddress">Did symbol or standard address, if DidOrAddress == null display whole network DIDs.</param>
        /// <returns>history addresses or dids</returns>
        public String getdid(String didOrAddress=null)
        {
            List<String> parameters = new List<String>() { };
            if (didOrAddress != null)
                parameters.Add(didOrAddress);

            return getResult<String>("getdid", parameters);
        }

        //////////////DID END//////////////////////////////////////////////////////////////////////////////////

        //////////////MIT//////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// register mit 
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TODID">Target did</param>
        /// <param name="SYMBOL">MIT symbol</param>
        /// <param name="content">Content of MIT</param>
        /// <param name="mits">List of symbol and content pair. Symbol and content are separated by a ':'</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String registermit(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, String content = null, List<String> mits = null, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL };

            if (content != null) parameters.AddRange(new List<String> { "--content", content.ToString() });

            if (mits != null)
                foreach (var i in mits)
                    parameters.AddRange(new List<String> { "--mits", i.ToString() });

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("registermit", parameters);
        }

        /// <summary>
        /// transfer mit to did
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <param name="TODID">Target did</param>
        /// <param name="SYMBOL">Asset MIT symbol</param>
        /// <param name="fee">Transaction fee. defaults to 10000 ETP bits</param>
        /// <returns>transaction</returns>
        public String transfermit(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, UInt64? fee = null)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL };

            if (fee != null) parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("transfermit", parameters);
        }


        /// <summary>
        /// get mits
        /// </summary>
        /// <param name="SYMBOL">Asset symbol. If not specified then show whole network MIT symbols.</param>
        /// <param name="trace">If specified then trace the history. Default is not specified.</param>
        /// <param name="limit">MIT count per page.</param>
        /// <param name="index">Page index.</param>
        /// <param name="current">If specified then show the lastest information of specified MIT. Default is not specified.</param>
        /// <returns>mits</returns>
        public String getmit(String SYMBOL = null, Boolean? trace = null, UInt32? limit = null, UInt32? index = null, Boolean? current = null)
        {
            List<String> parameters = new List<String>() { SYMBOL };
            if (trace != null && trace == true) parameters.Add("--trace");
            if (current != null && current == true) parameters.Add("--current");
            if (limit != null) parameters.AddRange(new List<String> { "--limit", limit.ToString() });
            if (index != null) parameters.AddRange(new List<String> { "--index", index.ToString() });
            return getResult<String>("getmit", parameters);
        }


        /// <summary>
        /// list mits under the account
        /// </summary>
        /// <param name="ACCOUNTNAME">Account name required.</param>
        /// <param name="ACCOUNTAUTH">Account password(authorization) required.</param>
        /// <returns></returns>
        public String listmits(String ACCOUNTNAME = null, String ACCOUNTAUTH = null)
        {
            List<String> parameters = new List<String>() { };
            if (ACCOUNTNAME != null && ACCOUNTAUTH != null)
                parameters.AddRange(new List<String> { ACCOUNTNAME, ACCOUNTAUTH });

            return getResult<String>("listmits", parameters);
        }
        //////////////MIT END//////////////////////////////////////////////////////////////////////////////////

    }


}
