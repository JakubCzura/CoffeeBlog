using CoffeeBlog.Domain.Entities.Errors;
using CoffeeBlog.Domain.Interfaces.Errors;
using CoffeeBlog.Infrastructure.Repositories.Base;

namespace CoffeeBlog.Infrastructure.Repositories.Errors;

public class ApiErrorRepository : DbEntityBaseRepository<ApiError>, IApiErrorRepository
{
}