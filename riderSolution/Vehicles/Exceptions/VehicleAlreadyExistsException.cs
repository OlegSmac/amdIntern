namespace Vehicles.Exceptions;

public class VehicleAlreadyExistsException : Exception
{
    public VehicleAlreadyExistsException(string message) : base (message) {}
}