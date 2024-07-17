using Microsoft.EntityFrameworkCore;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Repositries;
using OrderManagmentSystem.Core.Specifications;
using OrderMangmentSystem.Repositry.Data;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangmentSystem.Repositry
{
	public class GenericRepositry<T> :   IGenericRepositry<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepositry(StoreContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task Add(T entity)
		=>await _dbContext .Set<T>().AddAsync (entity);

		public Task AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(T entity)
		{
			throw new NotImplementedException();
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync(); ;
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplaySpecification(spec).ToListAsync() ;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id); ;
		}

		public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplaySpecification(spec).FirstOrDefaultAsync(); ;
		}

		public Task Update(T entity)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}

		private IQueryable <T>ApplaySpecification(ISpecification <T> spec)
		{
			return  SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
		}
	}
}
