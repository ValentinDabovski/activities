namespace Domain.Exceptions;

public class InvalidCategoryException : BaseDomainException
{
    public InvalidCategoryException()
    {
    }

    public InvalidCategoryException(string error)
    {
        Error = error;
    }
}