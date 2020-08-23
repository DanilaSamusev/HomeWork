using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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

        private const string CreateQuery = "INSERT INTO [Northwind].[dbo].[Orders]" +
                             " (OrderDate, RequiredDate, ShipCity, ShipName)" +
                             " VALUES (@OrderDate, @RequiredDate, @ShipCity, @ShipName)";

        private const string UpdateOrderDateQuery = "UPDATE [Northwind].[dbo].[Orders] SET OrderDate = @OrderDate WHERE OrderId = @OrderId";
        private const string UpdateShippedDate = "UPDATE [Northwind].[dbo].[Orders] SET ShippedDate = @ShippedDate WHERE OrderId = @OrderId";

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

        public void Create(Order order)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(CreateQuery, connection);
            SetParameter(command, order.OrderDate, nameof(order.OrderDate));
            SetParameter(command, order.RequiredDate, nameof(order.RequiredDate));
            SetParameter(command, order.ShipCity, nameof(order.ShipCity));
            SetParameter(command, order.ShipName, nameof(order.ShipName));
            command.ExecuteNonQuery();
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

        public void SetOrderAsActive(int id, DateTime orderDate)
        {
            using var connection = _providerFactory.CreateConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = UpdateOrderDateQuery;
            command.CommandType = CommandType.Text;

            SetParameter(command, id, "Id");
            SetParameter(command, orderDate, "OrderDate");
        }

        public void SetOrderAsShipped(int id, DateTime shippedDate)
        {
            using var connection = _providerFactory.CreateConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = UpdateOrderDateQuery;
            command.CommandType = CommandType.Text;

            SetParameter(command, id, "Id");
            SetParameter(command, shippedDate, "ShippedDate");
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

        private void SetParameter(DbCommand command, object value, string name)
        {
            var parameter = new SqlParameter {ParameterName = name, Value = value};
            command.Parameters.Add(parameter);
        }
    }
}
