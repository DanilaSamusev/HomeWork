﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("")]

        public void Linq1()
        {
            decimal X = 100000;

            var customerNames = dataSource.Customers
                .Where(customer => customer.Orders.Sum(order => order.Total) > X)
                .Select(customer => customer.CompanyName);

            foreach (var name in customerNames)
            {
                ObjectDumper.Write(name);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("")]

        public void Linq2()
        {
            var customers = dataSource.Customers
                .Select(customer => new
                {
                    Customer = customer,
                    Suppliers = dataSource.Suppliers
                        .Where(supplier => supplier.City == customer.City && supplier.Country == customer.Country)
                });

            customers = dataSource.Customers.GroupJoin(dataSource.Suppliers,
                    customer => new {customer.City, customer.Country},
                    supplier => new {supplier.City, supplier.Country},
                    (customer, suppliers) => new {Customer = customer, Suppliers = suppliers});

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.Customer.CompanyName);

                foreach (var supplier in customer.Suppliers)
                {
                    ObjectDumper.Write($"\t{supplier.SupplierName}");
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("")]

        public void Linq3()
        {
            decimal X = 11000;

            var customerNames = dataSource.Customers
                .Where(customer => customer.Orders.Any(order => order.Total > X))
                .Select(customer => customer.CompanyName);

            foreach (var name in customerNames)
            {
                ObjectDumper.Write(name);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq4()
        {
            var customersInfo = dataSource.Customers
                .Where(customer => customer.Orders.Length != 0)
                .Select(customer => $"{customer.CompanyName} become a customer in {customer.Orders.Select(order => order.OrderDate.ToString("Y")).First()}");

            foreach (var p in customersInfo)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5-1")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq5ByYear()
        {
            var customersInfo = dataSource.Customers
                .Where(customer => customer.Orders.Length != 0)
                .OrderBy(customer => customer.Orders.Select(order => order.OrderDate.ToString("yyyy")).First())
                .Select(customer => $"{customer.CompanyName} become a customer in {customer.Orders.Select(order => order.OrderDate.ToString("Y")).First()}");

            foreach (var p in customersInfo)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5-2")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq5ByMont()
        {
            var customersInfo = dataSource.Customers
                .Where(customer => customer.Orders.Length != 0)
                .OrderBy(customer => customer.Orders.Select(order => order.OrderDate.ToString("M")).First())
                .Select(customer => $"{customer.CompanyName} become a customer in {customer.Orders.Select(order => order.OrderDate.ToString("Y")).First()}");

            foreach (var p in customersInfo)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5-3")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq5ByTotal()
        {
            var customersInfo = dataSource.Customers
                .Where(customer => customer.Orders.Length != 0)
                .OrderBy(customer => customer.Orders.Sum(order => order.Total))
                .Reverse()
                .Select(customer => $"{customer.CompanyName} become a customer in {customer.Orders.Select(order => order.OrderDate.ToString("Y")).First()}");

            foreach (var p in customersInfo)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5-4")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq5ByName()
        {
            var customersInfo = dataSource.Customers
                .Where(customer => customer.Orders.Length != 0)
                .OrderBy(customer => customer.CompanyName)
                .Select(customer => $"{customer.CompanyName} become a customer in {customer.Orders.Select(order => order.OrderDate.ToString("Y")).First()}");

            foreach (var p in customersInfo)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq6()
        {
            var customersInfo = dataSource.Customers
                .Where(customer => customer.PostalCode != null
                                   && customer.PostalCode.Any(ch => !Regex.IsMatch(ch.ToString(), "[0-9]"))
                                   || string.IsNullOrWhiteSpace(customer.Region)
                                   || !customer.Phone.Contains("("))
                .Select(customer => $"{customer.CompanyName} {customer.PostalCode}");

            foreach (var p in customersInfo)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 7")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq7()
        {
            var groups = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(grp => new { key = grp.Key, items = grp.GroupBy(d => d.UnitsInStock) });

            foreach (var group in groups)
            {
                ObjectDumper.Write(group.key);

                foreach (var g in group.items)
                {
                    ObjectDumper.Write($"\t{g.Key}");

                    foreach (var product in g)
                    {
                        ObjectDumper.Write($"\t\t{product.ProductName}");
                    }
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 8")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq8()
        {
            var groups = dataSource.Products
                .GroupBy(GetGroupNumber);

            foreach (var group in groups)
            {
                ObjectDumper.Write(group.Key);

                foreach (var product in group)
                {
                    ObjectDumper.Write($"\t{product.ProductName} {product.UnitPrice}");
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 9")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq9()
        {
            var groups = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(grp =>
                    new
                    {
                        key = grp.Key,
                        sum = grp.Select(product => product.Orders.Sum(order => order.Total)).Sum(s => s) / grp.Count()
                    });

            var gr = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(grp =>
                    new
                    {
                        key = grp.Key,
                        sum = grp.Select(product => product.Orders.Length).Sum(s => s) / grp.Count()
                    });

            foreach (var group in groups)
            {
                ObjectDumper.Write(group.key);

                ObjectDumper.Write($"\t{group.sum}");
            }

            foreach (var group in gr)
            {
                ObjectDumper.Write(group.key);

                ObjectDumper.Write($"\t{group.sum}");
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 10_1")]
        [Description("This sample return customer name, whose total order price is more than X")]

        public void Linq10_1()
        {
            var groups = dataSource.Customers
                .SelectMany(customer => customer.Orders)
                .GroupBy(product => product.OrderDate.ToString("yyyy"))
                .Select(group => new { key = group.Key, count = group.Count() / 12 });

            foreach (var group in groups)
            {
                ObjectDumper.Write($"{group.key} - {group.count} orders in month");
            }

            var result = dataSource.Customers
                .SelectMany(customer => customer.Orders)
                .GroupBy(product => product.OrderDate.ToString("yyyy"))
                .Sum(group => group.Count()) / 3;

            ObjectDumper.Write($"{result} orders in year");
        }


        [Category("Restriction Operators")]
        [Title("Where - Task 10_2")]
        [Description("Second version of task 10")]

        public void Linq10_2()
        {
            var customerInfos = dataSource.Customers
                .Select(customer => new
                {
                    CustomerId = customer.CustomerID,
                    MonthsStatistic = customer.Orders.GroupBy(order => order.OrderDate.Month)
                        .Select(group => new { Month = group.Key, OrdersCount = group.Count() }),
                    YearsStatistic = customer.Orders.GroupBy(order => order.OrderDate.Year)
                        .Select(group => new { Year = group.Key, OrdersCount = group.Count() }),
                    YearMonthStatistic = customer.Orders
                        .GroupBy(order => new { order.OrderDate.Year, order.OrderDate.Month })
                        .Select(group => new { group.Key.Year, group.Key.Month, OrdersCount = group.Count() })
                });


            foreach (var info in customerInfos)
            {
                ObjectDumper.Write($"Customer id - {info.CustomerId}");

                foreach (var month in info.MonthsStatistic)
                {
                    ObjectDumper.Write($"\tMonth - {month.Month} orders - {month.OrdersCount}");
                }

                foreach (var year in info.YearsStatistic)
                {
                    ObjectDumper.Write($"\tYear - {year.Year} orders - {year.OrdersCount}");
                }

                foreach (var yearMonth in info.YearMonthStatistic)
                {
                    ObjectDumper.Write($"\tMonth - {yearMonth.Month} year - {yearMonth.Year} orders - {yearMonth.OrdersCount}");
                }
            }
        }

        private int GetGroupNumber(Product product)
        {
            decimal lowPrice = 25M;
            decimal middlePrice = 50M;

            if (product.UnitPrice <= lowPrice)
            {
                return 1;
            }

            if (lowPrice < product.UnitPrice && product.UnitPrice <= middlePrice)
            {
                return 2;
            }

            return 3;
        }
    }
}
