use [Northwind]
go

select *
from Employees e
where 
	(select count(EmployeeID)
	from Orders o
	where o.EmployeeID = e.EmployeeID) > 150


/* ������ ���� ���������, ������� ����� ����� 150 �������. ������������ ��������� SELECT.
*/