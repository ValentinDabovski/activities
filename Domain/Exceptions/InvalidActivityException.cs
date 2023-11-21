namespace Domain.Exceptions;

public class InvalidActivityException : BaseDomainException
{
    public InvalidActivityException()
    {
    }

    public InvalidActivityException(string error)
    {
        Error = error;
    }
}