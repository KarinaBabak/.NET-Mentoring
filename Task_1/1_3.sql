use [Northwind]
go

/*select	OrderID		as 'Order Number',
	case 
		when cast(ShippedDate as date) > '1998-05-06' 
			or ShippedDate is NULL then 'Not Shipped'
		else cast(ShippedDate as varchar(12))
	end				as 'Shipped Date'
from dbo.Orders
*/

select	OrderID		as 'Order Number',
	ISNULL(cast(ShippedDate as varchar(12)), 'Not shipped') as 'Shipped Date'
from dbo.Orders
where cast(ShippedDate as date) > '1998-05-06'or ShippedDate is null


/*������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate) �� ������� ��� ���� ��� ������� ��� �� ����������.
� ������� ������ ������������ ������ ������� OrderID (������������� � Order Number) � ShippedDate (������������� � Shipped Date).
� ����������� ������� ���������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�,
��� ��������� �������� ���������� ���� � ������� �� ���������.
*/