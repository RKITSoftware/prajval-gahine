

-- get employee details based on username (join)
USE firmdb;
CREATE OR REPLACE VIEW
	VWS_EMP01_USR01
AS
SELECT
	R01F02 AS "username", 
	R01F04 AS "email",
	R01F05 AS "phone no",
    EMP01.P01F02 AS "first name",
    P01F03 AS "last name",
    P01F04 AS "gender"
FROM
	(		
		SELECT
			R01F02, 
			R01F04,
			R01F05,
            P01F02
		FROM
			USR01 INNER JOIN USREMP01 ON  USR01.R01F01 = USREMP01.P01F01
	) AS T INNER JOIN EMP01 ON T.P01F02 = EMP01.P01F01;



	



-- credit salary
CALL SALARY_CREDIT();
