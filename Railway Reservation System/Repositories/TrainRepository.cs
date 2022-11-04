using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Data;
using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public class TrainRepository:ITrainRespository
    {
        private readonly RailwayRSDbContext rRSDbContext;

        public TrainRepository(RailwayRSDbContext rRSDbContext)
        {
            this.rRSDbContext = rRSDbContext;
        }

        #region CRUD Operations
        public async Task<Train> AddAsync(Train train)
        {
            try
            {
                await rRSDbContext.AddAsync(train);
                await rRSDbContext.SaveChangesAsync();
                return train;
            }
            catch(Exception ex)
            {
                throw new Exception("All the fields are required");
            }

        }

        public async Task<Train> DeleteAsync(int id)
        {
            var train = await rRSDbContext.Trains.FirstOrDefaultAsync(x => x.Id == id);

            if (train == null)
            {
                throw new Exception("Record not Found, could not delete");
            }

            rRSDbContext.Trains.Remove(train);
            await rRSDbContext.SaveChangesAsync();

            return train;
        }

        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            return await rRSDbContext.Trains.ToListAsync();
        }

        public async Task<Train> GetAsync(int id)
        {
            var record= await rRSDbContext.Trains.FirstOrDefaultAsync(x => x.Id == id);
            if(record == null)
            {
                throw new Exception("Record not Found");
            }
            return record;
        }

        public async Task<IEnumerable<Train>> SearchTrains(string sourcestation, string destinationstation, DateTime date)
        {
            var data= rRSDbContext.Trains.Where(x => x.DestinationStation == destinationstation && x.SourceStation==sourcestation && x.DepartureDatetime==date).ToList();
            if(data==null)
            {
                throw new Exception("No Trains available");
            }
            return data;
            
            
        }

        public async Task<Train> UpdateAsync(int id, Train train)
        {
            var existingtrain = await rRSDbContext.Trains.FirstOrDefaultAsync(x => x.Id == id);

            if (existingtrain == null)
            {
                throw new Exception("Record not Found, could not Update");
            }
            existingtrain.Name = train.Name;
            existingtrain.SourceStation = train.SourceStation;
            existingtrain.DestinationStation = train.DestinationStation;
            existingtrain.ArrivalDatetime = train.ArrivalDatetime;
            existingtrain.DepartureDatetime = train.DepartureDatetime;
            existingtrain.TotalSeats = train.TotalSeats;
            existingtrain.Class=train.Class;
            existingtrain.Fare = train.Fare;



            await rRSDbContext.SaveChangesAsync();

            return existingtrain;
        }
        #endregion
    }
}
