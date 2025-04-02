using Domain.Entities;
using Domain.Repository;

namespace Infra.Data.Repository
{
    public class PeopleRepository(IUnitOfWork unitOfWork) : BaseRepository<Person>(unitOfWork), IPeopleRepository
    {
    }
}
