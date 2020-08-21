SELECT c1.CustomerID AS 'Customer id', c2.CustomerID AS 'Neighbor id', c1.City AS 'City'
FROM Northwind.Customers c1 
    LEFT JOIN Northwind.Customers c2  
        ON c1.CustomerID <> c2.CustomerID AND c1.City = c2.City
ORDER BY c1.CustomerID;