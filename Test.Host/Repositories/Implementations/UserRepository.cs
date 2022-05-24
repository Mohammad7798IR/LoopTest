using Microsoft.EntityFrameworkCore;
using Test.Host.Entities;
using Test.Host.Repositories.Interfaces;
using TestProject.Host.Context;

namespace Test.Host.Repositories.Implementations
{
    public partial class UserRepository : IUserRepository
    {
        private readonly TestDBContext _context;

        public UserRepository(TestDBContext context)
        {
            _context = context;
        }


    }
    public partial class UserRepository
    {
        public async Task<List<ApplicationUser>> AllUsers()
        {
            return await _context.Users.Where(a => a.NationalCode != null).ToListAsync();
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _context.Users.Where(a => a.NationalCode == id).SingleOrDefaultAsync();
        }

        public async Task<bool> IsNationalCodeExists(string nationalCode)
        {
           return await _context.Users.AnyAsync(a=>a.NationalCode == nationalCode);
        }

        public async Task<bool> IsPhoneNumberExists(string phoneNumber)
        {
            return await _context.Users.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }

    public partial class UserRepository
    {
        public async Task AddUser(ApplicationUser newUser)
        {
           await _context.Users.AddAsync(newUser);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void RemoveUser(ApplicationUser newUser)
        {
            _context.Remove(newUser);
        }
    }
}
