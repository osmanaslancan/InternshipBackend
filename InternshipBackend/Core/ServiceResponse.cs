namespace InternshipBackend.Core;

public class ServiceResponse
{
    public ServiceError? Error { get; set; }
}

public class ServiceError
{
    public required string Name { get; set; }
    public string? Details { get; set; }
    public Dictionary<string, string>? Errors { get; set; }
}