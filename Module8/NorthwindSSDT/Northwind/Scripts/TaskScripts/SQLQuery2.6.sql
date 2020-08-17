SELECT o.EmployeeID AS 'Seller', COUNT(o.OrderID) AS 'Amount', o.CustomerID
FROM Northwind.Orders AS o
GROUP BY o.EmployeeID, o.CustomerID