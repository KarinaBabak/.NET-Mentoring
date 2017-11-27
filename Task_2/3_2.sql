use [Northwind]
go

select c.ContactName,
	count(o.OrderID) as 'Orders Count'
from Customers c
left join Orders o
	on o.CustomerID = c.CustomerID
group by c.ContactName
order by 'Orders Count'


/* ������ � ����������� ������� ����� ���� ���������� �� ������� Customers 
� ��������� ���������� �� ������� �� ������� Orders. 
������� �� ��������, ��� � ��������� ���������� ��� �������, 
�� ��� ����� ������ ���� �������� � ����������� �������.
����������� ���������� ������� �� ����������� ���������� �������.
*/