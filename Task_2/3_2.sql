use [Northwind]
go

select c.ContactName,
	count(o.OrderID) as 'Orders Count'
from Customers c
left join Orders o
	on o.CustomerID = c.CustomerID
group by c.ContactName
order by 'Orders Count'


/* Выдать в результатах запроса имена всех заказчиков из таблицы Customers 
и суммарное количество их заказов из таблицы Orders. 
Принять во внимание, что у некоторых заказчиков нет заказов, 
но они также должны быть выведены в результатах запроса.
Упорядочить результаты запроса по возрастанию количества заказов.
*/