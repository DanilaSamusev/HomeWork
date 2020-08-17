SELECT o.EmployeeID AS 'Seller', COUNT(o.OrderID) AS 'Amount', 
	(SELECT e.FirstName + ' ' + e.LastName
	 FROM Northwind.Employees AS e
	 WHERE e.EmployeeID = o.EmployeeID)
FROM Northwind.Orders AS o
GROUP BY o.EmployeeID
ORDER BY 'Amount' DESC