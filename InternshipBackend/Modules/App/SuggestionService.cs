using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.Internship;
using Microsoft.EntityFrameworkCore;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;

namespace InternshipBackend.Modules.App;

public class SuggestionService : BaseService
{
    private readonly IOpenAIService _openAiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAccountService _accountService;
    private readonly IInternshipPostingService _internshipPostingService;
    private readonly IGenericRepository<UserSuggestion> _repository;

    public SuggestionService(IOpenAIService openAiService, IHttpContextAccessor httpContextAccessor,
        IAccountService accountService, IInternshipPostingService internshipPostingService,
        IGenericRepository<UserSuggestion> repository)
    {
        _openAiService = openAiService;
        _httpContextAccessor = httpContextAccessor;
        _accountService = accountService;
        _internshipPostingService = internshipPostingService;
        _repository = repository;
        openAiService.SetDefaultModelId(Models.Gpt_3_5_Turbo);
    }

    private record SuggestionData(List<InternshipPostingListDto> Postings, UserDTO User);

    private record OpenAiResponseData(List<int> Id);

    private async Task<ChatCompletionCreateResponse> GetCachedResponse()
    {
        var userInfo = await _accountService.GetUser();

        var cache = await _repository.GetQueryable().Where(x => x.UserId == userInfo.Id).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        
        
        if (cache is { Response: not null })
        {
            return JsonSerializer.Deserialize<ChatCompletionCreateResponse>(cache.Response)!;
        }
        
        if (userInfo == null)
        {
            throw new ValidationException("User not found");
        }

        var postings = await _internshipPostingService.ListAsync(new InternshipPostingListRequestDto
        {
            From = 0,
            Take = 100,
            Sort = InternshipPostingSort.Popularity,
        });
        var data = JsonSerializer.Serialize(new SuggestionData(postings.Items, userInfo));
        var response = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
        {
            Model = Models.Gpt_4o,
            ChatResponseFormat = ChatCompletionCreateRequest.ResponseFormats.Json,
            Messages =
            [
                ChatMessage.FromSystem(
                    "I will give you list of intern ship posting information and a user information as a json. You will give me top 10 postings that is best suited for that user.  Answer me as a json that has id key with list of id's as value."),
                ChatMessage.FromUser(data),
            ]
        });

        var hasher = MD5.Create();
        var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));

        var suggestion = new UserSuggestion
        {
            CreatedAt = DateTime.UtcNow,
            QuestionHash = Convert.ToBase64String(hash),
            Response = JsonSerializer.Serialize(response),
            UserId = userInfo.Id,
        };

        await _repository.CreateAsync(suggestion);

        return response;
    }

    public async Task<List<InternshipPostingListDto>> GetSuggestions()
    {
        var response = await GetCachedResponse();
        
        var data = JsonSerializer.Deserialize<OpenAiResponseData>(response.Choices[0].Message.Content!);
        
        var postings = await _internshipPostingService.ListByIds(data!.Id);

        return postings;
    }
}