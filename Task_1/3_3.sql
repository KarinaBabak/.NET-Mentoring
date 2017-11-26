use [Northwind]
go

select CustomerID, Country
from dbo.Customers
where Country like '[B-G]%'



/*Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g,
не используя оператор BETWEEN. 
*/