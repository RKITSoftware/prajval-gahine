
DELIMITER $$

CREATE PROCEDURE
	SALARY_CREDIT()
BEGIN
	-- stores the last salary credit date
	DECLARE v_last_salary_credit_date VARCHAR(10);
    
    SET v_last_salary_credit_date = (SELECT G01F01 FROM STG01);
    
    -- check if current month salary is already credited
    IF (MONTH(v_last_salary_credit_date) = MONTH(CURDATE()) AND YEAR(v_last_salary_credit_date) = YEAR(CURDATE())) THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Salary already credited for current month';
    END IF;

	-- creating a temporary table - EMP_MONTH_WH (employee month work hour) this stores employees current month employeeId and work hour
	CREATE TEMPORARY TABLE
		EMP_MONTH_WH(
			H01F01 INT COMMENT 'Employee Id',
            H01F02 FLOAT COMMENT 'Total work hours'
		);
        
	-- inserting employeeId and his/her current month workhour
	INSERT INTO
		EMP_MONTH_WH
	SELECT
		D01F02 AS "Employee Id",
		SUM(D01F04) AS "Month total work hour"
	FROM
		ATD01
	WHERE
		-- D01F03 BETWEEN last_salary_credit_date AND CURDATE()
		(v_last_salary_credit_date IS NULL) OR (v_last_salary_credit_date <= D01F03 AND D01F03 < CURDATE())
        -- MONTH(D01F03) = 1 AND YEAR(D01F03) = 2024
	GROUP BY 
		D01F02;
    
	-- inserting / crediting salary of all employees for current month
    INSERT INTO
		SLY01(Y01F02, Y01F03, Y01F04, Y01F05)
	SELECT
		H01F01, CURDATE(), H01F02 * (SELECT N01F04 FROM PSN01 WHERE N01F01=(SELECT P01F06 FROM EMP01 WHERE P01F01=H01F01))/42, (SELECT P01F06 FROM EMP01 WHERE P01F01=H01F01)
    FROM
		EMP_MONTH_WH;
        
	 -- updating the setting table (STG01) for latest salary-credit date
     UPDATE
		STG01
	SET
		G01F01=CURDATE();
        
	-- dropping the EMP_MONTH_WH temporary table
    DROP TEMPORARY TABLE IF EXISTS EMP_MONTH_WH;    
END $$