using CoffeeBlog.Domain.Entities.Errors;
using CoffeeBlog.Domain.Interfaces.Base;

namespace CoffeeBlog.Domain.Interfaces.Errors;

public interface IApiErrorRepository : IDbEntityBaseRepository<ApiError>
{
}