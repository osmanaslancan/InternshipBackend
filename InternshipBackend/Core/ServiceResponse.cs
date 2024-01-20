namespace InternshipBackend.Core;

public class ServiceResponse<T> : ServiceResponse
{
    public T? Data { get; set; }
}

public class ServiceResponse
{
    public ServiceError? Error { get; set; }

    public static ServiceResponse<T> Success<T>(T data)
    {
        return new ServiceResponse<T>
        {
            Data = data
        };
    }
}

public class ServiceError
{
    public required string Name { get; set; }
    public string? Details { get; set; }
    public Dictionary<string, string>? Errors { get; set; }
}