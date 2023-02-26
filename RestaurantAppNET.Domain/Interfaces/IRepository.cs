using CRMAppNET.Domain.Entities;
using CRMAppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAppNET.Domain.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        T Add(T entity);

        bool Remove (int id);

        T GetById(int id);

        ICollection<T> GetList(Func<T,bool> expression=null);

        T Update(int id, T entity);
    }
}
