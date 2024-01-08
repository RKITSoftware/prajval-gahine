USE db1;

-- taking a backup into a text file
SELECT
	*
INTO OUTFILE
	"C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\db1_textbackup.txt"
FROM
	EMP01;
    
-- creating an employee table in which backup is to be taken
CREATE TABLE
	EMP04(
		P04F01 INT PRIMARY KEY,
        P04F02 VARCHAR(50),
        P04F03 INT,
        P04F04 INT
    );
    
-- restoring the text backup in EMP04 table
LOAD DATA LOCAL INFILE
	"C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\db1_textbackup.txt"
INTO TABLE
	EMP04;

-- checking if EMP04 table has populated
SELECT
	P04F01 Id, P04F02 Name
FROM
	EMP04;
    

SHOW VARIABLES LIKE "secure_file_priv";
SHOW GLOBAL VARIABLES LIKE "local_infile";
SET GLOBAL local_infile = true;