use [Northwind]
go

select employee.EmployeeID as 'EmployeeID', 
	(select LastName
	from Employees supervisior
	where supervisior.EmployeeID = employee.EmployeeID) as 'Supervisior'
from Employees employee


/* �� ������� Employees ����� ��� ������� �������� ��� ������������.
*/