using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;

namespace UnitOfWorkDapper.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<bool> SaveAsync(User user);

        Task<User> GetByIdAsync(int id);

        Task<bool> ImportUser(User user, UserInfo userInfo, List<UserAddress> listAddress);
    }
}
