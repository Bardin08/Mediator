using System;
using System.Threading.Tasks;

using Mediator.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddMediator(ServiceLifetime.Scoped, typeof(Program).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send<PrintTextRequest, bool>(new PrintTextRequest());
            
            var dateTimeOffset = await mediator.Send<GetCurrentUtcTimeRequest, DateTimeOffset>(new GetCurrentUtcTimeRequest());
            Console.WriteLine(dateTimeOffset.ToString());
        }
    }
}