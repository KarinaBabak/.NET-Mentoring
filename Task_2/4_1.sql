use [Northwind]
go

select CompanyName
from Suppliers 
where SupplierID in
	(select SupplierID
	from Products pr
	where UnitsInStock = 0)


/* ¬ыдать всех поставщиков (колонка CompanyName в таблице Suppliers),
у которых нет хот€ бы одного продукта на складе (UnitsInStock в таблице Products равно 0). 
»спользовать вложенный SELECT дл€ этого запроса с использованием оператора IN. 
*/