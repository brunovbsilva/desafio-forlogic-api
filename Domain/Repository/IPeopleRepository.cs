using Domain.Entities;
using Domain.Services.Responses;

namespace Domain.Repository
{
    public interface IPeopleRepository : IBaseRepository<Person>
    {
        Task<HomeScoreResponse> GetHomeScoreAsync();
    }
}
