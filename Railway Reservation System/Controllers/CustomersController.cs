using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Railway_Reservation_System.Repositories;

namespace Railway_Reservation_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("NgOrigins")]

    public class CustomersController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ITokenHandler tokenHandler;

        public CustomersController(ICustomerRepository customerRepository,ITokenHandler tokenHandler)
        {
            this.customerRepository = customerRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var customers= await customerRepository.GetAllAsync();

            return Ok(customers);
        }

        [HttpPost]
        [Route("CustomerLogin")]
        public async Task<IActionResult> CustomerLoginAsync(Models.DTOs.LoginRequest loginRequest)
        {



            //Check Username and Password

            var user = await customerRepository.AuthenticateCustomerAsync(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                //Generate JWT token and send it back
                var token = await tokenHandler.CreateCustomerTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("Username or Password is Incorrect.");
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetCustomerAsync")]
        [Authorize(Roles = "Admin,Passenger")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var customer=await customerRepository.GetAsync(id);
            if(customer==null)
            {
                return NotFound();
            }
            return Ok(customer);

        }

        [HttpPost("CreateCustomer")]
        
        
        public async Task<IActionResult> AddCustomerAsync(Models.Customer addCustomer)
        {   

            var customer = new Models.Customer()
            {
                Name = addCustomer.Name,
                Email = addCustomer.Email,
                Gender = addCustomer.Gender,
                Password = addCustomer.Password,
                Age = addCustomer.Age,
            };
            customer=await customerRepository.AddAsync(customer);
            return CreatedAtAction(nameof(GetCustomerAsync), new {id=customer.CustomerId}, customer);
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            var customer = await customerRepository.DeleteAsync(id);

            if(customer==null)
            {
                return NotFound();
            }

            return Ok(customer);
        }



        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustomerAsync([FromRoute]int id, [FromBody]Models.Customer updateCustomer)
        {

            var customer = new Models.Customer()
            {
                Name = updateCustomer.Name,
                Email = updateCustomer.Email,
                Gender = updateCustomer.Gender,
                Password = updateCustomer.Password,
                Age = updateCustomer.Age,
            };

            customer = await customerRepository.UpdateAsync(id, customer);

            if(customer==null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        #region Private Methods

         private bool ValidateAddCustomerAsync(Models.Customer addCustomer)
        {
            if(addCustomer==null)
            {
                ModelState.AddModelError(nameof(addCustomer), $"Data Required. Fields cannot be Empty");

                return false;
            }

            if(string.IsNullOrWhiteSpace(addCustomer.Name))
            {
                ModelState.AddModelError(nameof(addCustomer.Name), $"{nameof(addCustomer.Name)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addCustomer.Email))
            {
                ModelState.AddModelError(nameof(addCustomer.Email), $"{nameof(addCustomer.Email)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addCustomer.Password))
            {
                ModelState.AddModelError(nameof(addCustomer.Password), $"{nameof(addCustomer.Password)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addCustomer.Gender))
            {
                ModelState.AddModelError(nameof(addCustomer.Gender), $"{nameof(addCustomer.Gender)} cannot be null emnpty or whitespace");
            }
            if (addCustomer.Age<16)
            {
                ModelState.AddModelError(nameof(addCustomer.Age), $"{nameof(addCustomer.Age)} cannot be less than 16");
            }



            if (ModelState.ErrorCount>0)
            {
                return false;
            }

            return false;

        }

         private bool ValidateUpdateCustomerAsync(Models.Customer updateCustomer)
        {
            if (updateCustomer == null)
            {
                ModelState.AddModelError(nameof(updateCustomer), $"Data Required. Fields cannot be Empty");

                return false;
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Name))
            {
                ModelState.AddModelError(nameof(updateCustomer.Name), $"{nameof(updateCustomer.Name)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Email))
            {
                ModelState.AddModelError(nameof(updateCustomer.Email), $"{nameof(updateCustomer.Email)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Password))
            {
                ModelState.AddModelError(nameof(updateCustomer.Password), $"{nameof(updateCustomer.Password)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(updateCustomer.Gender))
            {
                ModelState.AddModelError(nameof(updateCustomer.Gender), $"{nameof(updateCustomer.Gender)} cannot be null emnpty or whitespace");
            }
            if (updateCustomer.Age < 16)
            {
                ModelState.AddModelError(nameof(updateCustomer.Age), $"{nameof(updateCustomer.Age)} cannot be less than 16");
            }



            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return false;
        }

        #endregion
    }
}
