use [Northwind]
go

/*select 
	SUM(Quantity*(UnitPrice - UnitPrice*Discount)) as 'Totals'
from dbo.[Order Details]
*/

select SUM(Quantity*UnitPrice - Discount) as 'Totals'
from dbo.[Order Details]




/*Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним.
 Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
*/