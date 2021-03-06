using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    public interface IMediator
    {
        public Task<TResponse> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>;
        public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest<TResponse>;
    }
}