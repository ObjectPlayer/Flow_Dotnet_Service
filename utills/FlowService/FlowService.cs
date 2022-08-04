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
        public async Task<FlowCollection> getCollection(string collectionId)
        {
            FlowCollection collectionResult;

            collectionResult = await _flowHttpClient.GetCollectionAsync(collectionId);

            return collectionResult;
        }


        //Get Transactions
        public async Task<FlowTransactionResponse> getTransaction(string transactionId)
        {
            FlowTransactionResponse transacctionResult;

            transacctionResult = await _flowHttpClient.GetTransactionAsync(transactionId);

            return transacctionResult;
        }

        //Get Transaction Result
        public async Task<FlowTransactionResult> getTransactionResult(string transactionId)
        {
            FlowTransactionResult transacctionResult;

            transacctionResult = await _flowHttpClient.GetTransactionResultAsync(transactionId);

            return transacctionResult;
        }


        //Get Accounts
        public async Task<FlowAccount> getAccount(string address)
        {
            FlowAccount accountResult;

            var addressWithoutPrefix = Regex.Replace(address, Constants.DataTypes.addressPrefix, "");

            accountResult = await _flowHttpClient.GetAccountAtLatestBlockAsync(addressWithoutPrefix);

            return accountResult;
        }


        //Get Events
        public async Task<IEnumerable<FlowBlockEvent>> getEvent(string eventName, UInt64 startHeight = 0, UInt64 endHeight = 100)
        {
            IEnumerable<FlowBlockEvent> eventResult;

            eventResult = await _flowHttpClient.GetEventsForHeightRangeAsync(eventName, startHeight, endHeight);

            return eventResult;

        }


        // PrintData
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

            else if (type.Equals(Constants.DataTypes.transaction))
            {
                Console.WriteLine($"ReferenceBlockId: {data.ReferenceBlockId}");
                Console.WriteLine($"Payer: {data.Payer.Address}");
                Console.WriteLine("Authorizers: [{0}]", data.Authorizers);
                Console.WriteLine($"Proposer: {data.ProposalKey.Address.Address}\n");
            }

            else if (type.Equals(Constants.DataTypes.transactionResult))
            {
                Console.WriteLine($"Status: {data.Status}");
                Console.WriteLine($"Error: {data.ErrorMessage}\n");
            }

            else if (type.Equals(Constants.DataTypes.events))
            {
                if (data is IEnumerable<FlowBlockEvent>)
                {
                    foreach (var blockEvent in data)
                    {
                        PrintResult(blockEvent.Events, Constants.DataTypes.events);
                    }
                }
                else
                {
                    foreach (var @event in data)
                    {
                        Console.WriteLine($"Type: {@event.Type}");
                        Console.WriteLine($"Values: {@event.Payload}");
                        Console.WriteLine($"Transaction ID: {@event.TransactionId} \n");
                    }

                }
            }

            else if (type.Equals(Constants.DataTypes.collection))
            {
                Console.WriteLine($"ID: {data.Id.FromByteStringToHex()}");
                Console.WriteLine("Transactions: [{0}]", data.TransactionIds);
            }


        }



    }
}




