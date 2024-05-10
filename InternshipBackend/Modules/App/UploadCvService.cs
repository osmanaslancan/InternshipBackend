using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using InternshipBackend.Modules.UserDetails;
using Microsoft.IdentityModel.Tokens;

namespace InternshipBackend.Modules.App;

public interface IUploadCvService
{
    Task<UploadResponse> UploadFile(UploadCvRequest request);
}

public class UploadCvService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory clientFactory,
    IUserDetailService userDetailService,
    IConfiguration configuration) : UploadServiceBase(httpContextAccessor, clientFactory, configuration), IUploadCvService
{
    protected override string Bucket => "PrivateCvs";

    private string CreateToken(string url)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SupabaseSigningKey"]!));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("url", url)
            }),
            Expires = DateTime.UtcNow.AddMinutes(100),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    protected override string UploadDirectory(Guid userSupabaseId, Guid guid)
    {
        var result = base.UploadDirectory(userSupabaseId, guid);
        var token = CreateToken(FilePostfix(userSupabaseId, guid));
        return $"{result}?token={token}";
    }

    protected override string DownloadDirectory(Guid userSupabaseId, Guid guid)
    {
        return guid.ToString();
    }

    public async Task<UploadResponse> UploadFile(UploadCvRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.File);
        
        using var stream = new MemoryStream();
        await request.File.CopyToAsync(stream);

        var bytes = stream.ToArray();

        if (request.File.ContentType != "application/pdf" ||  !bytes[..4].SequenceEqual("%PDF"u8.ToArray()))
        {
            throw new ValidationException("Invalid file format. Only PDF files are allowed.");
        }
        
        var response = await Upload(bytes, request.File.Name, request.File.FileName, request.File.ContentType);
        
        await userDetailService.AddCvToCurrentUser(request.File.FileName, response.Url!);
        
        return response;
    }

}