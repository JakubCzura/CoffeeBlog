using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Errors;
using CoffeeBlog.Domain.Entities.Errors;
using CoffeeBlog.Infrastructure.Persistence.Repositories.DbEntitiesBase;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories.Errors;

public class ApiErrorRepository : DbEntityBaseRepository<ApiError>, IApiErrorRepository
{
}