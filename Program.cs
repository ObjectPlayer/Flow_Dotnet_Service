
using FlowCommService;

namespace flow_dotnet_service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Sham!");

            FlowService flowService = new FlowService();

            await flowService.getBlock();


            Console.WriteLine("Hello Sham 2");
        }
    }
}
