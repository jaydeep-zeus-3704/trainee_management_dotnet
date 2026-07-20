namespace trainee_management.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message):base(message){}
}

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string message):base(message){}
}
public class ForbidenException : Exception
{
    public ForbidenException(string message):base(message){}
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


public class UpdateFailedException : Exception
{
    public UpdateFailedException(string message):base(message){}
}


public class InvalidExtensionException : Exception
{
    public InvalidExtensionException(string message):base(message){}
}


public class FileSizeException : Exception
{
    public FileSizeException(string message):base(message){}
}

