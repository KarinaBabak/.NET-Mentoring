use [Northwind]
go

/*select 
	SUM(Quantity*(UnitPrice - UnitPrice*Discount)) as 'Totals'
from dbo.[Order Details]
*/

select SUM(Quantity*UnitPrice - Discount) as 'Totals'
from dbo.[Order Details]




/*����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� � ������ �� ���.
 ����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.
*/