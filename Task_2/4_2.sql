use [Northwind]
go

select *
from Employees e
where 
	(select count(EmployeeID)
	from Orders o
	where o.EmployeeID = e.EmployeeID) > 150


/* Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.
*/