
-- using firmdb database
use firmdb;

-- inserting records into role table (RLE01)
INSERT INTO
	RLE01(E01F02)
VALUES
	('Admin'),
    ('Employee');
    
SELECT * FROM RLE01;
    
-- inserting records into departemnt table (DPT01)
INSERT INTO
	DPT01(T01F02)
VALUES
	('Development'),
    ('Marketing'),
    ('Testing'),
    ('Network');
    
SELECT * FROM DPT01;
    
-- insert records into position table
INSERT INTO
	PSN01(N01F02, N01F03, N01F04, N01F05, N01F06)
VALUES
	('Full Stack Developer', 6.5, 50000, 10000, 1),
    ('Flutter', 4.5, 35000, 10000, 1),
    ('Project Manager', 8.5, 68000, 25000, 1),
    ('Social Media Coordinator', 3.5, 25000, 5000, 2),
    ('Marketing Executive', 4.0, 30000, 4000, 2),
    ('Director of Marketing', 7.5, 60000, 30000, 2),
    ('QA Engineer', 4.5, 35000, 10000, 3),
    ('Test Engineer', 4.5, 35000, 10000, 3),
    ('Software Tester', 5.5, 41500, 12000, 3),    
    ('Computer Technician', 4.0, 3000, 8000, 4),
    ('Webmaster', 4.5, 35000, 10000, 4),
    ('Network Administrator', 5.5, 41500, 12000, 4),
    ('Cyber Security Technician', 6.5, 50000, 15000, 4);

SELECT * FROM PSN01;    

-- insert an user, an employee
SET @last_id_in_USR01 = 0;
CALL SP_INSERT_USR_EMP(
	'prajvalgahine',
    0x70c5263eb726ab1b9afba4e7a56c036ec48aac06,
	'prajvalgahine@gmail.com',
    '9924380554',
    '2023-05-11',
    'Prajval',
    'Gahine',
    'M',
    "2001-12-07",
    1,
    @last_id_in_USR01);
    
-- insert a user-role record
INSERT INTO
	RLEUSR01(R01F01, R01F02)
VALUES
	(
		@last_id_in_USR01,
        (SELECT
			E01F01
		FROM
			RLE01
		WHERE E01F02='Admin')
    ),
	(
		@last_id_in_USR01,
        (SELECT
			E01F01
		FROM
			RLE01
		WHERE E01F02='Employee')
    );
    
    
-- insert recored into attendance table
INSERT INTO
	ATD01(D01F02, D01F03, D01F04)
VALUES
	(1, "2024-01-05", 8.5);

-- insert record into leave table
INSERT INTO
	LVE01(E01F02, E01F03, E01F04, E01F05, E01F06)
VALUES
	(1, '2024-01-05', '2024-01-15', 1, 'Driving test');
 

-- insert record into salary table
INSERT INTO
	SLY01(Y01F02, Y01F03, Y01F04, Y01F05)
VALUES
	(1, "2023-12-24", 50000, 2);
        