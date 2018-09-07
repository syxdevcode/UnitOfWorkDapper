using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;

namespace UnitOfWorkDapper.Services.Repositories.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<UserInfo>> GetAllAsync();

        Task<UserInfo> GetByIdAsync(int id);

        Task<bool> InsertAsync(UserInfo userInfo);

        Task<bool> UpdateAsync(UserInfo userInfo);
    }
}