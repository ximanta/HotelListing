using HotelListing.API.data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelListDbContext _context;

        public GenericRepository(HotelListDbContext _context)
        {
            this._context = _context;
        }
        public async Task<T> AddAsync(T entity)
        {
         await _context.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            DbSet<T>  CountrySet= _context.Set<T>();
            var entity = await CountrySet.FindAsync(id);
            _context.Remove(entity);

        }

        public async Task<bool> Exists(int id)
        {
         var entity = await   _context.Set<T>().FindAsync(id);
         return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
   
          return await _context.Set<T>().ToListAsync();
     
        }


        public async Task<T> GetAsync(int? id)
        { 
            if(id is null)
        {
            return null;
        }
   
         return await _context.Set<T>().FindAsync(id);
                  
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
