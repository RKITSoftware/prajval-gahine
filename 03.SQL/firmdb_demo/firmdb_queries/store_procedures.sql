   
DELIMITER $$
-- store procedure for insertion of user and employee record
CREATE PROCEDURE SP_INSERT_USR_EMP(
	p_username VARCHAR(45),
	p_hashed_password BINARY(20),
	p_email VARCHAR(254),
	p_phone_no VARCHAR(10),
	p_dob DATE,
	p_first_name VARCHAR(50),
	p_last_name VARCHAR(50),
	p_gender ENUM('M', 'F', 'O'),
	p_joining_date DATE,
	p_department_id INT,
    OUT last_id_in_USR01 INT
)
BEGIN
	DECLARE v_last_id_in_USR01 INT;
    DECLARE v_last_id_in_EMP01 INT;

	-- insert a user record
	INSERT INTO
		USR01(R01F02, R01F03, R01F04, R01F05, R01F06)
	VALUES
		(p_username, p_hashed_password, p_email, p_phone_no, p_dob);
	-- getting the last inserte query's first record id (AI column value) of user table
	SET v_last_id_in_USR01 = last_insert_id();
    SET last_id_in_USR01 = v_last_id_in_USR01;
        
    -- insert employee recorrd
	INSERT INTO
		EMP01(P01F02, P01F03, P01F04, P01F05, P01F06)
	VALUES
		(p_first_name, p_last_name, p_gender, p_joining_date, p_department_id);
    -- getting the last inserte query's first record id (AI column value) of employee table
	SET v_last_id_in_EMP01 = last_insert_id();

	-- insert user-employee record
	INSERT INTO
		USREMP01
	VALUES
		(v_last_id_in_USR01, v_last_id_in_EMP01);
end $$    
