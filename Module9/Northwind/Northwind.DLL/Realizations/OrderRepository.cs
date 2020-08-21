using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Northwind.Shared;

namespace Northwind.DLL.Realizations
{
    public class OrderRepository
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _providerFactory;

        private const string GetAllQuery = "SELECT OrderId, OrderDate, RequiredDate, ShipName, ShipCity, ShippedDate FROM [Northwind].[dbo].[Orders]";
        private const string GetByIdQuery =
            "SELECT OrderId, OrderDate, RequiredDate, ShipName, ShipCity, ShippedDate FROM [Northwind].[dbo].[Orders] WHERE OrderId = @Id";

        private const string DeleteQuery = "DELETE FROM [Northwind].[dbo].[Orders] WHERE OrderId = @Id";

        public OrderRepository(string connectionString, string provider)
        {

            _connectionString = connectionString;
            _providerFactory = DbProviderFactories.GetFactory(provider);
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = new List<Order>();

            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = GetAllQuery;
                command.CommandType = CommandType.Text;

                using var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = reader.GetInt32(0),
                        OrderDate = reader.GetDateTime(1),
                        RequiredDate = reader.GetDateTime(2),
                        ShipName = reader.GetString(3),
                        ShipCity = reader.GetString(4),
                        Status = string.IsNullOrEmpty(reader.GetValue(5).ToString()) ? OrderStatus.NotShipped : OrderStatus.Shipped,
                    };

                    orders.Add(order);
                }
            }

            return orders;
        }

        public Order GetById(int id)
        {
            Order order;

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

                order = new Order
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
            }

            return order;
        }

        public void Delete(int id)
        {
            using (var connection = _providerFactory.CreateConnection())
            {
                var order = GetById(id);

                if (order.Status == OrderStatus.NotShipped)
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using var command = connection.CreateCommand();
                    command.CommandText = DeleteQuery;
                    command.CommandType = CommandType.Text;

                    var idParameter = command.CreateParameter();
                    idParameter.ParameterName = "Id";
                    idParameter.Value = id;
                    command.Parameters.Add(idParameter);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
