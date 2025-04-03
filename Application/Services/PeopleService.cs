using Domain.Common;
using Domain.Entities;
using Domain.Entities.Dtos;
using Domain.Repository;
using Domain.SeedWork.Notification;
using Domain.Services;
using Domain.Services.Requests;
using Domain.Services.Responses;

namespace Application.Services;

public class PeopleService(INotification notification, IPeopleRepository repository) : BaseService(notification), IPeopleService
{
    public Task<BaseResponse<HomeScoreResponse>> GetHomeScoreAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<PersonDto>>> ListPersonAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<PersonDto>> RegisterPersonAsync(RegisterPersonRequest request)
    {
        var person = Person.Factory.Create(request, notification);
        await repository.InsertWithSaveChangesAsync(person);
        return new GenericResponse<PersonDto>(person);
    }
}
