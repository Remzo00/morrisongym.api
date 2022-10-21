using Morrison_Gym.API.Entities;

namespace Morrison_Gym.API.Repository.Contract
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int customerId);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomerAsync(Customer customer);
    }
}
