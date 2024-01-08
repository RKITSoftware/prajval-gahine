-- creating an FirmManagementDB if not exits
CREATE DATABASE IF NOT EXISTS
	FirmManagementDB;

-- using a database
USE
	FirmManagementDB;
    
 -- Creating an Employee table if not exits
 CREATE  TABLE IF NOT EXISTS
 	Employee(
 		Id INT,
        Username VARCHAR(20),
		Name VARCHAR(50)
     );

-- Inserting records into Employee table
INSERT INTO
	Employee (Id, Username, Name)
VALUES
	(104, "prajval", "Prajval Gahine"),
	(102, "yash", "Yash Lathiya"),
	(101, "deep", "Deep Patel"),
	(103, "krinsi", "Krinsi Kayda");

-- Viewing Employee table
SELECT
	Id,
    Username,
    Name
FROM
	Employee;
    
-- Viewing data in sorted order
SELECT
	Id,
    Username,
    Name
From
	employee
ORDER BY
	Id ASC, Name;
