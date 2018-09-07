using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Core;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;

namespace UnitOfWorkDapper.Services.Repositories
{
    public class UserInfoRepository: IUserInfoRepository
    {
        private readonly DapperDBContext _context;

        public UserInfoRepository(IContext context)
        {
            _context = (DapperDBContext)context;
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync()
        {
            return await _context.QueryAsync<UserInfo>("SELECT * FROM UserInfo");
        }

        public async Task<UserInfo> GetByIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WHERE Id=@Id", new { Id = id });
        }

        public async Task<bool> InsertAsync(UserInfo userInfo)
        {
            var b = await _context.ExecuteAsync("INSERT INTO UserInfo VALUES (@Id ,@UserId,@Age,@Birthday,@PhoneNumber,@Email,@QQ,@AddTime,@IsDelete)", userInfo) > 0;
            return b;
        }

        public async Task<bool> UpdateAsync(UserInfo userInfo)
        {
            return await _context.ExecuteAsync("UPDATE UserAddress SET Age=@Age,Birthday=@Birthday,PhoneNumber=@PhoneNumber,Email=@Email,QQ=@QQ,IsDelete=@IsDelete WHERE Id=@Id", userInfo) > 0;
        }
    }
}