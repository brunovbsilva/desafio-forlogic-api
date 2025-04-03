using Domain.Services;

namespace API.Controllers;

public class PeopleController(IPeopleService service) : BaseController
{
}
