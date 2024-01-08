
USE DB1;

-- retrieve employee info with highest salary(1st highest)
-- Non corelated sub query
SELECT
	P01F01,
	P01F02,
	P01F03,
	P01F04
FROM
	EMP01
WHERE
	P01F03 = (
			SELECT
				MAX(P01F03)
			FROM
				EMP01
			);
                
-- retrieve employee info with 4th highest salary
-- corelated sub query
SELECT
	P01F01,
	P01F02,
	P01F03,
	P01F04
FROM
	EMP01 E1
WHERE
	3 = 
(
    SELECT
		COUNT(DISTINCT P01F03)
	FROM
		EMP01 E2
    WHERE E2.P01F03 > E1.P01F03
);
			
    