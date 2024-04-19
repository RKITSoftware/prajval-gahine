using FirmAdvanceDemo.Models.POCO;
using ORMLiteDemo.Enums;
using ORMLiteDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ORMLiteDemo
{
    /// <summary>
    /// Provides methods to initialize the database schema and insert initial data.
    /// </summary>
    public class InitializeDB
    {
        public static string GetHMACBase64(string text, string key)
        {
            key = "lz7rhwwdKiifUvj5RntozFts2vQ4F2WL";

            byte[] hash;
            using (HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            }
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Initializes the database by creating tables and inserting initial data if the database does not exist.
        /// </summary>
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

                if (count == 0)
                {
                    DateTime now = DateTime.Now;

                    db.ExecuteNonQuery(string.Format("CREATE DATABASE IF NOT EXISTS `{0}`", dbName));
                    db.ChangeDatabase(dbName);

                    // create and initiate stg01 table
                    db.CreateTableIfNotExists<STG01>();
                    db.Insert<STG01>(new STG01() { G01F02 = now, G01F03 = now});

                    // create and initiate rle01 table
                    db.CreateTableIfNotExists<RLE01>();
                    List<RLE01> lstRole = new List<RLE01>()
                    {
                        new RLE01(){ E01F02 = EnmRole.A, E01F03 = now, E01F04 = now},
                        new RLE01(){ E01F02 = EnmRole.E, E01F03 = now, E01F04 = now}
                    };
                    db.InsertAll<RLE01>(lstRole);

                    // create and initiate dpt01 table
                    db.CreateTableIfNotExists<DPT01>();
                    List<DPT01> lstDepartment = new List<DPT01>()
                    {
                        new DPT01(){ T01F02 = "Development", T01F03 = now, T01F04 = now},
                        new DPT01(){ T01F02 = "Marketing", T01F03 = now, T01F04 = now},
                        new DPT01(){ T01F02 = "Testing", T01F03 = now, T01F04 = now},
                        new DPT01(){ T01F02 = "Network", T01F03 = now, T01F04 = now}
                    };
                    db.InsertAll(lstDepartment);

                    // create and initiate psn01 table
                    db.CreateTableIfNotExists<PSN01>();
                    List<PSN01> lstPosition = new List<PSN01>()
                    {
                        new PSN01 { N01F02 = "Full Stack Developer", N01F03 = 6.50, N01F04 = 50000, N01F05 = 10000.00, N01F06 = 1, N01F07 = now, N01F08 = now },
                        new PSN01 { N01F02 = "Flutter", N01F03 = 4.50, N01F04 = 35000, N01F05 = 10000.00, N01F06 = 1, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Project Manager", N01F03 = 8.50, N01F04 = 68000, N01F05 = 25000.00, N01F06 = 1, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Social Media Coordinator", N01F03 = 3.50, N01F04 = 25000, N01F05 = 5000.00, N01F06 = 2, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Marketing Executive", N01F03 = 4.00, N01F04 = 30000, N01F05 = 4000.00, N01F06 = 2, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Director of Marketing", N01F03 = 7.50, N01F04 = 60000, N01F05 = 30000.00, N01F06 = 2, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "QA Engineer", N01F03 = 4.50, N01F04 = 35000, N01F05 = 10000.00, N01F06 = 3, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Test Engineer", N01F03 = 4.50, N01F04 = 35000, N01F05 = 10000.00, N01F06 = 3, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Software Tester", N01F03 = 5.50, N01F04 = 41500, N01F05 = 12000.00, N01F06 = 3, N01F07 = now, N01F08 = now},
                        new PSN01 { N01F02 = "Computer Technician", N01F03 = 4.00, N01F04 = 3000, N01F05 = 8000.00, N01F06 = 4, N01F07 = now, N01F08 = now}
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
                        R01F02 = adminUsername,
                        R01F03 = GetHMACBase64(adminPassword, passwordHashSecretKey),
                        R01F04 = adminEmail,
                        R01F05 = adminPhoneNo,
                        R01F06 = now,
                        R01F07 = now
                    }, selectIdentity: true);

                    // add above user role in ule02 table
                    db.CreateTableIfNotExists<ULE02>();
                    db.Insert<ULE02>(new ULE02
                    {
                        E02F02 = userId,
                        E02F03 = EnmRole.A,
                        E02F04 = now,
                        E02F05 = now
                    });

                    db.CreateTableIfNotExists<EMP01>();
                    db.CreateTableIfNotExists<UMP02>();
                    db.CreateTableIfNotExists<ATD01>();
                    db.CreateTableIfNotExists<LVE02>();
                    // Service Stack free qouta limit reached

                    db.ExecuteNonQuery(@"
CREATE TABLE `SLY01` 
(
  `Y01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `Y01F02` INT(11) NOT NULL, 
  `Y01F03` DATETIME NOT NULL, 
  `Y01F04` DOUBLE NOT NULL, 
  `Y01F05` INT(11) NOT NULL, 
  `Y01F06` DATETIME NOT NULL, 
  `Y01F07` DATETIME NOT NULL, 

  CONSTRAINT `FK_SLY01_EMP01_Y01F02` FOREIGN KEY (`Y01F02`) REFERENCES `EMP01` (`P01F01`) ON DELETE CASCADE, 

  CONSTRAINT `FK_SLY01_PSN01_Y01F05` FOREIGN KEY (`Y01F05`) REFERENCES `PSN01` (`N01F01`) ON DELETE CASCADE 
)");

                    db.ExecuteNonQuery(@"
CREATE TABLE `PCH01` 
(
  `D01F01` INT(11) PRIMARY KEY AUTO_INCREMENT, 
  `H01F02` INT(11) NOT NULL, 
  `H01F03` DATETIME NOT NULL, 
  `H01F04` INT(11) NULL, 
  `H01F05` DATETIME NOT NULL, 
  `H01F06` DATETIME NOT NULL, 

  CONSTRAINT `FK_PCH01_EMP01_H01F02` FOREIGN KEY (`H01F02`) REFERENCES `EMP01` (`P01F01`) ON DELETE CASCADE 
)");
                }
            }
        }
    }
}