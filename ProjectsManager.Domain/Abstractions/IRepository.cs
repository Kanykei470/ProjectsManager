using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManager.Domain.Entities.Frameworks;

namespace ProjectsManager.Domain.Abstractions
{
    public interface IRepository <T> where T : class
    {
        Task<Result> Create (T entity);

        Task<Result> Update (T entity);

        Task<Result> Delete (T entity);

        Task<Result<List<T>>> GetAll ();
        Task<Result<T>> GetById(int Id);
    }
}
