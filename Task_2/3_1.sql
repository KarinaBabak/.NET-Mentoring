use [Northwind]
go

select distinct
	e.LastName,
	r.RegionDescription
from Employees e
inner join EmployeeTerritories et
	on et.EmployeeID = e.EmployeeID
inner join Territories t
	on t.TerritoryID = et.TerritoryID
inner join Region r
	on r.RegionID = t.RegionID 
where r.RegionDescription = 'Western';


/* Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
*/