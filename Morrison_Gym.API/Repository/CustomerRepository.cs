using Microsoft.EntityFrameworkCore;
using Morrison_Gym.API.Data;
using Morrison_Gym.API.Entities;
using Morrison_Gym.API.Repository.Contract;

namespace Morrison_Gym.API.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await FindAll().Include(x => x.Id).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return (await FindByCondition(x => x.Id == customerId).Include(u => u.Id).SingleOrDefaultAsync())!;
        }

        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);
        }

        public void DeleteCustomerAsync(Customer customer)
        {
            Create(customer);
        }
    }
}
