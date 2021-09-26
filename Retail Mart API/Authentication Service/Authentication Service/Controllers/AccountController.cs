using Authentication_Service.Model;
using Authentication_Service.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /*
         * IAccountRepository Object
         */
        private readonly IAccountRepository _accountRepository;

        /*
         * Logging Object
         */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountController));

        /*
         * Dependency Injection
         */
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _log4net.Info("Logger initiated");

        }

        /*
         * Login Function
         * api/Account/Login
         * Input : LoginModel
         * Output : DetailModel
         */
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return Unauthorized("Please enter your credentials to login");
                _log4net.Info("Please enter your credentials to login");
            }
            var validUser = _accountRepository.Login(loginModel);
            if (validUser == null)
            {
                return Unauthorized("You are not a valid user... Please check your mail and password!!!");
                _log4net.Info("You are not a valid user... Please check your mail and password!!!");
            }
            _log4net.Info("login successfull");
            return Ok(validUser);
        }

        /*
         * Login Function
         * api/Account/Register
         * Input : RegisterModel
         * Output : Status
         */
        [HttpPost("Register")]
        public IActionResult Register(RegisterModel registerModel)
        {
            if(registerModel==null)
            {
                return BadRequest();
                _log4net.Info("Registeration Failed");
            }
            _log4net.Info("Registeration Successfull");
            return Ok(_accountRepository.Register(registerModel));
        }
    }
}
