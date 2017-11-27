use [Northwind]
go

select (
	select FirstName + ' ' + LastName
	from Employees e 
	where e.EmployeeID = o.EmployeeID) as 'Seller',
	count(OrderID) as 'Amount'
from dbo.Orders o
group by EmployeeID
order by count(OrderID) desc

/*�� ������� Orders ����� ���������� �������, c�������� ������ ���������.
����� ��� ���������� �������� � ��� ����� ������ � ������� Orders,
��� � ������� EmployeeID ������ �������� ��� ������� ��������.
� ����������� ������� ���� ���������� ������� � ������ ��������
(������ ������������� ��� ���������� ������������� LastName & FirstName.
��� ������ LastName & FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������.
����� �������� ������ ������ ������������ ����������� �� EmployeeID.)
� ��������� ������� �Seller� � ������� c ����������� ������� ���������� � ��������� 'Amount'.
���������� ������� ������ ���� ����������� �� �������� ���������� �������. 
*/