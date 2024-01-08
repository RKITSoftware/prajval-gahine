USE DB1;

-- CREATING EMPLOYEE TABLE(EMP02 - Rajkot branch)
-- P02F01 - employee id
-- P02F02 - employee name
-- P02F03 - employee salary
-- P02F04 - employee department id
CREATE TABLE
	EMP02(
		P02F01 INT,
        P02F02 VARCHAR(50),
        P02F03 INT,
        P02F04 INT
    );

-- inserting records into EMP02 table
INSERT INTO
	EMP02
VALUES
	(101, "Prajval", 1200, 1002),
	(102, "Rahul", 1600, 1002);
    
    
    
    
-- CREATING EMPLOYEE TABLE(EMP03 - Ahemdabad branch)
-- P03F01 - employee id
-- P03F02 - employee name
-- P03F03 - employee salary
-- P03F04 - employee department id
CREATE TABLE
	EMP03(
		P03F01 INT,
        P03F02 VARCHAR(50),
        P03F03 INT,
        P03F04 INT
    );
    
-- inserting records into EMP03 table
INSERT INTO
	EMP03
VALUES
	(101, "Prajval", 1200, 1002),
	(103, "Deep", 1500, 1001),
	(104, "Krinsi", 1800, 1001),
	(105, "Gaurav", 1200, 1005);
    
-- TRUNCATE EMP02;
-- TRUNCATE EMP03;
    
-- set operators - UNION, UNION ALL, INTERSECT, EXCEPT
SELECT
	P02F01,
    P02F02,
    P02F03,
    P02F04
FROM
	EMP02
EXCEPT
SELECT
	P03F01,
    P03F02,
    P03F03,
    P03F04
FROM
	EMP03;


