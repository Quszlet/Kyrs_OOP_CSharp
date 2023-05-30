namespace Kyrs_OOP_CSharp
{
    public interface ICustomerRepository
    {
        void GetAllCustomers(DataGridView dataGridView);
        void SaveCustomer(Customer customer);
        void DeleteCustomer(int id);
        void FiltrationCustomers();
        void FindCustomers();
    }
}
