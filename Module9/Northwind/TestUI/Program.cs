﻿using System.Data.Common;
using System.Data.SqlClient;
using Northwind.DLL.Realizations;


namespace TestUI
{
    class Program
    {
        public static void Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            var provider = DbProviderFactories.GetProviderInvariantNames();
            var orderRepository = new OrderRepository("Data Source=(localdb)\\ProjectsV13;Integrated Security=True", "System.Data.SqlClient");

            //var orders = orderRepository.GetAll().ToArray();
            var orderDetails = orderRepository.GetById(10248);
        }
    }
}
