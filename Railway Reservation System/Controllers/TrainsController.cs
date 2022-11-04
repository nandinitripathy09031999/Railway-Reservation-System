using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Railway_Reservation_System.Repositories;

namespace Railway_Reservation_System.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TrainsController : Controller
    {
        private readonly ITrainRespository trainRepository;

        public TrainsController(ITrainRespository trainRepository)
        {
            this.trainRepository = trainRepository;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Passenger")]
        public async Task<IActionResult> GetAllTrainsAsync()
        {
            var trains = await trainRepository.GetAllAsync();

            return Ok(trains);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetTrainAsync")]
        [Authorize(Roles = "Admin,Passenger")]
        public async Task<IActionResult> GetTrainAsync(int id)
        {
            var train = await trainRepository.GetAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            return Ok(train);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTrainAsync(Models.Train addTrain)
        {
            //if(!ValidateAddTrainAsync(addTrain))
            //{
            //    return BadRequest(ModelState);
            //}
            var train = new Models.Train()
            {
                Name = addTrain.Name,
                SourceStation = addTrain.SourceStation,
                DestinationStation = addTrain.DestinationStation,
                ArrivalDatetime = addTrain.ArrivalDatetime,
                DepartureDatetime = addTrain.DepartureDatetime,
                TotalSeats = addTrain.TotalSeats,
                AvailableSeats = addTrain.AvailableSeats,
                Class = addTrain.Class,
                Fare = addTrain.Fare,

            };
            train = await trainRepository.AddAsync(train);
            return CreatedAtAction(nameof(GetTrainAsync), new { id = train.Id }, train);
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTrainAsync(int id)
        {
            var train = await trainRepository.DeleteAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            return Ok(train);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTrainAsync([FromRoute] int id, [FromBody] Models.Train updateTrain)
        {
            var train = new Models.Train()
            {
                Name = updateTrain.Name,
                SourceStation = updateTrain.SourceStation,
                DestinationStation = updateTrain.DestinationStation,
                ArrivalDatetime = updateTrain.ArrivalDatetime,
                DepartureDatetime = updateTrain.DepartureDatetime,
                TotalSeats = updateTrain.TotalSeats,
                AvailableSeats = updateTrain.AvailableSeats,
                Class = updateTrain.Class,
                Fare = updateTrain.Fare,
            };

            train = await trainRepository.UpdateAsync(id, train);

            if (train == null)
            {
                return NotFound();
            }

            return Ok(train);
        }


        //  [HttpGet("{param1}/{param2}/{Date:dateTime}")]
        [HttpGet]
        [Route("{sourceStation}/{DestinationStation}/{date:DateTime}")]
        [Authorize(Roles = "Admin,Passenger")]

        public  async Task<IActionResult> SearchTrainAsync( string sourceStation, string DestinationStation,DateTime date)
        {
           var train=await trainRepository.SearchTrains(sourceStation,DestinationStation,date);
            if (train == null)
            {
                return NotFound();
            }

            return Ok(train);
        }



        #region Private Methods

        private bool ValidateAddTrainAsync(Models.Train addTrain)
        {
            if (addTrain == null)
            {
                ModelState.AddModelError(nameof(addTrain), $"Data Required. Fields cannot be Empty");

                return false;
            }

            if (string.IsNullOrWhiteSpace(addTrain.Name))
            {
                ModelState.AddModelError(nameof(addTrain.Name), $"{nameof(addTrain.Name)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addTrain.SourceStation))
            {
                ModelState.AddModelError(nameof(addTrain.SourceStation), $"{nameof(addTrain.SourceStation)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addTrain.DestinationStation))
            {
                ModelState.AddModelError(nameof(addTrain.DestinationStation), $"{nameof(addTrain.DestinationStation)} cannot be null emnpty or whitespace");
            }

           


            if (addTrain.TotalSeats <= 0)
            {
                ModelState.AddModelError(nameof(addTrain.TotalSeats), $"{nameof(addTrain.TotalSeats)} cannot be less than or 0");
            }

            if (addTrain.AvailableSeats > addTrain.TotalSeats || addTrain.AvailableSeats < 0)
            {
                ModelState.AddModelError(nameof(addTrain.AvailableSeats), $"{nameof(addTrain.AvailableSeats)} cannot be more than Total Seats or Less than 0");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return false;

        }

        private bool ValidateUpdateTrainAsync(Models.Train updateTrain)
        {
            if (updateTrain == null)
            {
                ModelState.AddModelError(nameof(updateTrain), $"Data Required. Fields cannot be Empty");

                return false;
            }


            if (string.IsNullOrWhiteSpace(updateTrain.Name))
            {
                ModelState.AddModelError(nameof(updateTrain.Name), $"{nameof(updateTrain.Name)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(updateTrain.SourceStation))
            {
                ModelState.AddModelError(nameof(updateTrain.SourceStation), $"{nameof(updateTrain.SourceStation)} cannot be null emnpty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(updateTrain.DestinationStation))
            {
                ModelState.AddModelError(nameof(updateTrain.DestinationStation), $"{nameof(updateTrain.DestinationStation)} cannot be null emnpty or whitespace");
            }




            if (updateTrain.TotalSeats <= 0)
            {
                ModelState.AddModelError(nameof(updateTrain.TotalSeats), $"{nameof(updateTrain.TotalSeats)} cannot be less than or 0");
            }

            if (updateTrain.AvailableSeats > updateTrain.TotalSeats || updateTrain.AvailableSeats < 0)
            {
                ModelState.AddModelError(nameof(updateTrain.AvailableSeats), $"{nameof(updateTrain.AvailableSeats)} cannot be more than Total Seats or Less than 0");
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
