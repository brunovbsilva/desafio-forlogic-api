using Domain.Common;
using Domain.Entities.Dtos;
using Domain.Services.Requests;
using Domain.Services.Responses;

namespace Domain.Services;

public interface IPeopleService
{
    Task<BaseResponse<PersonDto>> RegisterPersonAsync(RegisterPersonRequest request);
    Task<BaseResponse<List<PersonDto>>> ListPersonAsync();
    Task<BaseResponse<HomeScoreResponse>> GetHomeScoreAsync();
}
