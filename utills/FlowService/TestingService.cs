using System;
using System.Threading.Tasks;
using FlowCommService;
using FlowServiceConstants;

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

            flowService.PrintResult(lastestBlock, Constants.DataTypes.block);

            var lastestBlockById = await flowService.getBlock(lastestBlock.Header.Id, Constants.DataTypes.id);

            flowService.PrintResult(lastestBlockById, Constants.DataTypes.block);

            var lastestBlockByHeight = await flowService.getBlock(lastestBlock.Header.Height, Constants.DataTypes.height);

            flowService.PrintResult(lastestBlockByHeight, Constants.DataTypes.block);

        }

        public async Task getAccount()
        {

            var userAddress = Constants.FlowTestingServiceConstants.testingAddress1;
            var userAccountResponse = await flowService.getAccounts(userAddress);
            flowService.PrintResult(userAccountResponse, Constants.DataTypes.account);
        }


        public async Task CompleteTesting()
        {
            await getBlock();
            await getAccount();
        }


    }
}




