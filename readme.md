## Mediator

### What is a mediator
**Mediator** is a behavioral design pattern that lets you reduce chaotic dependencies between objects. The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object.
<img src="https://user-images.githubusercontent.com/67170413/128600102-91a9fb83-12a2-485c-be50-95e4c561b7ce.png" align="center" alt="mediator pattern UML diagram" width="600"/>

### How to use
1. Add a reference to `Mediator.DependencyInjection`
2. Add a mediator to the service collection using method `AddMediator(this IServiceCollection, ServiceLifetime, params Assembly[])`
3. Create requests and handlers
4. Resolve mediator with DI
5. Use method `Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;` to process the request

### Usage example

```csharp
public class PrintTextRequest : IRequest<bool>
{
}

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
```

```csharp
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
```
