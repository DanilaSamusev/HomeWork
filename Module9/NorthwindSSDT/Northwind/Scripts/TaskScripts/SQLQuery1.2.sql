SELECT o.OrderID,
	CASE 
		WHEN o.ShippedDate IS NULL THEN 'Not Shipped'
	END AS ShippedDate
FROM Northwind.Orders AS o
WHERE o.ShippedDate IS NULL;