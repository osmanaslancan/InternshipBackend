﻿using AutoMapper;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
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
        CreateMap<ForeignLanguageModifyDto, ForeignLanguage>();
        CreateMap<ForeignLanguage, ForeignLanguageListDto>();
        CreateMap<City, CityDTO>();
        CreateMap<Country, CountryDTO>();
        CreateMap<CompanyModifyDto, Company>();
        CreateMap<Company, CompanyDto>();
        CreateMap<InternshipPostingModifyDto, InternshipPosting>();
        CreateMap<InternshipPosting, InternshipPostingListDto>();
        CreateMap<UniversityEducationModifyDto, UniversityEducation>();
        CreateMap<UniversityEducation, UniversityEducationListDto>();
        CreateMap<WorkHistory, WorkHistoryListDto>();
        CreateMap<WorkHistoryModifyDto, WorkHistory>();
        CreateMap<UserDetailDto, UserDetail>();
        CreateMap<UserDetail, UserDetailListDto>();
        CreateMap<UserProject, UserProjectListDto>();
        CreateMap<UserProjectModifyDto, UserProject>();
        CreateMap<UserReferenceModifyDto, UserReference>();
        CreateMap<UserReference, UserReferenceListDto>();
    }
}
