using AutoMapper;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules;
using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.ForeignLanguage;
using InternshipBackend.Modules.Location;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Modules.UserDetail;
using InternshipBackend.Modules.UserProject;
using InternshipBackend.Modules.WorkHistory;

namespace InternshipBackend;

public class InternshipBackendAutoMapperProfile : Profile
{
    public InternshipBackendAutoMapperProfile()
    {
        CreateMap<UserInfoUpdateDto, User>();
        CreateMap<User, UserDTO>();
        CreateMap<ForeignLanguageModifyDto, ForeignLanguage>();
        CreateMap<ForeignLanguage, ForeignLanguageListDto>();
        CreateMap<City, CityDTO>();
        CreateMap<Country, CountryDTO>();
        CreateMap<UniversityEducationModifyDto, UniversityEducation>();
        CreateMap<UniversityEducation, UniversityEducationListDto>();
        CreateMap<WorkHistory, WorkHistoryListDto>();
        CreateMap<UserDetailDTO, UserDetail>();
        CreateMap<UserProject, UserProjectListDto>();
        CreateMap<UserProjectModifyDto, UserProject>();
        CreateMap<string, DriverLicense>()
            .ForMember(x => x.License, x => x.MapFrom((src, dest) => src));
    }
}
