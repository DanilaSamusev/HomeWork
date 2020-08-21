using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Northwind.Shared;

namespace Northwind.DLL
{
    public class OrderDetailsRepository
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _providerFactory;

        private const string GetByIdQuery =
            "SELECT OrderId, OrderDate, RequiredDate, ShipName, ShipCity, ShippedDate FROM [Northwind].[dbo].[Orders] WHERE OrderId = @Id";

        private const string GetProductsByOrderIdQuery =
            "SELECT od.UnitPrice, Quantity, ProductName " +
            "FROM [Northwind].[Northwind].[Order Details] od " +
            "LEFT JOIN [Northwind].[dbo].[Products] p ON od.ProductID = p.ProductID WHERE OrderId = @Id";

        public OrderDetailsRepository(string connectionString, string provider)
        {

            _connectionString = connectionString;
            _providerFactory = DbProviderFactories.GetFactory(provider);
        }

        public OrderDetails GetById(int id)
        {
            OrderDetails orderDetails;

            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = GetByIdQuery;
                command.CommandType = CommandType.Text;

                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "Id";
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                orderDetails = new OrderDetails
                {
                    Id = reader.GetInt32(0),
                    OrderDate = reader.GetDateTime(1),
                    RequiredDate = reader.GetDateTime(2),
                    ShipName = reader.GetString(3),
                    ShipCity = reader.GetString(4),
                    Status = string.IsNullOrEmpty(reader.GetValue(5).ToString())
                        ? OrderStatus.NotShipped
                        : OrderStatus.Shipped,
                };

                var products = GetProductsByOrderId(id).ToList();
                orderDetails.products = products;
            }

            return orderDetails;
        }

        private IEnumerable<Product> GetProductsByOrderId(int id)
        {
            var products = new List<Product>();

            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = GetProductsByOrderIdQuery;
                command.CommandType = CommandType.Text;

                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "Id";
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    var product = new Product
                    {
                        UnitPrice = reader.GetDecimal(0),
                        Quantity = reader.GetInt16(1),
                        Name = reader.GetString(2),
                    };

                    products.Add(product);
                };
            }

            return products;
        }
    }
}
