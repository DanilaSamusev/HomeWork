SELECT c.CustomerID, c.Country
FROM Northwind.Customers AS c
WHERE c.Country BETWEEN 'b%' AND 'h%'
ORDER BY c.Country