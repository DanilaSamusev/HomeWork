SELECT SUM(od.Quantity*(od.UnitPrice-od.Discount)) AS 'Totals'
FROM Northwind.[Order Details] AS od