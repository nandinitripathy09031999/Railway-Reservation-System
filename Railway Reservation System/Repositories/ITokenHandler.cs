using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateCustomerTokenAsync(Customer customer);

        Task<string> CreateAdminTokenAsync(Admin admin);
        //Task<IEnumerable<string>> CreateAdminTokenAsync(Admin admin);


    }
}
