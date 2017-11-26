use [Northwind]
go

select CompanyName, Country
from dbo.Customers
where Country in ('USA', 'Canada')
order by CompanyName asc, Country asc

/*Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
Запрос сделать с только помощью оператора IN. 
Возвращать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания.
*/