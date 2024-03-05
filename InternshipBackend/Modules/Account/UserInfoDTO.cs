using AutoMapper;
using FluentValidation;
using InternshipBackend.Data;

namespace InternshipBackend.Modules;

[AutoMap(typeof(User))]
public class UserInfoUpdateDTO
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public string? PhoneNumber { get; set; }
}

public class UserInfoUpdateDTOValidator : AbstractValidator<UserInfoUpdateDTO>
{
    public UserInfoUpdateDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
    }
}