

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