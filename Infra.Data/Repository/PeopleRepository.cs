using Domain.Entities;
using Domain.Repository;
using Domain.Services.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class PeopleRepository(IUnitOfWork unitOfWork) : BaseRepository<Person>(unitOfWork), IPeopleRepository
    {
        public async Task<HomeScoreResponse> GetHomeScoreAsync()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            return new HomeScoreResponse
            {
                TotalScore = await GetAll().CountAsync(),
                PendingScore = await Get(x => !x.Active).CountAsync(),
                LastMonthScore = await Get(x => x.CreatedAt >= oneMonthAgo).CountAsync()
            };
        }
    }
}
