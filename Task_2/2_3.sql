use [Northwind]
go

select  Count(OrderID) as 'COUNT',
	CustomerID,
	EmployeeID
from dbo.Orders
where Year(ShippedDate) = 1998
group by CustomerID, EmployeeID


/*�� ������� Orders ����� ���������� �������, ��������� ������ ��������� � ��� ������� ����������. 
���������� ���������� ��� ������ ��� �������, ��������� � 1998 ����. 
*/