using FluentValidation;
using FluentValidation.Results;

namespace InternshipBackend.Tests.Mocks;

public class MockValidator<T> : AbstractValidator<T>
{
    public MockValidator()
    {
    }
}