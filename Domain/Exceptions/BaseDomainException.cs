namespace Domain.Exceptions;

public class BaseDomainException : Exception
{
    private string _error;

    public string Error
    {
        get => _error ?? base.Message;
        set => _error = value;
    }
}