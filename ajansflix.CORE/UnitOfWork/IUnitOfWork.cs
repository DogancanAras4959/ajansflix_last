using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.CORE.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class, IEntity
    {
        IRepository<T> Repository { get; }
        Task<int> Commit();
        void RollBack();

    }
}
