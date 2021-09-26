using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Retail_Product_Management_System.Models;
using System.Diagnostics;

namespace Retail_Product_Management_System.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Configuration Object
         */
        private readonly IConfiguration _configuration;

        /*
         * Logging Object
         */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));

        /*
         * Dependency Injection
         */
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _log4net.Info("Logger in HomeController");
        }

        /*
         * About Page
         */
        public IActionResult Index()
        {
            _log4net.Info("About Us Page");
            return View();
        }

        /*
         * Team Page
         */
        public IActionResult Team()
        {
            _log4net.Info("Team Page");
            return View();
        }

        /*
         * Contact Page
         */
        public IActionResult Contact()
        {
            _log4net.Info("Contact Page");
            return View();
        }
        
        /*
         * Privacy Page
         */
        public IActionResult Privacy()
        {
            _log4net.Info("Privacy Page");
            return View();
        }

        /*
         * Login Page
         */
        public IActionResult Login()
        {
            return View();
        }

        /*
         * Login Verification
         * Input : LoginModel
         * Output : DetailModel
         */
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            _log4net.Info("User is logging in");
            string tokenURI = _configuration.GetValue<string>("MyLinkValue:tokenUri") + "/api/Account/Login";
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(tokenURI, content))
                {
                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Login failed");
                        _log4net.Info("Entered wrong credentials");
                        ViewBag.Message = "Please Enter valid credentials entering wrong credentials";
                        return View("Login");
                    }
                    _log4net.Info("Login Successful and token generated");

                    var ApiResponse = result.Content.ReadAsAsync<DetailModel>();
                    ApiResponse.Wait();
                    var detailModel = ApiResponse.Result;

                    int CustomerId = detailModel.CustomerId;
                    string FirstName = detailModel.FirstName;
                    string Address = detailModel.Address;
                    string strtoken = detailModel.Token;

                    HttpContext.Session.SetInt32("customerId", CustomerId);
                    HttpContext.Session.SetString("firstname", FirstName);
                    HttpContext.Session.SetString("address", Address);
                    HttpContext.Session.SetString("token", strtoken);
                }
            }
            return RedirectToAction("Index", "Customer");
        }

        /*
         * Registeration Page
         */
        public IActionResult Register()
        {
            return View();
        }

        /*
         * Registeration Save Content
         * Input : RegisterModel
         * Output : Status
         */
        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            _log4net.Info("User is registering in");

            string tokenURI = _configuration.GetValue<string>("MyLinkValue:tokenUri") + "/api/Account/Register";

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(tokenURI, content);
                response.Wait();
                var result = response.Result;

                if (!result.IsSuccessStatusCode)
                {
                    _log4net.Info("Registration failed");
                    ViewBag.Message = "Username Already exists !!";
                    return View("Register");
                }
                _log4net.Info("Registration Successful");
            }
            return RedirectToAction("Login", "Home");
        }

        /*
         * Logout the user
         */
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            _log4net.Info("Logout Successful");
            return RedirectToAction("Login", "Home");
        }

        /*
         * Error Page
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
