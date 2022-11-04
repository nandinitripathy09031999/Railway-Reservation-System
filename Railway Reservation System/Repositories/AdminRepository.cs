using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Data;
using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly RailwayRSDbContext rRSDbContext;

        public AdminRepository(RailwayRSDbContext rRSDbContext)
        {
            this.rRSDbContext = rRSDbContext;
        }

        #region Admin Authentication
        public async Task<Admin> AuthenticateAdminAsync(string Name, string Password)
        {
            var user = await rRSDbContext.Admins
              .FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower() && x.Password == Password);

            if (user == null)
            {
                throw new Exception("User not Registered");
            }
            else
            {
                user.Password = null;
                return user;
            }

        }
        #endregion
    }
}
