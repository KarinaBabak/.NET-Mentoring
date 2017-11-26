use [Northwind]
go

select OrderID,
	case 
		when ShippedDate is NULL then 'Not Shipped'
		else cast(ShippedDate as varchar(12))
	end   
	as ShippedDate
from dbo.Orders


/*Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ (использовать системную функцию CASЕ). 
Запрос должен возвращать только колонки OrderID и ShippedDate
*/