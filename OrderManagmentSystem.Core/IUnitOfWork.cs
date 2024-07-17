using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepositry<TEntity> Repositry<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
