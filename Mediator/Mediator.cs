using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    public class Mediator : IMediator
    {
        private readonly ServiceFactory _serviceFactory;
        private readonly ConcurrentDictionary<Type, Type> _handlerInfos;

        public Mediator(ServiceFactory serviceFactory, ConcurrentDictionary<Type, Type> handlerInfos)
        {
            _serviceFactory = serviceFactory;
            _handlerInfos = handlerInfos;
        }

        public Task<TResponse> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            => Send<TRequest, TResponse>(request, CancellationToken.None);

        public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest<TResponse>
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestType = request.GetType();
            if (!_handlerInfos.ContainsKey(requestType))
            {
                throw new InvalidOperationException($"No handler found for {requestType.FullName}");
            }

            var handlerType = _handlerInfos[requestType];
            var handler = _serviceFactory.GetInstanceWithCast<IHandler<TRequest, TResponse>>(handlerType);

            return handler.Handle(request, cancellationToken);
        }
    }
}