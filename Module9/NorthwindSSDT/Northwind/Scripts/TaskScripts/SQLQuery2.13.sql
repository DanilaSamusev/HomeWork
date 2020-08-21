SELECT e.FirstName
FROM Northwind.Employees e
WHERE (SELECT COUNT(o.OrderID)
	   FROM Northwind.Orders o
	   WHERE o.EmployeeID = e.EmployeeID
	   GROUP BY o.EmployeeID) > 150