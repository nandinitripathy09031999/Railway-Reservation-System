using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Railway_Reservation_System.Repositories;

namespace Railway_Reservation_System.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentsController(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            var payments = await paymentRepository.GetAllAsync();

            return Ok(payments);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetPaymentAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentAsync(int id)
        {
            var payment = await paymentRepository.GetAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPaymentAsync(Models.Payment addPayment)
        {

            //if(!ValidateAddPaymentAsync(addPayment))
            //{
            //    return BadRequest(ModelState);
            //}
            var payment = new Models.Payment()
            {
                Amount = addPayment.Amount,
                CustomerId = addPayment.CustomerId,
                ReservationId = addPayment.ReservationId,
                
            };
            payment = await paymentRepository.AddAsync(payment);
            return CreatedAtAction(nameof(GetPaymentAsync), new { id = payment.Id }, payment);
        }



        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePaymentAsync(int id)
        {
            var payment = await paymentRepository.DeleteAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }


        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdatePaymentAsync([FromRoute] int id, [FromBody] Models.Payment updatePayment)
        {

            //if(!ValidateUpdatePaymentAsync(updatePayment))
            //{
            //    return BadRequest(ModelState);
            //}

            var payment = new Models.Payment()
            {
                Amount=updatePayment.Amount,    
                CustomerId=updatePayment.CustomerId,
                ReservationId=updatePayment.ReservationId,  
            };

            payment = await paymentRepository.UpdateAsync(id, payment);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }



        #region Private Methods

        private bool ValidateAddPaymentAsync(Models.Payment addPayment)
        {
            if (addPayment == null)
            {
                ModelState.AddModelError(nameof(addPayment), $"Data Required. Fields cannot be Empty");

                return false;
            }

            if (addPayment.CustomerId <= 0)
            {
                ModelState.AddModelError(nameof(addPayment.CustomerId), $"Please enter a valid {nameof(addPayment.CustomerId)} ");

            }

            if (addPayment.Amount <= 0)
            {
                ModelState.AddModelError(nameof(addPayment.Amount), $"{nameof(addPayment.Amount)} cannot be less than 0");
            }

            if (addPayment.ReservationId <= 0)
            {
                ModelState.AddModelError(nameof(addPayment.ReservationId), $"Please enter a valid{nameof(addPayment.ReservationId)} ");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return false;

        }

        private bool ValidateUpdatePaymentAsync(Models.Payment updatePayment)
        {
            if (updatePayment == null)
            {
                ModelState.AddModelError(nameof(updatePayment), $"Data Required. Fields cannot be Empty");

                return false;
            }

            if (updatePayment.CustomerId <= 0)
            {
                ModelState.AddModelError(nameof(updatePayment.CustomerId), $"Please enter a valid {nameof(updatePayment.CustomerId)} ");

            }

            if (updatePayment.Amount <= 0)
            {
                ModelState.AddModelError(nameof(updatePayment.Amount), $"{nameof(updatePayment.Amount)} cannot be less than 0");
            }

            if (updatePayment.ReservationId <= 0)
            {
                ModelState.AddModelError(nameof(updatePayment.ReservationId), $"Please enter a valid{nameof(updatePayment.ReservationId)} ");
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
