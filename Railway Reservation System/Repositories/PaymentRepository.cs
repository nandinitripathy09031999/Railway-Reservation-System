using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Data;
using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly RailwayRSDbContext rRSDbContext;

        public PaymentRepository(RailwayRSDbContext rRSDbContext)
        {
            this.rRSDbContext = rRSDbContext;
        }

        #region CRUD Operations
        public async Task<Payment> AddAsync(Payment payment)
        {
            try
            {
                await rRSDbContext.AddAsync(payment);
                await rRSDbContext.SaveChangesAsync();
                return payment;
            }
            catch(Exception ex)
            {
                throw new Exception("All fields are Required");
            }
        }


        public async Task<Payment> DeleteAsync(int id)
        {
            var payment = await rRSDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (payment == null)
            {
                throw new Exception("Record not found, could not delete");
            }

            rRSDbContext.Payments.Remove(payment);
            await rRSDbContext.SaveChangesAsync();

            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await rRSDbContext.Payments.ToListAsync();
        }

        public async Task<Payment> GetAsync(int id)
        {
            var record= await rRSDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if(record == null)
            {
                throw new Exception("Record not Found");
            }
            else
            {
                return record;
            }
        }

        public async Task<Payment> UpdateAsync(int id, Payment payment)
        {
            var existingpayment = await rRSDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingpayment == null)
            {
                throw new Exception("Record not Found, could not update");
            }
            existingpayment.Amount = payment.Amount;
            existingpayment.CustomerId = payment.CustomerId;
            existingpayment.ReservationId = payment.ReservationId;
            


            await rRSDbContext.SaveChangesAsync();

            return existingpayment;
        }
        #endregion

    }
}
