using Microsoft.IdentityModel.Tokens;
using Railway_Reservation_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Railway_Reservation_System.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #region Token Generator
        public Task<string> CreateCustomerTokenAsync(Customer customer)
        {
          
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, customer.Name));
            claims.Add(new Claim(ClaimTypes.Email, customer.Email));

            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Passenger"));


            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


           return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }



        public Task<string> CreateAdminTokenAsync(Admin admin)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, admin.Name));
            claims.Add(new Claim(ClaimTypes.Email, admin.Email));

            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));


            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            //var token1 = JwtSecurityTokenHandler.CreateToken(token);

            return (Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token)));
            //var tokenstring=new JwtSecurityTokenHandler().WriteToken(token);
            // return tokenstring;
        }
        #endregion
    }
}

