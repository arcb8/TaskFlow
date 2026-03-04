using System;
using System.Collections.Generic;

public class ValidationException : Exception
{
    public List<string> Errors { get; }

    public ValidationException(List<string> errors)
    {
        Errors = errors;
    }
}