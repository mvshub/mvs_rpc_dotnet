# mvs_rpc_dotnet


## Introduction

This project is a dotnet library that is intended to provide a wrapper around JSON-RPC provided by Metaverse [MVS](mvs.live).

The user must be familiar with the [metaverse jsonrpc interface](https://docs.mvs.org/api_v2/index.html) before using this library

## Installation

### NuGet

Thie project is available for [NuGet packages](https://www.nuget.org/packages/mvs_rpc/)

### Compile the Source
git clone https://github.com/mvshub/mvs_rpc_dotnet.git
compile the mvs_rpc_dotnet.sln
add the reference mvs_rpc.dll in your project

## How to use

* Start Metaverse Server (daemon).
* add the referencec of mvs_rpc_dotnet
```js
RPC rpc = new RPC();
rpc.URL="http://127.0.0.1:8820";
rpc.VERSION=rpcversion.v3;
String block = rpc.getblock(1200000);
```

## All interfaces
```js
- getnewaccount
- getaccount
- deleteaccount
- importaccount
- changepasswd
- getnewaddress
- validateaddress
- listaddresses
- dumpkeyfile
- importkeyfile
- 
- shutdown
- getinfo
- addnode
- getpeerinfo
- startmining
- stopmining
- getmininginfo
- setminingaccount
- getwork
- submitwork
- getmemorypool
- 
- getheight
- getblock
- getblockheader
- fetchheaderext
- gettx
- listtxs
- createrawtx
- decoderawtx
- signrawtx
- sendrawtx
- 
- getpublickey
- createmultisigtx
- getnewmultisig
- listmultisig
- deletemultisig
- signmultisigtx
- 
- send
- sendmore
- sendfrom
- deposit
- listbalances
- getbalance
- getaddressetp
- 
- createasset
- deletelocalasset
- issue
- secondaryissue
- sendasset
- sendassetfrom
- listassets
- getasset
- getaccountasset
- getaddressasset
- burn
- 
- issuecert
- transfercert
- 
- registermit
- transfermit
- listmits
- getmit
- 
- registerdid
- didsend
- didsendfrom
- didsendasset
- didsendassetfrom
- didsendmore
- didchangeaddress
- listdids
- getdid
```
# Todo

Add more call methods.
