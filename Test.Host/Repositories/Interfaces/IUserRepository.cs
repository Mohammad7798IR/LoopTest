using Test.Host.Entities;

namespace Test.Host.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> AllUsers();

        Task<ApplicationUser> GetUser(string id);

        Task<bool> IsPhoneNumberExists(string phoneNumber);

        Task<bool> IsNationalCodeExists(string nationalCode);

        Task AddUser(ApplicationUser newUser);

        void RemoveUser(ApplicationUser newUser);

        Task SaveChanges();
    }
}
