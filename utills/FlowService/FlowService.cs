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

        public async Task getBlock()
        {
            // get the latest sealed block
            var latestBlock = await _flowHttpClient.GetLatestBlockAsync();
            PrintResult(latestBlock);

            // get the block by ID
            var blockByIdResult = await _flowHttpClient.GetBlockByIdAsync(latestBlock.Header.Id);
            PrintResult(blockByIdResult);

            // get block by height
            var blockByHeightResult = await _flowHttpClient.GetBlockByHeightAsync(latestBlock.Header.Height);
            PrintResult(blockByHeightResult);
        }


        private void PrintResult(FlowBlock flowBlock)
        {
            Console.WriteLine($"ID: {flowBlock.Header.Id}");
            Console.WriteLine($"height: {flowBlock.Header.Height}");
            Console.WriteLine($"timestamp: {flowBlock.Header.Timestamp}\n");
        }


    }
}




