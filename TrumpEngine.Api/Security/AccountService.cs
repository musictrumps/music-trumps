using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Payloads;
using TrumpEngine.Api.Models;

namespace TrumpEngine.Api.Security
{
    public interface IAccountService
    {
        Task<string> Authenticate(LoginApiRequest request);
       
    }
    public class AccountService : IAccountService
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        public AccountService(IFirebaseAuthService firebase)
        {
            _firebaseAuthService = firebase;
        }


        public async  Task<string> Authenticate(LoginApiRequest request)
        {
            var firebaseRequest = new VerifyPasswordRequest()
            {
                Email = request.Login,
                Password = request.Password
            };
            var response = await _firebaseAuthService.VerifyPasswordAsync(firebaseRequest);
            return response.IdToken;
        }

    }
}
