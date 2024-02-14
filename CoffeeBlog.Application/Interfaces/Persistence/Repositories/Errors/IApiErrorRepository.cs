using CoffeeBlog.Application.Interfaces.Persistence.Repositories.DbEntitiesBase;
using CoffeeBlog.Domain.Entities.Errors;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories.Errors;

public interface IApiErrorRepository : IDbEntityBaseRepository<ApiError>
{
}