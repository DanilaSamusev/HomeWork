SELECT e1.EmployeeID, e2.EmployeeID as 'Manager id'
FROM Northwind.Employees e1
    LEFT JOIN Northwind.Employees e2 
    ON e1.ReportsTo = e2.EmployeeID