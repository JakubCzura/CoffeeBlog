using CoffeeBlog.Application.Interfaces.Persistence.Repositories.DbEntitiesBase;
using CoffeeBlog.Domain.Entities.Requests;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories.Requests;

public interface IRequestDetailRepository : IDbEntityBaseRepository<RequestDetail>
{
}