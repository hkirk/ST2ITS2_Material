namespace ExceptionExamples;

public class NoCoffeeException : Exception
{
    public int Expected { get; private set; }
    public NoCoffeeException(string message, int expected) : base(message)
    {
        Expected = expected;
    }
}