using Microsoft.EntityFrameworkCore;
using SPHSS.Repository.Base;
using SPHSS.Repository.Models;

namespace SPHSS.Repository
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public UserAccountRepository() { }

        public async Task<UserAccount> GetUserAccountAsync(string userName, string password)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            u.UserName == userName &&
            u.Password == password &&
            u.IsActive == true);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            //u.Email == email &&
            //u.Password == password &&
            //u.IsActive == true);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            //u.Phone == phone &&
            //u.Password == password &&
            //u.IsActive == true);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            //u.EmployCode == employCode &&
            //u.Password == password &&
            //u.IsActive == true);
        }
    }
}
