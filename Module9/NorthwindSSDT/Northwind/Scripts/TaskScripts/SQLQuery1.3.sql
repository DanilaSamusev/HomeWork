SELECT o.OrderID AS 'Order Number',
	CASE 
		WHEN o.ShippedDate IS NULL THEN 'Not Shipped'
		ELSE CAST(o.ShippedDate AS CHAR(20))
	END AS 'Shipped Date'
FROM Northwind.Orders AS o
WHERE o.ShippedDate IS NULL OR o.ShippedDate > 06/05/1998