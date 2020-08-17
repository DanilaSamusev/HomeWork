SELECT c.CompanyName, c.Country
FROM Northwind.Customers AS c
WHERE c.Country IN ('USA', 'Canada')
ORDER BY c.CompanyName
