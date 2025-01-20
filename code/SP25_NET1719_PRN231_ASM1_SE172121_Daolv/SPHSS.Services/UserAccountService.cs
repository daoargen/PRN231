using SPHSS.Repository;
using SPHSS.Repository.Models;

namespace SPHSS.Services
{

    public class UserAccountService
    {
        private readonly UserAccountRepository _repository;
        public UserAccountService() => _repository = new UserAccountRepository();
        public async Task<UserAccount> Authenticate(string userName, string password)
        {
            return await _repository.GetUserAccountAsync(userName, password);
        }
    }
}
