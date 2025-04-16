namespace project1.Exceptions;

public class VehicleAlreadyExistsException : Exception
{
    public VehicleAlreadyExistsException(string message) : base (message) {}
}