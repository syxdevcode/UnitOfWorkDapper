using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Core;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;

namespace UnitOfWorkDapper.Services.Repositories
{
    public class UserAddressRepository: IUserAddressRepository
    {
        private readonly DapperDBContext _context;

        public UserAddressRepository(IContext context)
        {
            _context = (DapperDBContext)context;
        }

        public async Task<IEnumerable<UserAddress>> GetAllAsync()
        {
            return await _context.QueryAsync<UserAddress>("SELECT * FROM UserAddress");
        }

        public async Task<UserAddress> GetByIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<UserAddress>("SELECT * FROM UserAddress WHERE Id=@Id", new { Id = id });
        }

        public async Task<bool> InsertAsync(UserAddress userAddress)
        {
            var b = await _context.ExecuteAsync("INSERT INTO UserAddress VALUES (@Id ,@UserId,@RecName,@PhoneNumber,@IsDefault,@Province,@City,@Regin, @Street,@AddTime,@IsDelete)", userAddress) > 0;
            return b;
        }

        public async Task<bool> UpdateAsync(UserAddress userAddress)
        {
            return await _context.ExecuteAsync("UPDATE UserAddress SET RecName=@RecName,PhoneNumber=@PhoneNumber,IsDefault=@IsDefault,Province=@Province,City=@City,Regin=@Regin,Street= @Street,IsDelete=@IsDelete WHERE Id=@Id", userAddress) > 0;
        }
    }
}