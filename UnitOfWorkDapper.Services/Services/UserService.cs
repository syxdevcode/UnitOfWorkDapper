using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Core;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;
using UnitOfWorkDapper.Services.Services.Interfaces;

namespace UnitOfWorkDapper.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserAddressRepository userAddressRepository, IUserRepository userRepository, IUserInfoRepository userInfoRepository, IUnitOfWork unitOfWork)
        {
            _userAddressRepository = userAddressRepository;
            _userRepository = userRepository;
            _userInfoRepository = userInfoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> SaveAsync(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Id))
            {
                // update data
                await _userRepository.UpdateAsync(user);
            }
            else
            {
                // insert data
                await _userRepository.InsertAsync(user);
            }
            var b = _unitOfWork.SaveChanges();

            return b;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> ImportUser(User user,UserInfo userInfo,UserAddress userAddress)
        {
            var b1= await _userRepository.UpdateAsync(user);

            var b2 = await _userRepository.UpdateAsync(user);
            var b3 = await _userRepository.UpdateAsync(user);

            bool result = false;

            if (b1 && b2 && b3)
            {
                result = _unitOfWork.SaveChanges();
            }            

            return result;
        }
    }
}