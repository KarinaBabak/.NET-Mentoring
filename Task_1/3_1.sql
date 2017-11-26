use [Northwind]
go

select distinct OrderID
from dbo.[Order Details]
where Quantity between 3 and 10




/*Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться),
где встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details.
Использовать оператор BETWEEN.
Запрос должен возвращать только колонку OrderID
*/