use [Northwind]
go

select Count(distinct CustomerID) as 'Customers Count'
from Orders 


/*�� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������. 
������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.
*/