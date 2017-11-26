use [Northwind]
go

/*select	OrderID		as 'Order Number',
	case 
		when cast(ShippedDate as date) > '1998-05-06' 
			or ShippedDate is NULL then 'Not Shipped'
		else cast(ShippedDate as varchar(12))
	end				as 'Shipped Date'
from dbo.Orders
*/

select	OrderID		as 'Order Number',
	ISNULL(cast(ShippedDate as varchar(12)), 'Not shipped') as 'Shipped Date'
from dbo.Orders
where cast(ShippedDate as date) > '1998-05-06'or ShippedDate is null


/*Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату или которые еще не доставлены.
В запросе должны возвращаться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать в Shipped Date).
В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’,
для остальных значений возвращать дату в формате по умолчанию.
*/