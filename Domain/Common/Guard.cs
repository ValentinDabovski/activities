using System.ComponentModel.DataAnnotations;
using Domain.Exceptions;

namespace Domain.Common;

public static class Guard
{
    public static void AgainstEmptyString<TException>(string value, string name = "Value")
        where TException : BaseDomainException, new()
    {
        if (!string.IsNullOrEmpty(value)) return;

        ThrowException<TException>($"{name} cannot be null or empty.");
    }

    public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = "Value")
        where TException : BaseDomainException, new()
    {
        AgainstEmptyString<TException>(value, name);

        if (minLength <= value.Length && value.Length <= maxLength) return;

        ThrowException<TException>($"{name} must have between {minLength} and {maxLength} symbols.");
    }

    public static void Against<TException>(object actualValue, object unexpectedValue, string name = "Value")
        where TException : BaseDomainException, new()
    {
        if (!actualValue.Equals(unexpectedValue)) return;

        ThrowException<TException>($"{name} must not be {unexpectedValue}.");
    }

    public static void ForValidEmail<TException>(string value, string name = "Value")
        where TException : BaseDomainException, new()
    {
        var emailValidation = new EmailAddressAttribute();

        if (emailValidation.IsValid(value)) return;

        ThrowException<TException>($"{name} must be valid email address.");
    }

    private static void ThrowException<TException>(string message)
        where TException : BaseDomainException, new()
    {
        var exception = new TException
        {
            Error = message
        };

        throw exception;
    }
}