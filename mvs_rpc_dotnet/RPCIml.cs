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
        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TOADDRESS(std::string): "Target address"
            :param: DIDSYMBOL(std::string): "Did symbol"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction didchangeaddress(String ACCOUNTNAME, String ACCOUNTAUTH, String TOADDRESS, String DIDSYMBOL, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TOADDRESS, DIDSYMBOL };
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("didchangeaddress", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TRANSACTION(string of hexcode): "The input Base16 transaction to sign."
            :param: selfpublickey(std::string): "The private key of this public key will be used to sign."
            :param: broadcast(bool): "Broadcast the tx if it is fullly signed, disabled by default."
        */
        public String signmultisigtx(String ACCOUNTNAME, String ACCOUNTAUTH, String TRANSACTION, String selfpublickey, Boolean broadcast)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TRANSACTION };
            parameters.AddRange(new List<String> { "--selfpublickey", selfpublickey.ToString() });
            parameters.AddRange(new List<String> { "--broadcast", broadcast.ToString() });
            return getResult<String>("signmultisigtx", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: ADDRESS(std::string): "The address will be bound to, can change to other addresses later."
            :param: SYMBOL(std::string): "The symbol of global unique MVS Digital Identity Destination/Index, supports alphabets/numbers/(“@”, “.”, “_”, “-“), case-sensitive, maximum length is 64."
            :param: fee(uint64_t): "The fee of tx. defaults to 1 etp."
        */
        public transaction registerdid(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS, String SYMBOL, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS, SYMBOL };
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("registerdid", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: SYMBOL(std::string): "The asset symbol, global uniqueness, only supports UPPER-CASE alphabet and dot(.)"
            :param: model(std::string): The token offering model by block height. 
            TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
            LQ - Locked Quantity each period; 
            LP - Locked Period, numeber of how many blocks; 
            UN - Unlock Number, number of how many LPs; 
            eg: 
                TYPE=1;LQ=9000;LP=60000;UN=3  
                TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
            defaults to disable.
            :param: fee(uint64_t): "The fee of tx. minimum is 10 etp."
        */
        public transaction issue(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL, String model, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, SYMBOL };
            parameters.AddRange(new List<String> { "--model", model.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("issue", parameters);
        }


        /*
            :param: WORD(list of string): "The set of words that that make up the mnemonic. If not specified the words are read from STDIN."
            :param: language(explorer::config::language): "The language identifier of the dictionary of the mnemonic. Options are 'en', 'es', 'ja', 'zh_Hans', 'zh_Hant' and 'any', defaults to 'any'."
            :param: accountname(std::string): Account name required.
            :param: password(std::string): Account password(authorization) required.
            :param: hd_index(std::uint32_t): "The HD index for the account."
        */
        public accountEx importaccount(List<String> WORD, String language, String accountname, String password, UInt32 hd_index)
        {
            List<String> parameters = new List<String>() { String.Join(" ", WORD.ToArray()) };
            parameters.AddRange(new List<String> { "--language", language.ToString() });
            parameters.AddRange(new List<String> { "--accountname", accountname.ToString() });
            parameters.AddRange(new List<String> { "--password", password.ToString() });
            parameters.AddRange(new List<String> { "--hd_index", hd_index.ToString() });
            return getResult<accountEx>("importaccount", parameters);
        }


        /*
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public String stopmining(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<String>("stopmining", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: FROMADDRESS(std::string): "Send from this address, must be a multi-signature script address."
            :param: TOADDRESS(std::string): "Send to this address"
            :param: AMOUNT(uint64_t): "ETP integer bits."
            :param: symbol(std::string): "asset name, not specify this option for etp tx"
            :param: type_(uint16_t): "Transaction type, defaults to 0. 0 -- transfer etp, 3 -- transfer asset"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public String createmultisigtx(String ACCOUNTNAME, String ACCOUNTAUTH, String FROMADDRESS, String TOADDRESS, UInt64 AMOUNT, String symbol, UInt16 type_, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROMADDRESS, TOADDRESS, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            parameters.AddRange(new List<String> { "--type_", type_.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("createmultisigtx", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: ADDRESS(std::string): "Address."
        */
        public String getpublickey(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS };

            return getResult<String>("getpublickey", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: AMOUNT(uint64_t): "ETP integer bits."
            :param: address(std::string): "The deposit target address."
            :param: deposit(uint16_t): "Deposits support [7, 30, 90, 182, 365] days. defaluts to 7 days"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction deposit(String ACCOUNTNAME, String ACCOUNTAUTH, UInt64 AMOUNT, String address, UInt16 deposit, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--address", address.ToString() });
            parameters.AddRange(new List<String> { "--deposit", deposit.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("deposit", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: SYMBOL(std::string): "Asset symbol."
            :param: cert(bool): "If specified, then only get related asset cert. Default is not specified."
        */
        public List<asset> getaccountasset(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL, Boolean cert)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, SYMBOL };
            parameters.AddRange(new List<String> { "--cert", cert.ToString() });
            return getResult<List<asset>>("getaccountasset", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TO_(std::string): "Asset receiver did/address."
            :param: ASSET(std::string): "Asset MST symbol."
            :param: AMOUNT(uint64_t): "Asset integer bits. see asset <decimal_number>."
            :param: model(std::string): The token offering model by block height. 
            TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
            LQ - Locked Quantity each period; 
            LP - Locked Period, numeber of how many blocks; 
            UN - Unlock Number, number of how many LPs; 
            eg: 
                TYPE=1;LQ=9000;LP=60000;UN=3  
                TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
            defaults to disable.
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction didsendasset(String ACCOUNTNAME, String ACCOUNTAUTH, String TO_, String ASSET, UInt64 AMOUNT, String model, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TO_, ASSET, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--model", model.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("didsendasset", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: SYMBOL(std::string): "The asset will be burned."
            :param: AMOUNT(uint64_t): "Asset integer bits. see asset <decimal_number>."
        */
        public transaction burn(String ACCOUNTNAME, String ACCOUNTAUTH, String SYMBOL, UInt64 AMOUNT)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, SYMBOL, AMOUNT.ToString() };

            return getResult<transaction>("burn", parameters);
        }


        /*
            :param: height(uint32_t): "specify the starting point to pop out blocks. eg, if specified 1000000, then all blocks with height greater than or equal to 1000000 will be poped out."
        */
        public String popblock(UInt32 height)
        {
            List<String> parameters = new List<String>() { height.ToString() };

            return getResult<String>("popblock", parameters);
        }


        /*
            :param: nozero(bool): "Defaults to false."
            :param: greater_equal(uint64_t): "Greater than ETP bits."
            :param: lesser_equal(uint64_t): "Lesser than ETP bits."
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public List<balance> listbalances(Boolean nozero, UInt64 greater_equal, UInt64 lesser_equal, String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--nozero", nozero.ToString() });
            parameters.AddRange(new List<String> { "--greater_equal", greater_equal.ToString() });
            parameters.AddRange(new List<String> { "--lesser_equal", lesser_equal.ToString() });
            return getResult<List<balance>>("listbalances", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: rate(int32_t): "The percent threshold value when you secondary issue.              0,  not allowed to secondary issue;              -1,  the asset can be secondary issue freely;             [1, 100], the asset can be secondary issue when own percentage greater than or equal to this value.             Defaults to 0."
            :param: symbol(std::string): "The asset symbol, global uniqueness, only supports UPPER-CASE alphabet and dot(.), eg: CHENHAO.LAPTOP, dot separates prefix 'CHENHAO', It's impossible to create any asset named with 'CHENHAO' prefix, but this issuer."
            :param: issuer(std::string): "Issue must be specified as a DID symbol."
            :param: volume(non_negative_uint64): "The asset maximum supply volume, with unit of integer bits."
            :param: decimalnumber(uint32_t): "The asset amount decimal number, defaults to 0."
            :param: description(std::string): "The asset data chuck, defaults to empty string."
        */
        public asset createasset(String ACCOUNTNAME, String ACCOUNTAUTH, Int32 rate, String symbol, String issuer, UInt64 volume, UInt32 decimalnumber, String description)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--rate", rate.ToString() });
            parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            parameters.AddRange(new List<String> { "--issuer", issuer.ToString() });
            parameters.AddRange(new List<String> { "--volume", volume.ToString() });
            parameters.AddRange(new List<String> { "--decimalnumber", decimalnumber.ToString() });
            parameters.AddRange(new List<String> { "--description", description.ToString() });
            return getResult<asset>("createasset", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TOADDRESS(std::string): "Send to this address"
            :param: AMOUNT(uint64_t): "ETP integer bits."
            :param: memo(std::string): "Attached memo for this transaction."
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 etp bits"
        */
        public transaction send(String ACCOUNTNAME, String ACCOUNTAUTH, String TOADDRESS, UInt64 AMOUNT, String memo, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TOADDRESS, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("send", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: password(std::string): "The new password."
        */
        public accountstatus changepasswd(String ACCOUNTNAME, String ACCOUNTAUTH, String password)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--password", password.ToString() });
            return getResult<accountstatus>("changepasswd", parameters);
        }


        /*
            :param: type_(uint16_t): "Transaction type. 0 -- transfer etp, 1 -- deposit etp, 3 -- transfer asset"
            :param: senders(list of string): "Send from addresses"
            :param: receivers(list of string): "Send to [address:amount]. amount is asset number if sybol option specified"
            :param: symbol(std::string): "asset name, not specify this option for etp tx"
            :param: deposit(uint16_t): "Deposits support [7, 30, 90, 182, 365] days. defaluts to 7 days"
            :param: mychange(std::string): "Mychange to this address, includes etp and asset change"
            :param: message(std::string): "Message/Information attached to this transaction"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public String createrawtx(UInt16 type_, List<String> senders, List<String> receivers, String symbol, UInt16 deposit, String mychange, String message, UInt64 fee)
        {
            List<String> parameters = new List<String>() { };
            parameters.AddRange(new List<String> { "--type_", type_.ToString() });
            foreach (var i in senders)
            {
                parameters.AddRange(new List<String> { "--senders", i.ToString() });
            }
            foreach (var i in receivers)
            {
                parameters.AddRange(new List<String> { "--receivers", i.ToString() });
            }
            parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            parameters.AddRange(new List<String> { "--deposit", deposit.ToString() });
            parameters.AddRange(new List<String> { "--mychange", mychange.ToString() });
            parameters.AddRange(new List<String> { "--message", message.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("createrawtx", parameters);
        }


        /*
            :param: PAYMENT_ADDRESS(std::string): "Valid payment address. If not specified the address is read from STDIN."
        */
        public address validateaddress(String PAYMENT_ADDRESS)
        {
            List<String> parameters = new List<String>() { PAYMENT_ADDRESS };

            return getResult<address>("validateaddress", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: FROMADDRESS(std::string): "Send from this address"
            :param: TOADDRESS(std::string): "Send to this address"
            :param: AMOUNT(uint64_t): "ETP integer bits."
            :param: memo(std::string): "The memo to descript transaction"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction sendfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROMADDRESS, String TOADDRESS, UInt64 AMOUNT, String memo, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROMADDRESS, TOADDRESS, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("sendfrom", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: ADDRESS(std::string): "The multisig script corresponding address."
        */
        public mulsignature deletemultisig(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS };

            return getResult<mulsignature>("deletemultisig", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public List<did> listdids(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<List<did>>("listdids", parameters);
        }


        /*
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public UInt64 getheight(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<UInt64>("getheight", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TO_(std::string): "Send to this did/address"
            :param: AMOUNT(uint64_t): "ETP integer bits."
            :param: memo(std::string): "Attached memo for this transaction."
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 etp bits"
        */
        public transaction didsend(String ACCOUNTNAME, String ACCOUNTAUTH, String TO_, UInt64 AMOUNT, String memo, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TO_, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("didsend", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TODID(std::string): "Target did"
            :param: SYMBOL(std::string): "Asset cert symbol"
            :param: CERT(std::string): "Asset cert type name. eg. ISSUE, DOMAIN or NAMING"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction transfercert(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, String CERT, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL, CERT };
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("transfercert", parameters);
        }


        /*
            :param: TRANSACTION(string of hexcode): "The input Base16 transaction to broadcast."
            :param: fee(uint64_t): "The max tx fee. default_value 10 etp"
        */
        public String sendrawtx(String TRANSACTION, UInt64 fee)
        {
            List<String> parameters = new List<String>() { TRANSACTION };
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<String>("sendrawtx", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TODID(std::string): "The DID will own this cert."
            :param: SYMBOL(std::string): "Asset Cert Symbol/Name."
            :param: CERT(std::string): "Asset cert type name can be: ISSUE: cert of issuing asset, generated by issuing asset and used in secondaryissue asset.  DOMAIN: cert of domain, generated by issuing asset, the symbol is same as asset symbol(if it does not contain dot) or the prefix part(that before the first dot) of asset symbol. NAMING: cert of naming right of domain. The owner of domain cert can issue this type of cert by issuecert with symbol like “domain.XYZ”(domain is the symbol of domain cert)."
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction issuecert(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, String CERT, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL, CERT };
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("issuecert", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: NUMBER(std::string): "Block number, or earliest, latest or pending"
        */
        public String fetchheaderext(String ACCOUNTNAME, String ACCOUNTAUTH, String NUMBER)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, NUMBER };

            return getResult<String>("fetchheaderext", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: FROM_(std::string): "From did/address"
            :param: TO_(std::string): "Target did/address"
            :param: SYMBOL(std::string): "Asset symbol"
            :param: AMOUNT(uint64_t): "Asset integer bits. see asset <decimal_number>."
            :param: model(std::string): The token offering model by block height. 
            TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
            LQ - Locked Quantity each period; 
            LP - Locked Period, numeber of how many blocks; 
            UN - Unlock Number, number of how many LPs; 
            eg: 
                TYPE=1;LQ=9000;LP=60000;UN=3  
                TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
            defaults to disable.
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction didsendassetfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROM_, String TO_, String SYMBOL, UInt64 AMOUNT, String model, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROM_, TO_, SYMBOL, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--model", model.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("didsendassetfrom", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: receivers(list of string): "Send to [did/address:etp_bits]."
            :param: mychange(std::string): "Mychange to this did/address"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction didsendmore(String ACCOUNTNAME, String ACCOUNTAUTH, List<String> receivers, String mychange, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            foreach (var i in receivers)
            {
                parameters.AddRange(new List<String> { "--receivers", i.ToString() });
            }
            parameters.AddRange(new List<String> { "--mychange", mychange.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("didsendmore", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: receivers(list of string): "Send to [address:etp_bits]."
            :param: mychange(std::string): "Mychange to this address"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction sendmore(String ACCOUNTNAME, String ACCOUNTAUTH, List<String> receivers, String mychange, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            foreach (var i in receivers)
            {
                parameters.AddRange(new List<String> { "--receivers", i.ToString() });
            }
            parameters.AddRange(new List<String> { "--mychange", mychange.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("sendmore", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: symbol(std::string): "The asset symbol/name. Global unique."
        */
        public delasset deletelocalasset(String ACCOUNTNAME, String ACCOUNTAUTH, String symbol)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            return getResult<delasset>("deletelocalasset", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: address(std::string): "Address."
            :param: height(a range expressed by 2 integers): "Get tx according height eg: -e start-height:end-height will return tx between [start-height, end-height)"
            :param: symbol(std::string): "Asset symbol."
            :param: limit(uint64_t): "Transaction count per page."
            :param: index(uint64_t): "Page index."
        */
        public transactions listtxs(String ACCOUNTNAME, String ACCOUNTAUTH, String address, Tuple<UInt64, UInt64> height, String symbol, UInt64 limit, UInt64 index)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--address", address.ToString() });
            parameters.AddRange(new List<String> { "--height", String.Format("{0}:{1}", height.Item1, height.Item2) });
            parameters.AddRange(new List<String> { "--symbol", symbol.ToString() });
            parameters.AddRange(new List<String> { "--limit", limit.ToString() });
            parameters.AddRange(new List<String> { "--index", index.ToString() });
            return getResult<transactions>("listtxs", parameters);
        }


        /*
            :param: SYMBOL(std::string): "Asset symbol. If not specified then show whole network MIT symbols."
            :param: trace(bool): "If specified then trace the history. Default is not specified."
            :param: limit(uint32_t): "MIT count per page."
            :param: index(uint32_t): "Page index."
            :param: current(bool): "If specified then show the lastest information of specified MIT. Default is not specified."
        */
        public List<mit> getmit(String SYMBOL, Boolean trace, UInt32 limit, UInt32 index, Boolean current)
        {
            List<String> parameters = new List<String>() { SYMBOL };
            parameters.AddRange(new List<String> { "--trace", trace.ToString() });
            parameters.AddRange(new List<String> { "--limit", limit.ToString() });
            parameters.AddRange(new List<String> { "--index", index.ToString() });
            parameters.AddRange(new List<String> { "--current", current.ToString() });
            return getResult<List<mit>>("getmit", parameters);
        }


        /*
            :param: language(std::string): "Options are 'en', 'es', 'ja', 'zh_Hans', 'zh_Hant' and 'any', defaults to 'en'."
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public account getnewaccount(String language, String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--language", language.ToString() });
            return getResult<account>("getnewaccount", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public List<mit> listmits(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<List<mit>>("listmits", parameters);
        }


        /*
            :param: ADMINNAME(std::string): "admin name."
            :param: ADMINAUTH(std::string): "admin password/authorization."
        */
        public String shutdown(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<String>("shutdown", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TRANSACTION(string of hexcode): "The input Base16 transaction to sign."
        */
        public rawtx signrawtx(String ACCOUNTNAME, String ACCOUNTAUTH, String TRANSACTION)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TRANSACTION };

            return getResult<rawtx>("signrawtx", parameters);
        }


        /*
            :param: json(bool): "Json format or Raw format, default is Json(true)."
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public List<transaction> getmemorypool(Boolean json, String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };
            parameters.AddRange(new List<String> { "--json", json.ToString() });
            return getResult<List<transaction>>("getmemorypool", parameters);
        }


        /*
            :param: hash(string of hash256): "The Base16 block hash."
            :param: height(uint32_t): "The block height."
        */
        public blockheader getblockheader(String hash, UInt32 height)
        {
            List<String> parameters = new List<String>() { };
            parameters.AddRange(new List<String> { "--hash", hash.ToString() });
            parameters.AddRange(new List<String> { "--height", height.ToString() });
            return getResult<blockheader>("getblockheader", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: cert(bool): "If specified, then only get related asset cert. Default is not specified."
        */
        public List<asset> listassets(String ACCOUNTNAME, String ACCOUNTAUTH, Boolean cert)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--cert", cert.ToString() });
            return getResult<List<asset>>("listassets", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: FROMADDRESS(std::string): "From address"
            :param: TOADDRESS(std::string): "Target address"
            :param: SYMBOL(std::string): "Asset symbol"
            :param: AMOUNT(uint64_t): "Asset integer bits. see asset <decimal_number>."
            :param: model(std::string): The token offering model by block height. 
            TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
            LQ - Locked Quantity each period; 
            LP - Locked Period, numeber of how many blocks; 
            UN - Unlock Number, number of how many LPs; 
            eg: 
                TYPE=1;LQ=9000;LP=60000;UN=3  
                TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
            defaults to disable.
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction sendassetfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROMADDRESS, String TOADDRESS, String SYMBOL, UInt64 AMOUNT, String model, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROMADDRESS, TOADDRESS, SYMBOL, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--model", model.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("sendassetfrom", parameters);
        }


        /*
            :param: SYMBOL(std::string): "Asset symbol. If not specified, will show whole network asset symbols."
            :param: cert(bool): "If specified, then only get related asset cert. Default is not specified."
        */
        public List<String> getasset(String SYMBOL, Boolean cert)
        {
            List<String> parameters = new List<String>() { SYMBOL };
            parameters.AddRange(new List<String> { "--cert", cert.ToString() });
            return getResult<List<String>>("getasset", parameters);
        }


        /*
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public info getinfo(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<info>("getinfo", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TODID(std::string): "target did to check and issue asset, fee from and mychange to the address of this did too."
            :param: SYMBOL(std::string): "issued asset symbol"
            :param: VOLUME(uint64_t): "The volume of asset, with unit of integer bits."
            :param: model(std::string): The token offering model by block height. 
            TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
            LQ - Locked Quantity each period; 
            LP - Locked Period, numeber of how many blocks; 
            UN - Unlock Number, number of how many LPs; 
            eg: 
                TYPE=1;LQ=9000;LP=60000;UN=3  
                TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
            defaults to disable.
            :param: fee(uint64_t): "The fee of tx. default_value 10000 ETP bits"
        */
        public transaction secondaryissue(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, UInt64 VOLUME, String model, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL, VOLUME.ToString() };
            parameters.AddRange(new List<String> { "--model", model.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("secondaryissue", parameters);
        }


        /*
            :param: ADDRESS(std::string): "address"
            :param: cert(bool): "If specified, then only get related asset cert. Default is not specified."
        */
        public List<asset> getaddressasset(String ADDRESS, Boolean cert)
        {
            List<String> parameters = new List<String>() { ADDRESS };
            parameters.AddRange(new List<String> { "--cert", cert.ToString() });
            return getResult<List<asset>>("getaddressasset", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: number(std::uint32_t): "The number of addresses to be generated, defaults to 1."
        */
        public List<String> getnewaddress(String ACCOUNTNAME, String ACCOUNTAUTH, UInt32 number)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--number", number.ToString() });
            return getResult<List<String>>("getnewaddress", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public balances getbalance(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<balances>("getbalance", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: signaturenum(uint16_t): "Account multisig signature number."
            :param: publickeynum(uint16_t): "Account multisig public key number."
            :param: selfpublickey(std::string): "the public key belongs to this account."
            :param: publickey(list of string): "cosigner public key used for multisig"
            :param: description(std::string): "multisig record description."
        */
        public mulsignature getnewmultisig(String ACCOUNTNAME, String ACCOUNTAUTH, UInt16 signaturenum, UInt16 publickeynum, String selfpublickey, List<String> publickey, String description)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--signaturenum", signaturenum.ToString() });
            parameters.AddRange(new List<String> { "--publickeynum", publickeynum.ToString() });
            parameters.AddRange(new List<String> { "--selfpublickey", selfpublickey.ToString() });
            foreach (var i in publickey)
            {
                parameters.AddRange(new List<String> { "--publickey", i.ToString() });
            }
            parameters.AddRange(new List<String> { "--description", description.ToString() });
            return getResult<mulsignature>("getnewmultisig", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TODID(std::string): "Target did"
            :param: SYMBOL(std::string): "Asset MIT symbol"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction transfermit(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL };
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("transfermit", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: LASTWORD(std::string): "The last word of your private-key phrase."
        */
        public accountstatus deleteaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String LASTWORD)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, LASTWORD };

            return getResult<accountstatus>("deleteaccount", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public List<mulsignature> listmultisig(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<List<mulsignature>>("listmultisig", parameters);
        }


        /*
            :param: DidOrAddress(std::string): "Did symbol or standard address; If no input parameters, then display whole network DIDs."
        */
        public List<didhistoryaddress> getdid(String DidOrAddress)
        {
            List<String> parameters = new List<String>() { DidOrAddress };

            return getResult<List<didhistoryaddress>>("getdid", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: address(std::string): "The mining target address. Defaults to empty, means a new address will be generated."
            :param: number(uint16_t): "The number of mining blocks, useful for testing. Defaults to 0, means no limit."
        */
        public String startmining(String ACCOUNTNAME, String ACCOUNTAUTH, String address, UInt16 number)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };
            parameters.AddRange(new List<String> { "--address", address.ToString() });
            parameters.AddRange(new List<String> { "--number", number.ToString() });
            return getResult<String>("startmining", parameters);
        }


        /*
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public List<String> getwork(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<List<String>>("getwork", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: FILE(string of file path): "key file path."
            :param: FILECONTENT(std::string): "key file content. this will omit the FILE argument if specified."
        */
        public String importkeyfile(String ACCOUNTNAME, String ACCOUNTAUTH, String FILE, String FILECONTENT)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FILE, FILECONTENT };

            return getResult<String>("importkeyfile", parameters);
        }


        /*
            :param: TRANSACTION(string of hexcode): "The input Base16 transaction to sign."
        */
        public transaction decoderawtx(String TRANSACTION)
        {
            List<String> parameters = new List<String>() { TRANSACTION };

            return getResult<transaction>("decoderawtx", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: ADDRESS(std::string): "Asset receiver."
            :param: SYMBOL(std::string): "Asset symbol/name."
            :param: AMOUNT(uint64_t): "Asset integer bits. see asset <decimal_number>."
            :param: model(std::string): The token offering model by block height. 
            TYPE=1 - fixed quantity model; TYPE=2 - specify parameters; 
            LQ - Locked Quantity each period; 
            LP - Locked Period, numeber of how many blocks; 
            UN - Unlock Number, number of how many LPs; 
            eg: 
                TYPE=1;LQ=9000;LP=60000;UN=3  
                TYPE=2;LQ=9000;LP=60000;UN=3;UC=20000,20000,20000;UQ=3000,3000,3000 
            defaults to disable.
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction sendasset(String ACCOUNTNAME, String ACCOUNTAUTH, String ADDRESS, String SYMBOL, UInt64 AMOUNT, String model, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, ADDRESS, SYMBOL, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--model", model.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("sendasset", parameters);
        }


        /*
            :param: NONCE(std::string): "nonce. without leading 0x"
            :param: HEADERHASH(std::string): "header hash. with leading 0x"
            :param: MIXHASH(std::string): "mix hash. with leading 0x"
        */
        public Boolean submitwork(String NONCE, String HEADERHASH, String MIXHASH)
        {
            List<String> parameters = new List<String>() { NONCE, HEADERHASH, MIXHASH };

            return getResult<Boolean>("submitwork", parameters);
        }


        /*
            :param: PAYMENT_ADDRESS(string of Base58-encoded public key address): "The payment address. If not specified the address is read from STDIN."
        */
        public String getaddressetp(String PAYMENT_ADDRESS)
        {
            List<String> parameters = new List<String>() { PAYMENT_ADDRESS };

            return getResult<String>("getaddressetp", parameters);
        }


        /*
            :param: json(bool): "Json/Raw format, default is '--json=true'."
            :param: HASH(string of hash256): "The Base16 transaction hash of the transaction to get. If not specified the transaction hash is read from STDIN."
        */
        public transaction gettx(Boolean json, String HASH)
        {
            List<String> parameters = new List<String>() { json.ToString(), HASH };

            return getResult<transaction>("gettx", parameters);
        }


        /*
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public mining_info getmininginfo(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<mining_info>("getmininginfo", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: TODID(std::string): "Target did"
            :param: SYMBOL(std::string): "MIT symbol"
            :param: content(std::string): "Content of MIT"
            :param: mits(list of string): "List of symbol and content pair. Symbol and content are separated by a ':'"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction registermit(String ACCOUNTNAME, String ACCOUNTAUTH, String TODID, String SYMBOL, String content, List<String> mits, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, TODID, SYMBOL };
            parameters.AddRange(new List<String> { "--content", content.ToString() });
            foreach (var i in mits)
            {
                parameters.AddRange(new List<String> { "--mits", i.ToString() });
            }
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("registermit", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: PAYMENT_ADDRESS(string of Base58-encoded public key address): "the payment address of this account."
        */
        public String setminingaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String PAYMENT_ADDRESS)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, PAYMENT_ADDRESS };

            return getResult<String>("setminingaccount", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
        */
        public List<String> listaddresses(String ACCOUNTNAME, String ACCOUNTAUTH)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH };

            return getResult<List<String>>("listaddresses", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: LASTWORD(std::string): "The last word of your master private-key phrase."
            :param: DESTINATION(string of file path): "The keyfile storage path to."
            :param: data(bool): "If specified, the keyfile content will be append to the report, rather than to local file specified by DESTINATION."
        */
        public String dumpkeyfile(String ACCOUNTNAME, String ACCOUNTAUTH, String LASTWORD, String DESTINATION, Boolean data)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, LASTWORD, DESTINATION };
            parameters.AddRange(new List<String> { "--data", data.ToString() });
            return getResult<String>("dumpkeyfile", parameters);
        }


        /*
            :param: ADMINNAME(std::string): Administrator required.(when administrator_required in mvs.conf is set true)
            :param: ADMINAUTH(std::string): Administrator password required.
        */
        public List<String> getpeerinfo(String ADMINNAME, String ADMINAUTH)
        {
            List<String> parameters = new List<String>() { ADMINNAME, ADMINAUTH };

            return getResult<List<String>>("getpeerinfo", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: FROM_(std::string): "Send from this did/address"
            :param: TO_(std::string): "Send to this did/address"
            :param: AMOUNT(uint64_t): "ETP integer bits."
            :param: memo(std::string): "The memo to descript transaction"
            :param: fee(uint64_t): "Transaction fee. defaults to 10000 ETP bits"
        */
        public transaction didsendfrom(String ACCOUNTNAME, String ACCOUNTAUTH, String FROM_, String TO_, UInt64 AMOUNT, String memo, UInt64 fee)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, FROM_, TO_, AMOUNT.ToString() };
            parameters.AddRange(new List<String> { "--memo", memo.ToString() });
            parameters.AddRange(new List<String> { "--fee", fee.ToString() });
            return getResult<transaction>("didsendfrom", parameters);
        }


        /*
            :param: ACCOUNTNAME(std::string): Account name required.
            :param: ACCOUNTAUTH(std::string): Account password(authorization) required.
            :param: LASTWORD(std::string): "The last word of your backup words."
        */
        public accountresult getaccount(String ACCOUNTNAME, String ACCOUNTAUTH, String LASTWORD)
        {
            List<String> parameters = new List<String>() { ACCOUNTNAME, ACCOUNTAUTH, LASTWORD };

            return getResult<accountresult>("getaccount", parameters);
        }


        /*
            :param: NODEADDRESS(std::string): "The target node address[x.x.x.x:port]."
            :param: ADMINNAME(std::string): "admin name."
            :param: ADMINAUTH(std::string): "admin password/authorization."
            :param: operation(std::string): "The operation[ add|ban ] to the target node address. default: add."
        */
        public String addnode(String NODEADDRESS, String ADMINNAME, String ADMINAUTH, String operation)
        {
            List<String> parameters = new List<String>() { NODEADDRESS, ADMINNAME, ADMINAUTH };
            parameters.AddRange(new List<String> { "--operation", operation.ToString() });
            return getResult<String>("addnode", parameters);
        }


        /*
            :param: HASH_OR_HEIGH(std::string): "block hash or block height"
            :param: json(bool): "Json/Raw format, default is '--json=true'."
            :param: tx_json(bool): "Json/Raw format for txs, default is '--tx_json=true'."
        */
        public block getblock(String HASH_OR_HEIGH, Boolean json, Boolean tx_json)
        {
            List<String> parameters = new List<String>() { HASH_OR_HEIGH, json.ToString(), tx_json.ToString() };

            return getResult<block>("getblock", parameters);
        }

    }


}
