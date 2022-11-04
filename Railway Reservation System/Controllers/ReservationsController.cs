using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Railway_Reservation_System.Repositories;

namespace Railway_Reservation_System.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReservationAsync()
        {
            var reservation = await reservationRepository.GetAllAsync();

            return Ok(reservation);
        }

        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetReservationAsync")]
        [Authorize(Roles = "Admin,Passenger")]
        public async Task<IActionResult> GetReservationAsync(int Id)
        {
            var reservation = await reservationRepository.GetAsync(Id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddReservationAsync(Models.Reservation addReservation)
        {
            var reservation = new Models.Reservation()
            {
                trainno = addReservation.trainno,
                CustomerId = addReservation.CustomerId,
                NoOfPeople = addReservation.NoOfPeople,
                SourceStation = addReservation.SourceStation,
                DestinationStation = addReservation.DestinationStation,
                DatetimeOfCreation = addReservation.DatetimeOfCreation,
                Status = addReservation.Status,

            };
            reservation = await reservationRepository.AddAsync(reservation);
            return CreatedAtAction(nameof(GetReservationAsync), new { Id = reservation.Id }, reservation);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,Passenger")]
        public async Task<IActionResult> CancelReservationAsync(int id)
        {
            var reservation = await reservationRepository.CancelReservationAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPut]
        [Route("{Id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateReservationAsync([FromRoute] int Id, [FromBody] Models.Reservation updateReservation)
        {
            var reservation = new Models.Reservation()
            {
                trainno = updateReservation.trainno,
                CustomerId = updateReservation.CustomerId,
                NoOfPeople = updateReservation.NoOfPeople,
                SourceStation = updateReservation.SourceStation,
                DestinationStation = updateReservation.DestinationStation,
                DatetimeOfCreation = updateReservation.DatetimeOfCreation,
                Status = updateReservation.Status,
            };

            reservation = await reservationRepository.UpdateAsync(Id, reservation);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        #region Private Methods

        private bool ValidateAddReservationAsync(Models.Reservation addReservation)
        {
            if (addReservation == null)
            {
                ModelState.AddModelError(nameof(addReservation), $"Data Required. Fields cannot be Empty");

                return false;
            }

            if (string.IsNullOrWhiteSpace(addReservation.SourceStation))
            {
                ModelState.AddModelError(nameof(addReservation.SourceStation), $"{nameof(addReservation.SourceStation)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addReservation.DestinationStation))
            {
                ModelState.AddModelError(nameof(addReservation.DestinationStation), $"{nameof(addReservation.DestinationStation)} cannot be null emnpty or whitespace");
            }




            if (addReservation.trainno <= 0)
            {
                ModelState.AddModelError(nameof(addReservation.trainno), $"{nameof(addReservation.trainno)} cannot be less than or 0");
            }

            if (addReservation.CustomerId <= 0)
            {
                ModelState.AddModelError(nameof(addReservation.CustomerId), $"{nameof(addReservation.CustomerId)} cannot be Less than 0");
            }

            if (addReservation.NoOfPeople <= 0 || addReservation.NoOfPeople >= 5)
            {
                ModelState.AddModelError(nameof(addReservation.NoOfPeople), $"{nameof(addReservation.NoOfPeople)} cannot be Less than 0 and more than 5 people");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return false;

        }

        private bool ValidateUpdateReservationAsync(Models.Reservation updateReservation)
        {
            if (updateReservation == null)
            {
                ModelState.AddModelError(nameof(updateReservation), $"Data Required. Fields cannot be Empty");

                return false;
            }


            if (string.IsNullOrWhiteSpace(updateReservation.SourceStation))
            {
                ModelState.AddModelError(nameof(updateReservation.SourceStation), $"{nameof(updateReservation.SourceStation)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(updateReservation.DestinationStation))
            {
                ModelState.AddModelError(nameof(updateReservation.DestinationStation), $"{nameof(updateReservation.DestinationStation)} cannot be null emnpty or whitespace");
            }




            if (updateReservation.trainno <= 0)
            {
                ModelState.AddModelError(nameof(updateReservation.trainno), $"{nameof(updateReservation.trainno)} cannot be less than or 0");
            }

            if (updateReservation.CustomerId <= 0)
            {
                ModelState.AddModelError(nameof(updateReservation.CustomerId), $"{nameof(updateReservation.CustomerId)} cannot be Less than 0");
            }

            if (updateReservation.NoOfPeople <= 0 || updateReservation.NoOfPeople >= 5)
            {
                ModelState.AddModelError(nameof(updateReservation.NoOfPeople), $"{nameof(updateReservation.NoOfPeople)} cannot be Less than 0 and more than 5 people");
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
