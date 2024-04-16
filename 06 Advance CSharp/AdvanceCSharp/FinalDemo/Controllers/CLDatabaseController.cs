using MySql.Data.MySqlClient;
using System;
using System.Web.Http;
using static DatabaseWithCrudWebApi.DBConnection;

namespace DatabaseWithCrudWebApi.Controllers
{
    /// <summary>
    /// Controller for handling database-related API requests.
    /// </summary>
    [RoutePrefix("api/database")]
    public class CLDatabaseController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Creates and initializes the database table.
        /// </summary>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post()
        {
            string query = @"
CREATE TABLE usr01 (
    r01f01 INT AUTO_INCREMENT PRIMARY KEY,
    r01f02 VARCHAR(255) NOT NULL,
    r01f03 VARCHAR(20) NOT NULL,
    r01f04 DATETIME,
    r01f05 DATETIME,

    UNIQUE INDEX idx_usr01_r01f02 (r01f02)
);";
            MySqlCommand cmd = new MySqlCommand(query, DBConnection.Connection);

            try
            {

                DBConnection.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DBConnection.Connection.Close();
            }
            return Ok("Database created and initialized");
        }

        #endregion
    }
}
