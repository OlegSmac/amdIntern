namespace project1.Exceptions;

public class VehicleNotFoundException : Exception
{
    public VehicleNotFoundException(string message) : base (message) {}
}