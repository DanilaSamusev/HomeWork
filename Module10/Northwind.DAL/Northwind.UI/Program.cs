using Northwind.DAL;

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
        }
    }
}
