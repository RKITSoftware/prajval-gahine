
USE DB1;

-- count(*) - count no of records
SELECT
	COUNT(*) "No of Employees"
FROM
	EMP01;
    
-- count(col name) - here it returns distinct departments
SELECT
	COUNT(DISTINCT P01F04)
FROM
	EMP01;
    
-- sum()
SELECT
	SUM(P01F03)
FROM
	EMP01;

-- avg()
SELECT
	AVG(P01F03)
FROM
	EMP01;
    
-- max()
SELECT
	MAX(P01F03)
FROM
	EMP01;
    
-- min()
SELECT
	MIN(P01F03)
FROM
	EMP01;S