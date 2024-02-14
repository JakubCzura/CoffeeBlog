using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Requests;
using CoffeeBlog.Domain.Entities.Requests;
using CoffeeBlog.Infrastructure.Persistence.Repositories.DbEntitiesBase;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories.Requests;

public class RequestDetailRepository : DbEntityBaseRepository<RequestDetail>, IRequestDetailRepository
{
}