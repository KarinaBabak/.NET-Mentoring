use [Northwind]
go

select CustomerID, Country
from dbo.Customers
where substring(Country,1,1) between 'b' and 'g'
order by Country



/*������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g.
������������ �������� BETWEEN.
���������, ��� � ���������� ������� �������� Germany.
������ ������ ���������� ������ ������� CustomerID � Country � ������������ �� Country.
*/