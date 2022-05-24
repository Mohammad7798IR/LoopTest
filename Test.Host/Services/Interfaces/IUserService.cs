using Test.Host.DTOs;
using Test.Host.Entities;

namespace Test.Host.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();

        Task<ApplicationUser> GetUser(string id);

        Task<AddUserResult> AddUser(AddUserDTO addUserDTO);

        Task<UpdateUserResult> UpdateUser(string id, UpdateUserDTO updateUserDTO);

        Task<UpdateUserResult> DeleteUser(string id);
    }
}
