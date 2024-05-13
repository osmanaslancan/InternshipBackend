using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.University;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Modules.WorkHistory;

namespace InternshipBackend.Modules.Account;

public interface ILinkedinScraperService
{
    Task<LinkedinScrapeResponse> ScrapeLinkedin(LinkedinScrapeRequest request);
}

public class LinkedinScraperService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory,
    IUniversityService universityService,
    IWebHostEnvironment environment,
    IMapper mapper) : BaseService, ILinkedinScraperService
{
    #region ApiObjects

#nullable disable
    public record Artifact(
        [property: JsonPropertyName("width")] int? Width,
        [property: JsonPropertyName("fileIdentifyingUrlPathSegment")]
        string FileIdentifyingUrlPathSegment,
        [property: JsonPropertyName("expiresAt")]
        object ExpiresAt,
        [property: JsonPropertyName("height")] int? Height
    );

    public record BackgroundImage(
        [property: JsonPropertyName("com.linkedin.common.VectorImage")]
        ComLinkedinCommonVectorImage ComLinkedinCommonVectorImage
    );

    public record BasicLocation(
        [property: JsonPropertyName("countryCode")]
        string CountryCode
    );

    public record CertificationView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record ComLinkedinCommonVectorImage(
        [property: JsonPropertyName("artifacts")]
        IReadOnlyList<Artifact> Artifacts,
        [property: JsonPropertyName("rootUrl")]
        string RootUrl
    );

    public record Company(
        [property: JsonPropertyName("miniCompany")]
        MiniCompany MiniCompany,
        [property: JsonPropertyName("employeeCountRange")]
        EmployeeCountRange EmployeeCountRange,
        [property: JsonPropertyName("industries")]
        IReadOnlyList<string> Industries
    );

    public record CourseView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record DefaultLocale(
        [property: JsonPropertyName("country")]
        string Country,
        [property: JsonPropertyName("language")]
        string Language
    );

    public record EducationView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<Element> Elements
    );

    public record Element(
        [property: JsonPropertyName("timePeriod")]
        TimePeriod TimePeriod,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("positions")]
        IReadOnlyList<Position> Positions,
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("miniCompany")]
        MiniCompany MiniCompany,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("school")] School School,
        [property: JsonPropertyName("fieldOfStudyUrn")]
        string FieldOfStudyUrn,
        [property: JsonPropertyName("degreeName")]
        string DegreeName,
        [property: JsonPropertyName("schoolName")]
        string SchoolName,
        [property: JsonPropertyName("fieldOfStudy")]
        string FieldOfStudy,
        [property: JsonPropertyName("degreeUrn")]
        string DegreeUrn,
        [property: JsonPropertyName("schoolUrn")]
        string SchoolUrn,
        [property: JsonPropertyName("locationName")]
        string LocationName,
        [property: JsonPropertyName("geoLocationName")]
        string GeoLocationName,
        [property: JsonPropertyName("geoUrn")] string GeoUrn,
        [property: JsonPropertyName("companyName")]
        string CompanyName,
        [property: JsonPropertyName("company")]
        Company Company,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("region")] string Region,
        [property: JsonPropertyName("companyUrn")]
        string CompanyUrn,
        [property: JsonPropertyName("description")]
        string Description,
        [property: JsonPropertyName("grade")] string Grade
    );

    public record EmployeeCountRange(
        [property: JsonPropertyName("start")] int? Start,
        [property: JsonPropertyName("end")] int? End
    );

    public record EndDate(
        [property: JsonPropertyName("month")] int? Month,
        [property: JsonPropertyName("year")] int? Year
    );

    public record GeoLocation(
        [property: JsonPropertyName("geoUrn")] string GeoUrn
    );

    public record HonorView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record LanguageView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record Location(
        [property: JsonPropertyName("basicLocation")]
        BasicLocation BasicLocation
    );

    public record Logo(
        [property: JsonPropertyName("com.linkedin.common.VectorImage")]
        ComLinkedinCommonVectorImage ComLinkedinCommonVectorImage
    );

    public record MiniCompany(
        [property: JsonPropertyName("objectUrn")]
        string ObjectUrn,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("showcase")]
        bool? Showcase,
        [property: JsonPropertyName("active")] bool? Active,
        [property: JsonPropertyName("logo")] Logo Logo,
        [property: JsonPropertyName("universalName")]
        string UniversalName,
        [property: JsonPropertyName("dashCompanyUrn")]
        string DashCompanyUrn,
        [property: JsonPropertyName("trackingId")]
        string TrackingId
    );

    public record MiniProfile(
        [property: JsonPropertyName("firstName")]
        string FirstName,
        [property: JsonPropertyName("lastName")]
        string LastName,
        [property: JsonPropertyName("dashEntityUrn")]
        string DashEntityUrn,
        [property: JsonPropertyName("occupation")]
        string Occupation,
        [property: JsonPropertyName("objectUrn")]
        string ObjectUrn,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("backgroundImage")]
        BackgroundImage BackgroundImage,
        [property: JsonPropertyName("publicIdentifier")]
        string PublicIdentifier,
        [property: JsonPropertyName("trackingId")]
        string TrackingId
    );

    public record OrganizationView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record Paging(
        [property: JsonPropertyName("start")] int? Start,
        [property: JsonPropertyName("count")] int? Count,
        [property: JsonPropertyName("total")] int? Total,
        [property: JsonPropertyName("links")] IReadOnlyList<object> Links
    );

    public record PatentView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record Position(
        [property: JsonPropertyName("locationName")]
        string LocationName,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("geoLocationName")]
        string GeoLocationName,
        [property: JsonPropertyName("geoUrn")] string GeoUrn,
        [property: JsonPropertyName("companyName")]
        string CompanyName,
        [property: JsonPropertyName("timePeriod")]
        TimePeriod TimePeriod,
        [property: JsonPropertyName("company")]
        Company Company,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("region")] string Region,
        [property: JsonPropertyName("companyUrn")]
        string CompanyUrn,
        [property: JsonPropertyName("description")]
        string Description
    );

    public record PositionGroupView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<Element> Elements
    );

    public record PositionView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<Element> Elements
    );

    public record PrimaryLocale(
        [property: JsonPropertyName("country")]
        string Country,
        [property: JsonPropertyName("language")]
        string Language
    );

    public record Profile(
        [property: JsonPropertyName("summary")]
        string Summary,
        [property: JsonPropertyName("industryName")]
        string IndustryName,
        [property: JsonPropertyName("lastName")]
        string LastName,
        [property: JsonPropertyName("supportedLocales")]
        IReadOnlyList<SupportedLocale> SupportedLocales,
        [property: JsonPropertyName("locationName")]
        string LocationName,
        [property: JsonPropertyName("student")]
        bool? Student,
        [property: JsonPropertyName("geoCountryName")]
        string GeoCountryName,
        [property: JsonPropertyName("geoCountryUrn")]
        string GeoCountryUrn,
        [property: JsonPropertyName("versionTag")]
        string VersionTag,
        [property: JsonPropertyName("geoLocationBackfilled")]
        bool? GeoLocationBackfilled,
        [property: JsonPropertyName("elt")] bool? Elt,
        [property: JsonPropertyName("industryUrn")]
        string IndustryUrn,
        [property: JsonPropertyName("defaultLocale")]
        DefaultLocale DefaultLocale,
        [property: JsonPropertyName("firstName")]
        string FirstName,
        [property: JsonPropertyName("showEducationOnProfileTopCard")]
        bool? ShowEducationOnProfileTopCard,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("geoLocation")]
        GeoLocation GeoLocation,
        [property: JsonPropertyName("location")]
        Location Location,
        [property: JsonPropertyName("miniProfile")]
        MiniProfile MiniProfile,
        [property: JsonPropertyName("headline")]
        string Headline
    );

    public record ProjectView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record PublicationView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record LinkedinUserInfoResponse(
        [property: JsonPropertyName("positionGroupView")]
        PositionGroupView PositionGroupView,
        [property: JsonPropertyName("patentView")]
        PatentView PatentView,
        [property: JsonPropertyName("summaryTreasuryMediaCount")]
        int? SummaryTreasuryMediaCount,
        [property: JsonPropertyName("summaryTreasuryMedias")]
        IReadOnlyList<object> SummaryTreasuryMedias,
        [property: JsonPropertyName("educationView")]
        EducationView EducationView,
        [property: JsonPropertyName("organizationView")]
        OrganizationView OrganizationView,
        [property: JsonPropertyName("projectView")]
        ProjectView ProjectView,
        [property: JsonPropertyName("positionView")]
        PositionView PositionView,
        [property: JsonPropertyName("profile")]
        Profile Profile,
        [property: JsonPropertyName("languageView")]
        LanguageView LanguageView,
        [property: JsonPropertyName("certificationView")]
        CertificationView CertificationView,
        [property: JsonPropertyName("testScoreView")]
        TestScoreView TestScoreView,
        [property: JsonPropertyName("volunteerCauseView")]
        VolunteerCauseView VolunteerCauseView,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("courseView")]
        CourseView CourseView,
        [property: JsonPropertyName("honorView")]
        HonorView HonorView,
        [property: JsonPropertyName("skillView")]
        SkillView SkillView,
        [property: JsonPropertyName("volunteerExperienceView")]
        VolunteerExperienceView VolunteerExperienceView,
        [property: JsonPropertyName("primaryLocale")]
        PrimaryLocale PrimaryLocale,
        [property: JsonPropertyName("publicationView")]
        PublicationView PublicationView
    );

    public record School(
        [property: JsonPropertyName("objectUrn")]
        string ObjectUrn,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("active")] bool? Active,
        [property: JsonPropertyName("logo")] Logo Logo,
        [property: JsonPropertyName("schoolName")]
        string SchoolName,
        [property: JsonPropertyName("trackingId")]
        string TrackingId
    );

    public record SkillView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<Element> Elements
    );

    public record StartDate(
        [property: JsonPropertyName("month")] int? Month,
        [property: JsonPropertyName("year")] int? Year
    );

    public record SupportedLocale(
        [property: JsonPropertyName("country")]
        string Country,
        [property: JsonPropertyName("language")]
        string Language
    );

    public record TestScoreView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record TimePeriod(
        [property: JsonPropertyName("endDate")]
        EndDate EndDate,
        [property: JsonPropertyName("startDate")]
        StartDate StartDate
    );

    public record VolunteerCauseView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );

    public record VolunteerExperienceView(
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("entityUrn")]
        string EntityUrn,
        [property: JsonPropertyName("profileId")]
        string ProfileId,
        [property: JsonPropertyName("elements")]
        IReadOnlyList<object> Elements
    );
#nullable enable

    #endregion

    private string GetStoreFile()
    {
        if (environment.IsDevelopment())
        {
            var assemblyLocation = Assembly.GetAssembly(GetType())!.Location;
            var directory = Path.GetDirectoryName(assemblyLocation);
            return Path.Combine(directory!, "cookiestore.json");
        }
        return "./cookiestore.json";
    }
    
    private static async Task<string> GetSessionCookies(HttpClient client, CookieContainer cookieContainer)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("X-Li-User-Agent",
            "LIAuthLibrary:0.0.3 com.linkedin.android:4.1.881 Asus_ASUS_Z01QD:android_9");
        client.DefaultRequestHeaders.Add("User-Agent", "ANDROID OS");
        client.DefaultRequestHeaders.Add("X-User-Language", "en");
        client.DefaultRequestHeaders.Add("X-User-Locale", "en_US");
        client.DefaultRequestHeaders.Add("Accept-Language", "en-us");

        // get csrf cookies
        var response = await client.GetAsync("https://www.linkedin.com/uas/authenticate");
        response.EnsureSuccessStatusCode();

        var cookies = cookieContainer.GetCookies(new Uri("https://www.linkedin.com"))
            .First(x => x.Name == "JSESSIONID");
        var csrfToken = cookies.Value.Replace("\"", "");

        client.DefaultRequestHeaders.Add("csrf-token", csrfToken);
        return csrfToken;
    }
    async Task Login(HttpClient client2, CookieContainer cookieContainer)
    {
        var csrfToken = await GetSessionCookies(client2, cookieContainer);

        var loginRequest = new Dictionary<string, string>
        {
            { "session_key", "linkedin@osman.tech" },
            { "session_password", "5t%UX#FpZw^^be" },
            { "JSESSIONID", csrfToken }
        };

        var response = await client2.PostAsync("https://www.linkedin.com/uas/authenticate",
            new FormUrlEncodedContent(loginRequest));
        
        response.EnsureSuccessStatusCode();

        var cookieStore = cookieContainer.GetCookies(new Uri("https://www.linkedin.com"));

        await File.WriteAllTextAsync(GetStoreFile(), JsonSerializer.Serialize(cookieStore));
    }

    public async Task<LinkedinScrapeResponse> ScrapeLinkedin(LinkedinScrapeRequest request)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);

        var metadataString = httpContextAccessor.HttpContext.User.FindFirstValue("app_metadata");
        ArgumentNullException.ThrowIfNull(metadataString);
        var metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataString);
        if (metadata == null || !metadata.TryGetValue("provider", out var userProvider))
        {
            throw new Exception("User provider not found");
        }

        using var client = httpClientFactory.CreateClient("Linkedin");
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.AccessToken);
        var response = await client.GetAsync("https://api.linkedin.com/v2/me");
        response.EnsureSuccessStatusCode();

        var linkedinData = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        if (linkedinData == null)
        {
            throw new Exception("Failed to scrape linkedin");
        }

        var vanityName = linkedinData.GetValueOrDefault("vanityName")?.ToString() as string;
        if (string.IsNullOrEmpty(vanityName))
        {
            throw new Exception("Failed to scrape linkedin");
        }

        var handler = new HttpClientHandler();
        var cookieContainer = new CookieContainer();
        handler.CookieContainer = cookieContainer;

        if (File.Exists(GetStoreFile()))
        {
            var storedCookies =
                JsonSerializer.Deserialize<CookieCollection>(await File.ReadAllTextAsync(GetStoreFile()));
            cookieContainer.Add(storedCookies!);
        }


        var client2 = new HttpClient(handler);

        string? csrf_token = null;
        CookieCollection? cookies = null;
        if (cookieContainer.Count == 0)
        {
            await Login(client2, cookieContainer);
        }

        // client2.DefaultRequestHeaders.Clear();
        cookies = cookieContainer.GetCookies(new Uri("https://www.linkedin.com"));
        csrf_token = cookies["JSESSIONID"]!.Value.Replace("\"", "");
        
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("X-Li-User-Agent",
            "LIAuthLibrary:0.0.3 com.linkedin.android:4.1.881 Asus_ASUS_Z01QD:android_9");
        client.DefaultRequestHeaders.Add("User-Agent", "ANDROID OS");
        client.DefaultRequestHeaders.Add("X-User-Language", "en");
        client.DefaultRequestHeaders.Add("X-User-Locale", "en_US");
        client.DefaultRequestHeaders.Add("Accept-Language", "en-us");
        client2.DefaultRequestHeaders.Add("csrf-token", csrf_token);

        // await client2.GetAsync("https://www.linkedin.com/uas/authenticate");
        // get profile
        response = await client2.GetAsync(
            $"https://www.linkedin.com/voyager/api/identity/profiles/{vanityName}/profileView");

        if (response.StatusCode == HttpStatusCode.Found)
        {
            handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            client2.Dispose();
            client2 = new HttpClient(handler);
            await Login(client2, cookieContainer);
            response = await client2.GetAsync(
                $"https://www.linkedin.com/voyager/api/identity/profiles/{vanityName}/profileView");
            response.EnsureSuccessStatusCode();
        }


        var data = await response.Content.ReadFromJsonAsync<LinkedinUserInfoResponse>();

        var universities = (await universityService.ListAsync()).ToLookup(x => x.Name);


        var educations = data!.EducationView.Elements.Select(x =>
        {
            var university = universities[x.SchoolName].FirstOrDefault();
            var result = new UniversityEducationListDto()
            {
                Department = x.FieldOfStudy,
                StartDate = new DateTime(x.TimePeriod.StartDate.Year!.Value, x.TimePeriod.StartDate.Month ?? 1, 1),
            };
            if (x.TimePeriod.EndDate != null)
            {
                result.EndDate = new DateTime(x.TimePeriod.EndDate.Year!.Value, x.TimePeriod.EndDate.Month ?? 1, 1);
                result.IsGraduated = true;
            }

            if (x.Grade != null && double.TryParse(x.Grade, out var gpa))
            {
                result.GPA = gpa;
            }

            if (university != null)
            {
                result.UniversityId = university.Id;
            }
            else
            {
                result.UniversityName = x.SchoolName;
            }

            return result;
        }).OrderBy(x => x.StartDate).ToList();

        var workHistory = data.PositionView.Elements.Select(x =>
        {
            var result = new WorkHistoryListDto()
            {
                CompanyName = x.CompanyName,
                Position = x.Title,
                StartDate = new DateTime(x.TimePeriod.StartDate.Year!.Value, x.TimePeriod.StartDate.Month ?? 1, 1),
                IsWorkingNow = true,
                Description = x.Description,
            };
            if (x.TimePeriod.EndDate != null)
            {
                result.EndDate = new DateTime(x.TimePeriod.EndDate.Year!.Value, x.TimePeriod.EndDate.Month ?? 1, 1);
                result.IsWorkingNow = false;
            }

            return result;
        }).OrderBy(x => x.StartDate).ToList();

        return new LinkedinScrapeResponse()
        {
            Educations = educations,
            WorkHistory = workHistory,
        };
    }
}