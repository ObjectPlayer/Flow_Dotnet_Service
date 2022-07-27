using System;
using Flow.Net.Sdk.Client.Grpc;
using Flow.Net.Sdk.Client.Http;
using Flow.Net.Sdk.Core.Models;
using Flow.Net.Sdk.Core.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


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



        private void PrintResult(FlowBlock flowBlock)
        {
            Console.WriteLine($"ID: {flowBlock.Header.Id}");
            Console.WriteLine($"height: {flowBlock.Header.Height}");
            Console.WriteLine($"timestamp: {flowBlock.Header.Timestamp}\n");
        }



        //Get Blockchain data

        //Get Blocks
        public async Task<FlowBlock> getBlock(dynamic data = null, string type = "")
        {
            FlowBlock blockResult;

            if (data != null && type.Equals("id"))
                blockResult = await _flowHttpClient.GetBlockByIdAsync(data);


            else if (data != null && type.Equals("height"))
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

            accountResult = await _flowHttpClient.GetAccountAtLatestBlockAsync(address);

            return accountResult;

        }


        //Get Events


        public void PrintResult(dynamic data, string type)
        {
            if (type.Equals("block"))
            {
                Console.WriteLine($"ID: {data.Header.Id}");
                Console.WriteLine($"height: {data.Header.Height}");
                Console.WriteLine($"timestamp: {data.Header.Timestamp}\n");
            }

            else if (type.Equals("address"))
            {
                Console.WriteLine($"Address: {data.Address.Address}");
                Console.WriteLine($"Balance: {data.Balance}");
                Console.WriteLine($"Contracts: {data.Contracts.Count}");
                Console.WriteLine($"Keys: {data.Keys.Count}\n");
            }
        }



    }
}




