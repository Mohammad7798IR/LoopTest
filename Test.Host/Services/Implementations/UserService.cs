using Test.Host.DTOs;
using Test.Host.Entities;
using Test.Host.Repositories.Interfaces;
using Test.Host.Services.Interfaces;

namespace Test.Host.Services.Implementations
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AddUserResult> AddUser(AddUserDTO addUserDTO)
        {
            if (await _userRepository.IsPhoneNumberExists(addUserDTO.PhoneNumber))
                return AddUserResult.PhoneNumberExists;

            if (await _userRepository.IsNationalCodeExists(addUserDTO.NationalCode))
                return AddUserResult.NationalCodeExists;

            var newUser = new ApplicationUser()
            {
                BirthDate = addUserDTO.BirthDate,
                FirstName = addUserDTO.FirstName,
                LastName = addUserDTO.LastName,
                NationalCode = addUserDTO.NationalCode,
                PhoneNumber = addUserDTO.PhoneNumber,
            };

            await _userRepository.AddUser(newUser);
            await _userRepository.SaveChanges();
            return AddUserResult.Success;
        }

        public async Task<UpdateUserResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
                return UpdateUserResult.UserNotFound;

            _userRepository.RemoveUser(user);
            await _userRepository.SaveChanges();


            return UpdateUserResult.Success;
        }

        public async Task<UpdateUserResult> UpdateUser(string id, UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
                return UpdateUserResult.UserNotFound;

            user.BirthDate = updateUserDTO.BirthDate;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.PhoneNumber = updateUserDTO.PhoneNumber;


            await _userRepository.SaveChanges();

            return UpdateUserResult.Success;
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userRepository.AllUsers();
        }
    }
    public partial class UserService
    {

    }
}
