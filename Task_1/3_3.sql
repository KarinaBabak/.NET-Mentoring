use [Northwind]
go

select CustomerID, Country
from dbo.Customers
where Country like '[B-G]%'



/*������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g,
�� ��������� �������� BETWEEN. 
*/