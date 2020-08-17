SELECT c.CompanyName, COUNT(o.OrderID) AS 'Total'
FROM Northwind.Customers c
LEFT JOIN Northwind.Orders o
	ON c.CustomerID = o.CustomerID
GROUP BY c.CompanyName
ORDER BY COUNT(o.OrderID)