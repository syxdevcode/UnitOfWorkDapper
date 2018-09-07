using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Core;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;

namespace UnitOfWorkDapper.Services.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DapperDBContext _context;

        public UserRepository(IContext context)
        {
            _context = (DapperDBContext)context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.QueryAsync<User>("SELECT * FROM User");
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<User>("SELECT * FROM User WHERE Id=@Id", new { Id = id });
        }

        public async Task<bool> InsertAsync(User user)
        {
            var b = await _context.ExecuteAsync("INSERT INTO UserInfo VALUES (@Id ,@UserName,@NickName,@PassWord,@RegisterTime,@Grade,@AddTime,@IsDelete)", user) > 0;
            return b;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await _context.ExecuteAsync("UPDATE UserAddress SET UserName=@UserName,NickName=@NickName,PassWord=@PassWord,Grade=@Grade,IsDelete=@IsDelete WHERE Id=@Id", user) > 0;
        }
    }
}