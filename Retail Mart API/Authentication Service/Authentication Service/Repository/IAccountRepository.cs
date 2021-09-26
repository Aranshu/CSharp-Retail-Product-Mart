using Authentication_Service.Model;

namespace Authentication_Service.Repository
{
    public interface IAccountRepository
    {
        DetailModel Login(LoginModel loginModel);
        int Register(RegisterModel registerModel);
    }
}
