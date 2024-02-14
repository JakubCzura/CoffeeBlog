using CoffeeBlog.Domain.Entities.Requests;

namespace CoffeeBlog.Domain.Interfaces;

public interface IRequestDetailInterface
{
    public void CreateAsync(RequestDetail requestDetail);

    public Task<RequestDetail> GetAsync(int id);

    public Task<List<RequestDetail>> GetAllAsync();

    public Task UpdateAsync(RequestDetail requestDetail);

    public Task DeleteAsync(int id);
}