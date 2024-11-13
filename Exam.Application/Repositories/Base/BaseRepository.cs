using Container.Models.Pagination;
using Exam.Domain.DbModels;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Exam.Application.Repositories.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ExamContext _context;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ExamContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(IEnumerable<T> Data, PaginationModel Pagination)> GetAllAsync(
              Expression<Func<T, bool>> filter = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
              int pageNumber = 1,
              int pageSize = 10)
        {
            _logger.LogInformation("Fetching records of type {EntityType} with optional filter, order, and pagination.", typeof(T).Name);

            IQueryable<T> query = _context.Set<T>();

            // Apply filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
                _logger.LogInformation("Applying filter to query.");
            }

            // Apply ordering if provided
            if (orderBy != null)
            {
                query = orderBy(query);
                _logger.LogInformation("Applying ordering to query.");
            }

            // Get the total item count before pagination
            int totalItemCount = await query.CountAsync();

            // Apply pagination
            if (pageNumber > 0 && pageSize > 0)
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                _logger.LogInformation("Applying pagination: Page {PageNumber}, Size {PageSize}.", pageNumber, pageSize);
            }

            // Get the paged data
            var data = await query.ToListAsync();

            // Calculate the total page count
            int totalPageCount = pageSize == 0 ? 1 :  (int)Math.Ceiling((double)totalItemCount / pageSize);

            // Create pagination model
            var pagination = new PaginationModel
            {
                totalItemCount = totalItemCount,
                totalPageCount = totalPageCount,
                activePage = pageNumber
            };

            return (data, pagination);
        }



        public async Task<T> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching a record of type {EntityType} with ID: {Id}.", typeof(T).Name, id);
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation("Finding records of type {EntityType} with a specific condition.", typeof(T).Name);
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation("Fetching the first record of type {EntityType} that matches a specific condition.", typeof(T).Name);
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            _logger.LogInformation("Adding a new record of type {EntityType}.", typeof(T).Name);
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _logger.LogInformation("Updating a record of type {EntityType}.", typeof(T).Name);
            _context.Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _logger.LogInformation("Performing soft delete on a record of type {EntityType}.", typeof(T).Name);

            var isActiveProperty = typeof(T).GetProperty("IsActive");
            if (isActiveProperty != null && isActiveProperty.PropertyType == typeof(bool))
            {
                isActiveProperty.SetValue(entity, false);
                _context.Set<T>().Update(entity);
                await SaveChangesAsync();
            }
            else
            {
                _logger.LogError("IsActive property not found or not of type bool on entity {EntityType}.", typeof(T).Name);
                throw new InvalidOperationException($"The entity {typeof(T).Name} does not contain an 'IsActive' property of type bool.");
            }
        }

        protected async Task SaveChangesAsync()
        {
            _logger.LogInformation("Saving changes to the database.");
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUniqueAsync<TProperty>(Expression<Func<T, TProperty>> propertySelector, TProperty value, int id = 0)
        {
            var dbSet = _context.Set<T>();

            // Build predicate directly without AsExpandable
            var parameter = Expression.Parameter(typeof(T), "e");

            // Build condition e => e.Property == value
            var propertyExpression = Expression.Invoke(propertySelector, parameter);
            var valueExpression = Expression.Constant(value, typeof(TProperty));
            var equalsExpression = Expression.Equal(propertyExpression, valueExpression);

            // Combine with id check if necessary
            Expression predicateBody = equalsExpression;
            if (id != 0)
            {
                // Add condition for id if it's not zero
                var idExpression = Expression.Property(parameter, "Id");
                var notEqualIdExpression = Expression.NotEqual(idExpression, Expression.Constant(id));
                predicateBody = Expression.AndAlso(equalsExpression, notEqualIdExpression);
            }

            // Compile the predicate
            var predicate = Expression.Lambda<Func<T, bool>>(predicateBody, parameter);

            // Execute asynchronous query with compiled predicate
            return !await dbSet.AnyAsync(predicate);
        }


    }
}
