using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Authentication_Service.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product_Service.Context;

namespace Authentication_Service.Repository
{
    public class AccountRepository : IAccountRepository
    {
        /*
         * Db Context Object
         */
        private readonly AccountContext _accountContext;

        /*
         * Iconfiguration Object
         */
        private readonly IConfiguration _configuration;

        /*
         * Dependency Injection
         */
        public AccountRepository(AccountContext accountContext, IConfiguration configuration)
        {
            _accountContext = accountContext;
            _configuration = configuration;
        }

        /*
         * Login Fuction
         */
        public DetailModel Login(LoginModel loginModel)
        {
            var validUser = _accountContext.RegisterModels.Where(l => l.Email == loginModel.Email && l.Password == loginModel.Password).FirstOrDefault();
            if (validUser == null)
                return null;
            DetailModel detailModelObject = new DetailModel();
            detailModelObject.CustomerId = validUser.Id;
            detailModelObject.FirstName = validUser.FirstName;
            detailModelObject.Address = validUser.Address;
            detailModelObject.Token = GenerateToken(loginModel);
            return detailModelObject;
        }

        /*
         * Token Generator
         */
        private string GenerateToken(LoginModel loginModel)
        {
            string Key = _configuration["Token:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //string userRole = "Member";

            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Role, userRole),
                new Claim("UserId", loginModel.Email)
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(15),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /*
         * Registeration
         */
        public int Register(RegisterModel registerModel)
        {
            _accountContext.RegisterModels.Add(registerModel);
            _accountContext.SaveChanges();
            return registerModel.Id;
        }
    }
}
