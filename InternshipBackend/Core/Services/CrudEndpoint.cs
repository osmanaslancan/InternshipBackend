using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Core.Services;

public class CrudEndpoint<TDto, TData>(IGenericEntityService<TDto, TData> service) : BaseEndpoint
    where TData : class
{
    protected IGenericEntityService<TDto, TData> Service { get; } = service;

    [HttpPost]
    public async Task<ServiceResponse> CreateAsync([FromBody] TDto Dto)
    {
        await Service.CreateAsync(Dto);
        return new EmptyResponse();
    }

    [HttpPost("{id:int}")]
    public async Task<ServiceResponse> UpdateAsync([FromRoute] int id, [FromBody] TDto Dto)
    {
        await Service.UpdateAsync(id, Dto);
        return new EmptyResponse();
    }

    [HttpPost("{id:int}")]
    public async Task<ServiceResponse> DeleteAsync([FromRoute] int id)
    {
        await Service.DeleteAsync(id);
        return new EmptyResponse();
    }
}
