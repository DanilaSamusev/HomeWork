SELECT c.CompanyName, c.Country
FROM Northwind.Customers AS c
WHERE c.Country NOT IN ('USA', 'Canada')
ORDER BY c.CompanyName
