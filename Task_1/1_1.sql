use [Northwind]
go

select OrderID, ShippedDate, ShipVia
from dbo.Orders
where  cast(ShippedDate as date) >= '1998-05-06' and ShipVia >= 2
go

/*Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно
и которые доставлены с ShipVia >= 2. Запрос должен возвращать только колонки OrderID, ShippedDate и ShipVia.
*/