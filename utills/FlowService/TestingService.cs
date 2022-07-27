using System;
using System.Threading.Tasks;
using FlowCommService;

namespace FlowCommServiceTesting
{

    public class FlowServiceTesting
    {
        FlowService flowService;

        public FlowServiceTesting()
        {
            flowService = new FlowService();
        }


        public async Task getBlock()
        {

            var lastestBlock = await flowService.getBlock();

            flowService.PrintResult(lastestBlock, "block");

            var lastestBlockById = await flowService.getBlock(lastestBlock.Header.Id, "id");

            flowService.PrintResult(lastestBlockById, "block");

            var lastestBlockByHeight = await flowService.getBlock(lastestBlock.Header.Height, "height");

            flowService.PrintResult(lastestBlockByHeight, "block");

        }

        public async Task getAccount()
        {

            var lastestBlock = await flowService.getBlock();

            flowService.PrintResult(lastestBlock, "block");

            var lastestBlockById = await flowService.getBlock(lastestBlock.Header.Id, "id");

            flowService.PrintResult(lastestBlockById, "block");

            var lastestBlockByHeight = await flowService.getBlock(lastestBlock.Header.Height, "height");

            flowService.PrintResult(lastestBlockByHeight, "block");

        }


        public async Task CompleteTesting()
        {
            await getBlock();
        }


    }
}




