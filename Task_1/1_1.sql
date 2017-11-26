use [Northwind]
go

select OrderID, ShippedDate, ShipVia
from dbo.Orders
where  cast(ShippedDate as date) >= '1998-05-06' and ShipVia >= 2
go

/*������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (������� ShippedDate) ������������
� ������� ���������� � ShipVia >= 2. ������ ������ ���������� ������ ������� OrderID, ShippedDate � ShipVia.
*/