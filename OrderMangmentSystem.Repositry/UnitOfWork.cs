using Microsoft.EntityFrameworkCore;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Repositries;
using OrderMangmentSystem.Repositry.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangmentSystem.Repositry
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private Hashtable _repositries;
		public UnitOfWork(StoreContext dbContext)
        {
			_dbContext = dbContext;
			
			_repositries = new Hashtable();
		}
        public async Task<int> CompleteAsync()
		=>
			await _dbContext .SaveChangesAsync();


		public async ValueTask DisposeAsync()
		=>await  _dbContext .DisposeAsync();

		public IGenericRepositry<TEntity> Repositry<TEntity>() where TEntity : BaseEntity
		{
			var key = typeof(TEntity).Name;
			if (!_repositries.ContainsKey(key))
			{
				var repositry = new GenericRepositry<TEntity>(_dbContext);
				_repositries.Add(key, repositry);
			}
			return _repositries[key] as IGenericRepositry<TEntity>;
		}
	}
}
