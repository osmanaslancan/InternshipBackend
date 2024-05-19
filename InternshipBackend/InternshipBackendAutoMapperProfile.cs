using AutoMapper;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.ValueObjects;
using InternshipBackend.Modules;
using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.CompanyManagement;
using InternshipBackend.Modules.ForeignLanguage;
using InternshipBackend.Modules.Internship;
using InternshipBackend.Modules.Location;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Modules.UserDetails;
using InternshipBackend.Modules.UserProjects;
using InternshipBackend.Modules.WorkHistory;

namespace InternshipBackend;

public class InternshipBackendAutoMapperProfile : Profile
{
    public InternshipBackendAutoMapperProfile()
    {
        CreateMap<UserInfoUpdateDto, User>();
        CreateMap<User, UserDTO>();
        CreateMap<UserNotification, UserNotificationDto>();
        CreateMap<ForeignLanguageModifyDto, ForeignLanguage>();
        CreateMap<ForeignLanguage, ForeignLanguageListDto>();
        CreateMap<City, CityDTO>();
        CreateMap<Country, CountryDTO>();
        CreateMap<CompanyModifyDto, Company>();
        CreateMap<Company, CompanyDto>();
        CreateMap<Company, CompanyDetailDto>();
        CreateMap<InternshipPostingModifyDto, InternshipPosting>();
        CreateMap<InternshipPosting, InternshipPostingListDto>();
        CreateMap<InternshipPosting, InternshipPostingDto>();
        CreateMap<InternshipApplication, InternshipApplicationCompanyListDto>();
        CreateMap<InternshipApplication, InternshipApplicationInternListDto>();
        CreateMap<User, ApplicationDetailDto>();
        CreateMap<RatingResult, InternshipPostingCompanyDto>();
        CreateMap<InternshipPostingComment, InternshipPostingCommentDto>();
        CreateMap<UniversityEducationModifyDto, UniversityEducation>();
        CreateMap<UniversityEducation, UniversityEducationListDto>();
        CreateMap<WorkHistory, WorkHistoryListDto>();
        CreateMap<WorkHistoryModifyDto, WorkHistory>();
        CreateMap<UserDetailDto, UserDetail>();
        CreateMap<UserDetail, UserDetailListDto>();
        CreateMap<UserDetail, UserDetailDto>();
        CreateMap<UserProject, UserProjectListDto>();
        CreateMap<UserProjectModifyDto, UserProject>();
        CreateMap<UserReferenceModifyDto, UserReference>();
        CreateMap<UserReference, UserReferenceListDto>();
    }
}
