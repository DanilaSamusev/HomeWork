SELECT o.OrderDate AS 'Year', COUNT(o.OrderID) AS 'Total'
FROM Northwind.Orders AS o
GROUP BY o.OrderDate

