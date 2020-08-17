SELECT o.OrderID, o.ShippedDate, o.ShipVia 
FROM Northwind.Orders as o
WHERE o.ShippedDate > 06/05/1998 
  AND o.ShipVia >= 2;