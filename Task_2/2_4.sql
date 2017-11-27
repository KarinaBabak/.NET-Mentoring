use [Northwind]
go

select c.CustomerID, c.City, e.EmployeeID
from Customers c, Employees e
where c.City in (select distinct City
      from Employees)
and c.City = e.City


/*Найти покупателей и продавцов, которые живут в одном городе.
Если в городе живут только один или несколько продавцов, или только один или несколько покупателей,
то информация о таких покупателя и продавцах не должна попадать в результирующий набор.
Не использовать конструкцию JOIN. 
*/