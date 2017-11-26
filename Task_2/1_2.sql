use [Northwind]
go

select Count(case 
		when ShippedDate is null then 1 else null 
	end) as 'Count'
from Orders 


/*�� ������� Orders ����� ���������� �������, ������� ��� �� ���� ����������
(�.�. � ������� ShippedDate ��� �������� ���� ��������).
������������ ��� ���� ������� ������ �������� COUNT. �� ������������ ����������� WHERE � GROUP.
*/