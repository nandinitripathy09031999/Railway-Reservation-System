using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<Payment> GetAsync(int id);

        Task<Payment> AddAsync(Payment payment);

        Task<Payment> DeleteAsync(int id);

        Task<Payment> UpdateAsync(int id, Payment payment);




        //Testing
       // Task<Payment> GetAsync3(int id, int amt, int ci);

    }
}
