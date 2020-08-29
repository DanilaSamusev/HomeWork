using Northwind.DAL;
using Northwind.Shared;

namespace Northwind.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\ProjectsV13;Integrated Security=True";

            Repository productRepository = new Repository(connectionString);

            //var products = productRepository.GetAllProducts();
            var employees = productRepository.GetEmployeeForSupplierStatistic();

            EmployeeWithTerritories a = new EmployeeWithTerritories()
            {
                FirstName = "Me",
                LastName = "He",
            };

            productRepository.CreateEmployeeWithTerritories(a);

        }
    }
}
