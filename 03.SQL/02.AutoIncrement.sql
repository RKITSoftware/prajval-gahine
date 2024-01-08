-- using database
USE
	FirmManagementDB;

-- Altering column specification
ALTER TABLE
	Employee
MODIFY
	Id INT UNIQUE AUTO_INCREMENT;
    
-- setting autoincrement starting value
ALTER TABLE
	Employee
AUTO_INCREMENT=101;

-- inserting a new record
INSERT INTO
	Employee(Username, Name, City, Country)
VALUES
	("rahul", "Rahul Chaudhari", "Surat", "India");
    
-- viewing Employee table
SELECT
	Id,
    Username,
    Name,
    City,
    Country
FROM
	Employee;