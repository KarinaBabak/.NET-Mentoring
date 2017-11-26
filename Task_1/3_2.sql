use [Northwind]
go

select CustomerID, Country
from dbo.Customers
where substring(Country,1,1) between 'b' and 'g'
order by Country



/*¬ыбрать всех заказчиков из таблицы Customers, у которых название страны начинаетс€ на буквы из диапазона b и g.
»спользовать оператор BETWEEN.
ѕроверить, что в результаты запроса попадает Germany.
«апрос должен возвращать только колонки CustomerID и Country и отсортирован по Country.
*/