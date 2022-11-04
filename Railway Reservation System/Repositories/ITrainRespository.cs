using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public interface ITrainRespository
    {
        Task<IEnumerable<Train>> GetAllAsync();

        Task<Train> GetAsync(int id);

        Task<IEnumerable<Train>> SearchTrains(string sourcestation, string destinationstation, DateTime date );

        Task<Train> AddAsync(Train train);

        Task<Train> DeleteAsync(int id);

        Task<Train> UpdateAsync(int id, Train train);
    }
}
