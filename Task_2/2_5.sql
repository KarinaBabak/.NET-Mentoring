use [Northwind]
go

select c.City,
	Count(CustomerID) as 'Amount'
from Customers c
group by City


/*����� ���� �����������, ������� ����� � ����� ������. 
*/