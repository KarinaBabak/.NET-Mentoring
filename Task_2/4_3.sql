use [Northwind]
go

select *
from Customers c
where not exists
	(select OrderID
	from Orders o
	where o.CustomerID = c.CustomerID)


/* Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
 Использовать оператор EXISTS.
*/