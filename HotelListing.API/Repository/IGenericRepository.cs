using HotelListing.API.Models;

namespace HotelListing.API.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(PageQueryParameters queryParameters);

        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<bool> Exists(int id);
    }
}
