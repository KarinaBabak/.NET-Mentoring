use [Northwind]
go

select Count(distinct CustomerID) as 'Customers Count'
from Orders 


/*По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/