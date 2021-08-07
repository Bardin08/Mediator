using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    public interface IHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }
}