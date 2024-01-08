

-- get employee details based on username (join)
USE firmdb;
SELECT
	R01F02 AS "username", 
    R01F04 AS "email",
    R01F05 AS "phone no",
    P01F02 AS "first name",
    P01F03 AS "last name",
    P01F04 AS "gender"
FROM
	USR01 INNER JOIN EMP01
WHERE USR01.R01F02 = 'prajvalgahine';


-- change password
UPDATE
	USR01
SET
	R01F03=0xc64bd616e354661872cf4f84d48b87b46fcbd001
WHERE
	R01F02='prajvalgahine' AND R01F03=0x70c5263eb726ab1b9afba4e7a56c036ec48aac06;


-- trigger for attendance table insertion
CREATE TRIGGER
	TRG_ATD01_INSERT
BEFORE INSERT ON
	ATD01
FOR EACH ROW
	

-- attendance filling
INSERT INTO
	ATD01
VALUES
	();
    
    
-- get current month attendance
SELECT
	D01F03 AS "date",
    D01F04 AS "hours"
FROM
	ATD01
WHERE D01F02=1 AND MONTH(D01F03)=MONTH(CURDATE()) AND YEAR(D01F03)=YEAR(CURDATE());

-- get current month attendance count
SELECT
	COUNT(*) AS "Attendance Count"
FROM
	ATD01
WHERE D01F02=1 AND MONTH(D01F03)=MONTH(CURDATE()) AND YEAR(D01F03)=YEAR(CURDATE());


-- get current month leaves
SELECT
	E01F04 AS "Leave Date",
    E01F05 AS "No. of Leaves"
FROM
	LVE01
WHERE E01F02=1 AND MONTH(E01F04)=MONTH(CURDATE()) AND YEAR(E01F04)=YEAR(CURDATE());


-- get current month leave count
SELECT
	SUM(E01F05) AS "Total current month leaves"
FROM
	LVE01
WHERE E01F02=1 AND MONTH(E01F04)=MONTH(CURDATE()) AND YEAR(E01F04)=YEAR(CURDATE());

