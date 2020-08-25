using Dapper;

using Northwind.Shared;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Northwind.DAL
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                products = db.Query<Product>(
                        "SELECT ProductId, ProductName, UnitPrice, CompanyName, CategoryName " +
                    "FROM [Northwind].[dbo].[Products] p " +
                    "LEFT JOIN [Northwind].[dbo].[Suppliers] s ON s.SupplierId = p.SupplierId " +
                    "LEFT JOIN [Northwind].[dbo].[Categories] c ON c.CategoryId = p.CategoryId")
                .ToList();
            }

            return products;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                employees = db.Query<Employee>(
                    "SELECT DISTINCT LastName, FirstName, RegionDescription " +
                    "FROM [Northwind].[dbo].[Employees] e " +
                    "LEFT JOIN [Northwind].[dbo].[EmployeeTerritories] et ON et.EmployeeId = e.EmployeeId " +
                    "LEFT JOIN [Northwind].[dbo].[Territories] t ON t.TerritoryId = et.TerritoryId " +
                        "LEFT JOIN[Northwind].[dbo].[Regions] r ON r.RegionId = t.RegionId")
                .ToList();
            }

            return employees;
        }

        public IEnumerable<EmployeePerRegionStatistic> GetAllEmployeeStatistic()
        {
            List<EmployeePerRegionStatistic> employeeStatistic = new List<EmployeePerRegionStatistic>();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                employeeStatistic = db.Query<EmployeePerRegionStatistic>(
                        "SELECT Count(e.EmployeeId) as EmployeeCount, RegionDescription " +
                        "FROM [Northwind].[dbo].[Employees] e " +
                        "LEFT JOIN [Northwind].[dbo].[EmployeeTerritories] et ON et.EmployeeId = e.EmployeeId " +
                        "LEFT JOIN [Northwind].[dbo].[Territories] t ON t.TerritoryId = et.TerritoryId " +
                        "LEFT JOIN[Northwind].[dbo].[Regions] r ON r.RegionId = t.RegionId " +
                        "GROUP BY RegionDescription")
                    .ToList();
            }

            return employeeStatistic;
        }

        public List<EmployeeForSupplierStatistic> GetEmployeeForSupplierStatistic()
        {
            var employeeStatistic = new List<EmployeeForSupplierStatistic>();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                employeeStatistic = db.Query<EmployeeForSupplierStatistic>(
                        "SELECT DISTINCT EmployeeId, CompanyName " +
                        "FROM[Northwind].[Northwind].[Orders] e " +
                        "LEFT JOIN[Northwind].[Northwind].[Order Details] od ON e.OrderID = od.OrderID " +
                        "LEFT JOIN[Northwind].[dbo].[Products] p ON p.ProductID = od.ProductID " +
                        "LEFT JOIN[Northwind].[Northwind].[Suppliers] s ON s.SupplierID = p.SupplierID")
                    .ToList();
            }

            return employeeStatistic;
        }
    }
}
