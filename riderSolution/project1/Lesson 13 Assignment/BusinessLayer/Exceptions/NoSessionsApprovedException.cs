namespace project1.Lesson_13_Assignment.BusinessLayer.Exceptions;

public class NoSessionsApprovedException : Exception
{
    public NoSessionsApprovedException(string message) : base(message) { }
}