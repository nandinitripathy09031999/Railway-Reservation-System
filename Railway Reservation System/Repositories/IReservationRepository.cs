using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public interface IReservationRepository
    {

        Task<IEnumerable<Reservation>> GetAllAsync();

        Task<Reservation> GetAsync(int Id);

        Task<Reservation> AddAsync(Reservation reservation);

        Task<Reservation> CancelReservationAsync(int Id);

        Task<Reservation> UpdateAsync(int Id, Reservation reservation);
    }
}
