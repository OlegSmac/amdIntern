using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Auth.Commands;

public class RegistrationHandlerFactory
{
    private readonly IEnumerable<IRegistrationHandler> _handlers;

    public RegistrationHandlerFactory(IEnumerable<IRegistrationHandler> handlers)
    {
        _handlers = handlers;
    }

    public IRegistrationHandler? GetHandler(string type)
    {
        return _handlers.FirstOrDefault(handler => handler.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
    }
}
