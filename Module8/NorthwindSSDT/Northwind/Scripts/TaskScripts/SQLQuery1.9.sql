SELECT c.Country
FROM Northwind.Customers AS c
WHERE c.Country LIKE '[b-h]%'
ORDER BY c.Country