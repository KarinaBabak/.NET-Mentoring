use [Northwind]
go

/*
select 
	Count(OrderID) as 'Total'
	OrderDate
from Orders 
group by OrderDate
*/

/*ѕо таблице Orders найти количество заказов с группировкой по годам.
¬ результатах запроса надо возвращать две колонки c названи€ми Year и Total.
Ќаписать проверочный запрос, который вычисл€ет количество всех заказов.
*/