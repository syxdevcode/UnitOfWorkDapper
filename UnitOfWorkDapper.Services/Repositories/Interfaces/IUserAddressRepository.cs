using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;

namespace UnitOfWorkDapper.Services.Repositories.Interfaces
{
    public interface IUserAddressRepository
    {
        Task<IEnumerable<UserAddress>> GetAllAsync();

        Task<UserAddress> GetByIdAsync(int id);

        Task<bool> InsertAsync(UserAddress userAddress);

        Task<bool> UpdateAsync(UserAddress userAddress);
    }
}