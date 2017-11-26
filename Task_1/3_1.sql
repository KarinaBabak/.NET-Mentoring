use [Northwind]
go

select distinct OrderID
from dbo.[Order Details]
where Quantity between 3 and 10




/*������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������),
��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details.
������������ �������� BETWEEN.
������ ������ ���������� ������ ������� OrderID
*/