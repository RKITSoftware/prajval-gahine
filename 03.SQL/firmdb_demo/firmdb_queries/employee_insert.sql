
CALL SP_INSERT_USR_EMP(
	'gauravwagh',
    0xaabc363eb726ab1b9afba4e7a56c036ec48ac56b,
	'gauravwagh@gmail.com',
    '9824356654',
    '2021-12-05',
    'Gaurav',
    'Wagh',
    'M',
    "2002-02-18",
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
		WHERE E01F02='Employee')
    );
    
    
-- adding attendance
INSERT INTO
	ATD01(D01F02, D01F03, D01F04)
VALUES
	(2, "2024-01-05", 7.5),
    (2, "2024-01-06", 8),
    (2, "2024-01-07", 7);