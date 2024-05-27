
namespace InternshipBackend.Modules.CompanyManagement;

public class CompanyDetailDto : CompanyDto 
{
    public bool IsCurrentUserFollowing { get; set; }
}