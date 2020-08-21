SELECT s.CompanyName
FROM Northwind.Suppliers s
WHERE s.SupplierID IN (SELECT p.SupplierID
					   FROM Northwind.Products p
					   WHERE p.UnitsInStock = 0)				   