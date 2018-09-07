using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;

namespace UnitOfWorkDapper.Services.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<bool> SaveAsync(Product product);

        Task<Product> GetByIdAsync(int id);
    }
}