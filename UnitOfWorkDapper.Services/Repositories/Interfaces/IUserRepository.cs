using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;

namespace UnitOfWorkDapper.Services.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task<bool> InsertAsync(User user);

        Task<bool> UpdateAsync(User user);
    }
}