using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using static FirmAdvanceDemo.Utitlity.GeneralUtility;

namespace FirmAdvanceDemo
{
    public class InitializeDB
    {
        public static void Init()
        {
            // get the connection string
            string connectionString = (string)ConfigurationManager.ConnectionStrings["connect-to-mysqld"].ConnectionString;

            // create a connection factory (IDbConnectionFactory)
            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            using (IDbConnection db = dbFactory.OpenDbConnection())
            {
                string dbName = (string)ConfigurationManager.AppSettings["database-name"];
                // check if db exists
                string query = string.Format(@"
SELECT
    COUNT(*)
FROM
    INFORMATION_SCHEMA.SCHEMATA
WHERE
    SCHEMA_NAME = '{0}';
", dbName);
                int count = db.Scalar<int>(query);

                if(count == 0)
                {
                    DateTime now = DateTime.Now;

                    db.ExecuteNonQuery(string.Format("CREATE DATABASE IF NOT EXISTS `{0}`", dbName));
                    db.ChangeDatabase(dbName);

                    // create and initiate stg01 table
                    db.CreateTableIfNotExists<STG01>();
                    db.Insert<STG01>(new STG01() { g01f02 = null, g01f03 = now, g01f04 = now });

                    // create and initiate rle01 table
                    db.CreateTableIfNotExists<RLE01>();
                    List<RLE01> lstRole = new List<RLE01>()
                    {
                        new RLE01(){ e01f02 = "Admin", e01f03 = now, e01f04 = now},
                        new RLE01(){ e01f02 = "Employee", e01f03 = now, e01f04 = now}
                    };
                    db.InsertAll<RLE01>(lstRole);

                    // create and initiate dpt01 table
                    db.CreateTableIfNotExists<DPT01>();
                    List<DPT01> lstDepartment = new List<DPT01>()
                    {
                        new DPT01(){ t01f02 = "Development", t01f03 = now, t01f04 = now},
                        new DPT01(){ t01f02 = "Marketing", t01f03 = now, t01f04 = now},
                        new DPT01(){ t01f02 = "Testing", t01f03 = now, t01f04 = now},
                        new DPT01(){ t01f02 = "Network", t01f03 = now, t01f04 = now}
                    };
                    db.InsertAll(lstDepartment);

                    // create and initiate psn01 table
                    db.CreateTableIfNotExists<PSN01>();
                    List<PSN01> lstPosition = new List<PSN01>()
                    {
                        new PSN01 { n01f02 = "Full Stack Developer", n01f03 = 6.50, n01f04 = 50000, n01f05 = 10000.00, n01f06 = 1, n01f07 = now, n01f08 = now },
                        new PSN01 { n01f02 = "Flutter", n01f03 = 4.50, n01f04 = 35000, n01f05 = 10000.00, n01f06 = 1, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Project Manager", n01f03 = 8.50, n01f04 = 68000, n01f05 = 25000.00, n01f06 = 1, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Social Media Coordinator", n01f03 = 3.50, n01f04 = 25000, n01f05 = 5000.00, n01f06 = 2, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Marketing Executive", n01f03 = 4.00, n01f04 = 30000, n01f05 = 4000.00, n01f06 = 2, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Director of Marketing", n01f03 = 7.50, n01f04 = 60000, n01f05 = 30000.00, n01f06 = 2, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "QA Engineer", n01f03 = 4.50, n01f04 = 35000, n01f05 = 10000.00, n01f06 = 3, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Test Engineer", n01f03 = 4.50, n01f04 = 35000, n01f05 = 10000.00, n01f06 = 3, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Software Tester", n01f03 = 5.50, n01f04 = 41500, n01f05 = 12000.00, n01f06 = 3, n01f07 = now, n01f08 = now},
                        new PSN01 { n01f02 = "Computer Technician", n01f03 = 4.00, n01f04 = 3000, n01f05 = 8000.00, n01f06 = 4, n01f07 = now, n01f08 = now}
                    };
                    db.InsertAll<PSN01>(lstPosition);

                    // create reaming tables
                    db.CreateTableIfNotExists<USR01>();
                    // initiate a default admin for the system
                    string passwordHashSecretKey = ConfigurationManager.AppSettings["password-hash-secret-key"];
                    string adminUsername = ConfigurationManager.AppSettings["admin-username"];
                    string adminPassword = ConfigurationManager.AppSettings["admin-password"];
                    string adminEmail = ConfigurationManager.AppSettings["admin-email"];
                    string adminPhoneNo = ConfigurationManager.AppSettings["padmin-phone-no"];
                    int userId = (int)db.Insert<USR01>(new USR01()
                    {
                        r01f02 = adminUsername,
                        r01f03 = GetHMAC(adminPassword, passwordHashSecretKey),
                        r01f04 = adminEmail,
                        r01f05 = adminPhoneNo,
                        r01f06 = now,
                        r01f07 = now
                    }, selectIdentity: true);

                    // add above user role in ule02 table
                    db.CreateTableIfNotExists<ULE02>();
                    db.Insert<ULE02>(new ULE02 { e02f02 = userId,
                        e02f03 = EnmRole.Admin,
                        e02f04 = now,
                        e02f05 = now
                    });

                    db.CreateTableIfNotExists<EMP01>();
                    db.CreateTableIfNotExists<UMP02>();
                    db.CreateTableIfNotExists<ATD01>();
                    db.CreateTableIfNotExists<LVE02>();

                    db.ExecuteNonQuery(@"
CREATE TABLE `SLY01` 
(
  `y01f01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `y01f02` INT(11) NOT NULL, 
  `y01f03` DATETIME NOT NULL, 
  `y01f04` DOUBLE NOT NULL, 
  `y01f05` INT(11) NOT NULL, 
  `y01f06` DATETIME NOT NULL, 
  `y01f07` DATETIME NOT NULL, 

  CONSTRAINT `FK_SLY01_EMP01_y01f02` FOREIGN KEY (`y01f02`) REFERENCES `EMP01` (`p01f01`) ON DELETE CASCADE, 

  CONSTRAINT `FK_SLY01_PSN01_y01f05` FOREIGN KEY (`y01f05`) REFERENCES `PSN01` (`n01f01`) ON DELETE CASCADE 
)");

                    db.ExecuteNonQuery(@"
CREATE TABLE `PCH01` 
(
  `d01f01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `h01f02` INT(11) NOT NULL, 
  `h01f03` DATETIME NOT NULL, 
  `h01f04` INT(11) NULL, 
  `h01f05` DATETIME NOT NULL, 
  `h01f06` DATETIME NOT NULL, 

  CONSTRAINT `FK_PCH01_EMP01_h01f02` FOREIGN KEY (`h01f02`) REFERENCES `EMP01` (`p01f01`) ON DELETE CASCADE 
)");
                }
            }
        }
    }
}