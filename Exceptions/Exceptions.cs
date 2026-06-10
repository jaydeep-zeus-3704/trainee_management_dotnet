namespace trainee_management.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message):base(message){}
}


public class ValidationException:Exception
{
    public ValidationException(string message):base(message){}
}

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string message):base(message){}
}


public class InvalidCredentialsException:Exception
{
    public InvalidCredentialsException(string message):base(message){}
}
