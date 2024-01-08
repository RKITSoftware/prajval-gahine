USE db1;
SELECT * FROM emp01;

DESC emp01;

-- creating an index, specifically a non-clustered index
CREATE INDEX
	IX_EMP01_P01F03
ON EMP01 (P01F03 ASC);

-- get all indexes associated with a table
SHOW INDEXES FROM
	EMP01;
-- dropping an index from a table
ALTER TABLE
	EMP01
DROP INDEX
	IX_EMP01_P01F03;
    
-- adding a custered index by adding primary key
ALTER table	
	EMP01
ADD PRIMARY KEY (P01F01);

-- dropping a clustered index
ALTER TABLE
	EMP01
DROP PRIMARY KEY;

-- advantages of using indexes
-- SELECT statement with WHERE clause
SELECT
	P01F01,
    P01F02
FROM
	EMP01
WHERE
	P01F03 > 1000 AND P01F03 < 1500;
    
-- DELETE or UPDATE statments
UPDATE
	EMP01
SET
	P01F03 = 5000
WHERE
	P01F03 = 1800;
    
-- ORDER BY
SELECT
	P01F01,
    P01F02
FROM
	EMP01
ORDER BY
	P01F03 DESC;
    
-- GROUP BY
SELECT P01F03, Count(P01F03)
FROM
	EMP01
GROUP BY
	P01F03;
    
-- covering query
SELECT
	P01F03 Salary
FROM
	EMP01;