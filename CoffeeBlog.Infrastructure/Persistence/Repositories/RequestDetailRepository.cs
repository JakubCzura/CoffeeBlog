using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

public class RequestDetailRepository : DbEntityBaseRepository<RequestDetail>, IRequestDetailRepository
{
}