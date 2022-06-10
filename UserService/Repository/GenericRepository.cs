using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Data;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;
using UserService.entities;


namespace HotelListing.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
    
        public GenericRepository(UserDbContext _context, IMapper mapper)
        {
            this._context = _context;
            this._mapper = mapper;
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

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(PageQueryParameters queryParameters)
        {
            /*Get total count of records*/
            var totalSize = await _context.Set<T>().CountAsync();

            var items = await _context.Set<T>()
                /*Specify from where to start*/
                .Skip(queryParameters.StartIndex)
                /*Specify the paging count to retrieve*/
                .Take(queryParameters.PageSize)
                /*For efficient querying, only query those columns 
                 * required by DTO as specified by _mapper configuration*/
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                /*Retrieve paged records*/
                .ToListAsync();
            /*Construct and return PagedResult with records, and paging information*/
            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = queryParameters.PageNumber,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize
            };
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
        public User GetByEmail(String emailId)
        {
            var User = _context.Users
    .Where(m => m.EmailId == emailId)
    .Select(m => m)
    .SingleOrDefault();
            return User;
        }

    }
}
