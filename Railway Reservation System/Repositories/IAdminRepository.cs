using Railway_Reservation_System.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Railway_Reservation_System.Repositories
{
    public interface IAdminRepository
    {
        //Task<<IEnumerable>Admin> AuthenticateAdminAsync(string Name, string Password);
        
        Task<Admin> AuthenticateAdminAsync(string Name,string Password);
    }
}
