use [Northwind]
go

select CompanyName
from Suppliers 
where SupplierID in
	(select SupplierID
	from Products pr
	where UnitsInStock = 0)


/* ������ ���� ����������� (������� CompanyName � ������� Suppliers),
� ������� ��� ���� �� ������ �������� �� ������ (UnitsInStock � ������� Products ����� 0). 
������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN. 
*/