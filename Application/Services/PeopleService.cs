using Domain.Common;
using Domain.Entities;
using Domain.Entities.Dtos;
using Domain.Repository;
using Domain.SeedWork.Notification;
using Domain.Services;
using Domain.Services.Requests;
using Domain.Services.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services;

public class PeopleService(INotification notification, IPeopleRepository repository, IDistributedCache cache) : BaseService(notification), IPeopleService
{
    public Task<BaseResponse<HomeScoreResponse>> GetHomeScoreAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<PersonDto>>> ListPersonAsync()
    {
        var list = await repository.GetAll().Select(x => (PersonDto)x).ToListAsync();
        return new GenericResponse<List<PersonDto>>(list);
    }

    public async Task<BaseResponse<PersonDto>> RegisterPersonAsync(RegisterPersonRequest request)
    {
        var person = Person.Factory.Create(request, notification);
        await repository.InsertWithSaveChangesAsync(person);
        cache.Remove("GET:/api/people");
        return new GenericResponse<PersonDto>(person);
    }
}
