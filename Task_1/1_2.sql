use [Northwind]
go

select OrderID,
	case 
		when ShippedDate is NULL then 'Not Shipped'
		else cast(ShippedDate as varchar(12))
	end   
	as ShippedDate
from dbo.Orders


/*�������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. 
� ����������� ������� ���������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped� (������������ ��������� ������� CAS�). 
������ ������ ���������� ������ ������� OrderID � ShippedDate
*/