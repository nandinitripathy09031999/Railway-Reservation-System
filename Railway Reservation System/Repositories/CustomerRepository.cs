using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Data;
using Railway_Reservation_System.Models;
using Railway_Reservation_System.Repositories;

namespace railway_reservation_system.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RailwayRSDbContext rRSDbContext;

        public CustomerRepository(RailwayRSDbContext rRSDbContext)
        {
            this.rRSDbContext = rRSDbContext;
        }


        #region CRUD operations
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await rRSDbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetAsync(int id)
        {
            var record=await rRSDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if(record==null)
            {
                throw new Exception("Record not Found");
            }
            else
            {
                return record;
            }
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {

                await rRSDbContext.AddAsync(customer);
                await rRSDbContext.SaveChangesAsync();
                return customer;
            }
            catch(Exception ex)
            {
                throw new Exception("All the fields are required");

            }

            
        }

        public async Task<Customer> DeleteAsync(int id)
        {
            var customer = await rRSDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            if (customer == null)
            {
                throw new Exception("Record not Found, could not delete");
            }

            else
            {

                rRSDbContext.Customers.Remove(customer);
                await rRSDbContext.SaveChangesAsync();

                return customer;
            }
        }

        public async Task<Customer> UpdateAsync(int id, Customer customer)
        {
            var existingcustomer = await rRSDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            if (existingcustomer == null)
            {
                throw new Exception("Record not found, could not update");
            }
            existingcustomer.Name = customer.Name;
            existingcustomer.Age=customer.Age;
            existingcustomer.Gender=customer.Gender;
            existingcustomer.Email = customer.Email;
            existingcustomer.Password = customer.Password;



            await rRSDbContext.SaveChangesAsync();

            return existingcustomer;
        }

        #endregion

        #region User Authentication
        public async Task<Customer> AuthenticateCustomerAsync(string Name, string Password)
        {
            var user = await rRSDbContext.Customers
                .FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower() && x.Password == Password);

            if (user == null)
            {
                //throw new Exception("User not Registerd");
                return null;
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
