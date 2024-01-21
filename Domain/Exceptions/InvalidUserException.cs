namespace Domain.Exceptions;

public class InvalidUserException : BaseDomainException
{
    public InvalidUserException()
    {
    }

    public InvalidUserException(string error)
    {
        Error = error;
    }
}