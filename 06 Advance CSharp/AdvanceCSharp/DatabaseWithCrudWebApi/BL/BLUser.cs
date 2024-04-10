using DatabaseWithCrudWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseWithCrudWebApi;
using MySql.Data.MySqlClient;
using System.Data.Common;
using Org.BouncyCastle.Asn1.X509;
using System.Runtime.InteropServices;
using ServiceStack;

namespace DatabaseWithCrudWebApi.BL
{
    public class BLUser
    {

        private USR01 _objUSR01;

        private MySqlConnection _connection;

        public BLUser()
        {
            _connection = Connection.connection;
        }

        public void Presave(DTOUSR01 objDTOUSR01, EnmOperation operation)
        {
            _objUSR01 = Utility.ConvertModel<USR01>(objDTOUSR01);

            DateTime now = DateTime.Now;
            if(operation == EnmOperation.Create)
            {
                _objUSR01.r01f04 = now;
            }
            _objUSR01.r01f05 = now;
        }

        public bool Validate(EnmOperation operation)
        {
            // check the username already exists?
            string username = _objUSR01.r01f02;
            return !Exists(username);
        }

        public bool Exists(string username)
        {
            int count = -1;
            using (MySqlCommand cmd = new MySqlCommand(string.Format(@"
SELECT
    COUNT(*) AS count
FROM
    USR01
WHERE
    r01f02 = {0}
", username), _connection))
            {
                _connection.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    count = reader.GetInt32("count");
                }
                _connection.Close();
            }
            return count != 0;
        }

        public void Save(EnmOperation operation)
        {
            if(operation == EnmOperation.Create)
            {
                using(MySqlCommand cmd = new MySqlCommand(
                    string.Format(@"
                        INSERT INTO
                            USR01 (r01f02, r01f03, r01f04, r01f05)
                        VALUES
                        ({0}, {1}, {2}, {3});
                        ",
                        _objUSR01.r01f02,
                        _objUSR01.r01f03,
                        _objUSR01.r01f04,
                        _objUSR01.r01f05
                    ),
                    _connection))
                {
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
            else
            {
                string semiQuery = "UPDATE USR01 SET";
                if(_objUSR01.r01f02 != null)
                {
                    semiQuery += $" r01f02 = {_objUSR01.r01f02},";
                }
                if(_objUSR01.r01f03 != null)
                {
                    semiQuery += $" r01f03 = {_objUSR01.r01f03}";
                }
                semiQuery = semiQuery.Substring(semiQuery.Length - semiQuery.LastIndexOf(","));
                using(MySqlCommand cmd = new MySqlCommand(semiQuery, _connection))
                {
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();
                }
            }
        }
    }
}