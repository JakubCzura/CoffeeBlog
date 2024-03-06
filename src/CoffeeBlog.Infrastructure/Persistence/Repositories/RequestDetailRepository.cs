using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class RequestDetailRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<RequestDetail>(coffeeBlogDbContext), IRequestDetailRepository
{
}