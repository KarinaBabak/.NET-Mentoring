use [Northwind]
go

select *
from Customers c
where not exists
	(select OrderID
	from Orders o
	where o.CustomerID = c.CustomerID)


/* ������ ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders).
 ������������ �������� EXISTS.
*/