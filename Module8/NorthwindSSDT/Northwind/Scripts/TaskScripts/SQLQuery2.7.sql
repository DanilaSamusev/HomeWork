SELECT c.CustomerId, c.City, e.EmployeeID
FROM Northwind.Customers c
CROSS APPLY (SELECT e.EmployeeID
             FROM Northwind.Employees e 
             WHERE e.City = c.City) e;