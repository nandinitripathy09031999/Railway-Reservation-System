using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Data;
using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RailwayRSDbContext rRSDbContext;

        public ReservationRepository(RailwayRSDbContext rRSDbContext)
        {
            this.rRSDbContext = rRSDbContext;
        }

        #region CRUD Operations
        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            try
            {
                await rRSDbContext.AddAsync(reservation);
                await rRSDbContext.SaveChangesAsync();
                return reservation;
            }
            catch(Exception ex)
            {
                throw new Exception("All the fields are required");
            }
            
        }

        public async Task<Reservation> CancelReservationAsync(int id)
        {
            var reservation = await rRSDbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            if (reservation == null)
            {
                throw new Exception("Record not Found, could not Cancel Reservation ");
            }

            rRSDbContext.Reservations.Remove(reservation);
            await rRSDbContext.SaveChangesAsync();

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await rRSDbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetAsync(int Id)
        {
            var record=await rRSDbContext.Reservations.FirstOrDefaultAsync(x => x.Id == Id);
            if(record == null)
            {
                throw new Exception("Record not Found");
            }
            return record;
        }

        public async Task<Reservation> UpdateAsync(int Id, Reservation reservation)
        {
            var existingreservation = await rRSDbContext.Reservations.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingreservation == null)
            {
                throw new Exception("Record not Found, could not update Reservation details");
            }
            existingreservation.trainno = reservation.trainno;
            existingreservation.CustomerId = reservation.CustomerId;
            existingreservation.NoOfPeople = reservation.NoOfPeople;
            existingreservation.SourceStation = reservation.SourceStation;
            existingreservation.DestinationStation = reservation.DestinationStation;
            existingreservation.DatetimeOfCreation = reservation.DatetimeOfCreation;
            existingreservation.Status = reservation.Status;



            await rRSDbContext.SaveChangesAsync();

            return existingreservation;
        }
        #endregion
    }
}
