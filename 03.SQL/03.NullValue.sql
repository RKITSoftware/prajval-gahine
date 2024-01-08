-- using a database
USE
	FirmManagementDB;
    
-- adding two new columns
ALTER TABLE
	Employee
ADD
	City VARCHAR(20),
ADD
    Country VARCHAR(20) NOT NULL DEFAULT "India";
    
-- viewing Employee table
SELECT
	Id,
    Username,
    Name,
    Name,
    IFNULL(City, "No City") AS City,
    Country
FROM
	Employee;