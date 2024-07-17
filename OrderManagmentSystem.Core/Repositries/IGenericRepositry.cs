using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Repositries
{
	public interface IGenericRepositry<T> where T:BaseEntity 
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification <T>spec);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);

		Task Add(T entity);

		
		Task Update(T entity);


	}
}
