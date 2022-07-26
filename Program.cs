
using FlowCommServiceTesting;

namespace flow_dotnet_service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Sham 1");

            FlowServiceTesting flowServiceTesting = new FlowServiceTesting();

            await flowServiceTesting.CompleteTesting();


            Console.WriteLine("Hello Sham 2");
        }
    }
}
