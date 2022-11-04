using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer> GetAsync(int id);

        Task<Customer> AddAsync(Customer customer);

        Task<Customer> DeleteAsync(int id);

        Task<Customer> UpdateAsync(int id, Customer customer);

         Task<Customer> AuthenticateCustomerAsync(string Name, string Password);
    }
}
