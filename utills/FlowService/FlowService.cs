using System;
using Flow.Net.Sdk.Client.Grpc;
using Flow.Net.Sdk.Client.Http;
using Flow.Net.Sdk.Core.Models;
using Flow.Net.Sdk.Core.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlowServiceConstants;
using System.Text.RegularExpressions;

namespace FlowCommService
{

    public class FlowService
    {
        // RPC client
        private FlowHttpClient _flowHttpClient;

        // gRPC client
        private FlowGrpcClient _flowGrpcClient;

        public FlowService()
        {
            this._flowHttpClient = new FlowHttpClient(new HttpClient(), Flow.Net.Sdk.Client.Http.ServerUrl.TestnetHost);

            this._flowGrpcClient = new FlowGrpcClient(Flow.Net.Sdk.Client.Grpc.ServerUrl.TestnetHost);
        }

        //Get Blockchain data

        //Get Blocks
        public async Task<FlowBlock> getBlock(dynamic data = null, string type = "")
        {
            FlowBlock blockResult;

            if (data != null && type.Equals(Constants.DataTypes.id))
                blockResult = await _flowHttpClient.GetBlockByIdAsync(data);


            else if (data != null && type.Equals(Constants.DataTypes.height))
                blockResult = await _flowHttpClient.GetBlockByHeightAsync(data);

            else
                blockResult = await _flowHttpClient.GetLatestBlockAsync();

            return blockResult;

        }

        //Get Collections

        //Get Transactions

        //Get Accounts
        public async Task<FlowAccount> getAccounts(string address)
        {
            FlowAccount accountResult;

            var addressWithoutPrefix = Regex.Replace(address, @"0x","");

            accountResult = await _flowHttpClient.GetAccountAtLatestBlockAsync(addressWithoutPrefix);

            return accountResult;

        }


        //Get Events


        public void PrintResult(dynamic data, string type)
        {
            if (type.Equals(Constants.DataTypes.block))
            {
                Console.WriteLine($"ID: {data.Header.Id}");
                Console.WriteLine($"height: {data.Header.Height}");
                Console.WriteLine($"timestamp: {data.Header.Timestamp}\n");
            }

            else if (type.Equals(Constants.DataTypes.account))
            {
                Console.WriteLine($"Address: {data.Address.Address}");
                Console.WriteLine($"Balance: {data.Balance}");
                Console.WriteLine($"Contracts: {data.Contracts.Count}");
                Console.WriteLine($"Keys: {data.Keys.Count}\n");
                PrintResult(data.Keys, Constants.DataTypes.keys);
            }

            else if (type.Equals(Constants.DataTypes.keys))
            {
                foreach (var key in data)
                {
                    Console.WriteLine($"Key Index: {key.Index}");
                    Console.WriteLine($"Key Sequence Number: {key.SequenceNumber}");
                    Console.WriteLine($"Key Public Key: {key.PublicKey}");
                    Console.WriteLine($"Key Private Key: {key.PrivateKey}");
                    Console.WriteLine($"Key Hash Algorithm: {key.HashAlgorithm}");
                    Console.WriteLine($"Key Signature Algorithm: {key.SignatureAlgorithm}");
                    Console.WriteLine($"Key Revoked: {key.Revoked}");
                    Console.WriteLine($"Key Weight: {key.Weight}\n\n");
                }
            }


        }



    }
}




