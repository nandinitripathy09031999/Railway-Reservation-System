using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Railway_Reservation_System.Models;
using Railway_Reservation_System.Repositories;

namespace Railway_Reservation_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepository;
        private readonly ITokenHandler tokenHandler;

        public AdminController(IAdminRepository adminRepository,ITokenHandler tokenHandler)
        {
            this.adminRepository = adminRepository;
            this.tokenHandler = tokenHandler;
        }


        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> AdminLoginAsync(Models.DTOs.LoginRequest loginRequest)
        {
            //Check Username and Password
                 
                var user = await adminRepository.AuthenticateAdminAsync(loginRequest.Username, loginRequest.Password);

                if (user != null)
                {
                    //Generate JWT token and send it back
                    var token = await tokenHandler.CreateAdminTokenAsync(user);
                 return Ok((token));
                
                }
                return BadRequest("Username or Password is Incorrect.");
        }



    }
   
}
