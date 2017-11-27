use [Northwind]
go

select  Count(OrderID) as 'COUNT',
	CustomerID,
	EmployeeID
from dbo.Orders
where Year(ShippedDate) = 1998
group by CustomerID, EmployeeID


/*ѕо таблице Orders найти количество заказов, сделанных каждым продавцом и дл€ каждого покупател€. 
Ќеобходимо определить это только дл€ заказов, сделанных в 1998 году. 
*/