using CoffeeBlog.Domain.Entities.Requests;
using CoffeeBlog.Domain.Interfaces.Requests;
using CoffeeBlog.Infrastructure.Repositories.Base;

namespace CoffeeBlog.Infrastructure.Repositories.Requests;

public class RequestDetailRepository : DbEntityBaseRepository<RequestDetail>, IRequestDetailRepository
{
}