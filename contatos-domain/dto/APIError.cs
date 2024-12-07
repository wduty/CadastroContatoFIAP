namespace contatos_domain.dto;

public class ApiError
{
    public string ErrorMessage { get; }

    public ApiError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public override string ToString() => ErrorMessage;
}

