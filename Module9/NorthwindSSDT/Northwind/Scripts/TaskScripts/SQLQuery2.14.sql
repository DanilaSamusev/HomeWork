SELECT c.CustomerId
FROM Northwind.Customers c
WHERE NOT EXISTS (SELECT o.OrderId
                  FROM Northwind.Orders o
                  WHERE o.CustomerID = c.CustomerID);