
-- ddl commands
-- create table by specifying the table structure(data)
CREATE TABLE
	STD01(
		D01F01 INT,
        D01F02 VARCHAR(50),
        D01F03 INT
    );

INSERT INTO
	STD01(D01F01, D01F02, D01F03)
VALUES
	(101, "Prajval", 70),
	(102, "Rahul", 80),
	(103, "Gaurav", 75);

-- creating table using existing table
CREATE TABLE
	STD02
AS SELECT
	D01F01, D01F02, D01F03
FROM
	STD01;
		
-- droping table
DROP TABLE ST02;


-- altering table
ALTER TABLE
	STD01
ADD
	D01F04 INT;
    
ALTER table
	STD01
MODIFY
	D01F02 VARCHAR(40);
    
ALTER TABLE
	STD01
DROP COLUMN
	D01F04;
    
-- truncating a table
-- DROP TABLE STD01;
TRUNCATE STD01;

-- renaming column
ALTER TABLE
	STD01
RENAME COLUMN
	D01F01 TO D01F05;
    
-- TCL
DELETE FROM
	STD01
WHERE
	D01F01=101;
ROLLBACK;

SAVEPOINT STD01With101Deleted;

ROLLBACK TO STD01With101Deleted;

RELEASE SAVEPOINT STD01With101Deleted;

-- DCL

GRANT SELECT ON STD01 TO "png@localhost",;
REVOKE SELECT ON STD01 FROM "png@localhost";

