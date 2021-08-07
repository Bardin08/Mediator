using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Example
{
    public class PrintTextHandler : IHandler<PrintTextRequest, bool>
    {
        public async Task<bool> Handle(PrintTextRequest request, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Hw");
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine("Hw1");
            return true;
        }
    }
}