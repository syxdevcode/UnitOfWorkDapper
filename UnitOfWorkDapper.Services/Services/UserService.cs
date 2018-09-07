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

        public async Task<bool> ImportUser(User user, UserInfo userInfo, List<UserAddress> listAddress)
        {
            var b1 = await _userRepository.UpdateAsync(user);

            var b2 = false;

            if (!string.IsNullOrWhiteSpace(userInfo.UserId))
                b2 = await _userInfoRepository.UpdateAsync(userInfo);
            else b2 = true;

            var b3 = false;

            if (listAddress.Count == 0) b3 = true;

            foreach (var item in listAddress)
            {
                var b4 = await _userAddressRepository.UpdateAsync(item);

                if (!b4) break;
                else b3 = true;
            }

            bool result = false;

            if (b1 && b2 && b3)
            {
                result = _unitOfWork.SaveChanges();
            }

            return result;
        }
    }
}