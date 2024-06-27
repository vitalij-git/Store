using Core.Models;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IReadOnlyList<T>> GetListAllAsync();

        public Task<T> GetEntitywithSpec(ISpecification<T> specification);
        public Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> specification);
        public Task<int> CountAsync(ISpecification<T> specification);
    }
}
