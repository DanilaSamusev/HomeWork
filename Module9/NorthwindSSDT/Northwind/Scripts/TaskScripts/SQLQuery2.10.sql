SELECT DISTINCT e.EmployeeID, r.RegionDescription
FROM Northwind.Employees e
INNER JOIN Northwind.EmployeeTerritories et
	ON et.EmployeeID = e.EmployeeID
INNER JOIN Northwind.Territories t
	ON t.TerritoryID = et.TerritoryID
INNER JOIN Northwind.Region r
	ON r.RegionID = t.RegionID
WHERE r.RegionDescription = 'Western'