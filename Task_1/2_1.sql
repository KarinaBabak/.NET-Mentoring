use [Northwind]
go

select CompanyName, Country
from dbo.Customers
where Country in ('USA', 'Canada')
order by CompanyName asc, Country asc

/*������� �� ������� Customers ���� ����������, ����������� � USA � Canada. 
������ ������� � ������ ������� ��������� IN. 
���������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ���������� � �� ����� ����������.
*/