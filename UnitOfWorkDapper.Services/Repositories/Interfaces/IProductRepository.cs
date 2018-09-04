using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;

namespace UnitOfWorkDapper.Services.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<bool> InsertAsync(Product product);

        Task<bool> UpdateAsync(Product product);
    }
}
