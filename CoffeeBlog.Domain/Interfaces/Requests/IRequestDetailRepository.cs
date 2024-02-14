using CoffeeBlog.Domain.Entities.Requests;
using CoffeeBlog.Domain.Interfaces.Base;

namespace CoffeeBlog.Domain.Interfaces.Requests;

public interface IRequestDetailRepository : IDbEntityBaseRepository<RequestDetail>
{
}