using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Example
{
    public class GetCurrentUtcTimeRequestHandler : IHandler<GetCurrentUtcTimeRequest, DateTimeOffset>
    {
        public Task<DateTimeOffset> Handle(GetCurrentUtcTimeRequest request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(DateTimeOffset.UtcNow);
        }
    }
}