using Dapper;
using FinalDemo.Connection;
using FinalDemo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FinalDemo.Service
{
    public class BLEmployeeService : AbstractGenericService<Emp01>
    {
        /// <summary>
        /// Retrieves a list of all employees from the database.
        /// </summary>
        /// <returns>An IEnumerable of Emp01 representing the list of employees.</returns>
        public override IEnumerable<Emp01> GetEmployees()
        {
            try
            {
                using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
                {
                    objConnection.Open();
                    string query = "SELECT " +
                                        "p01f01," +
                                        "p01f02," +
                                        "p01f03," +
                                        "p01f04 " +
                                 "FROM " +
                                        "emp01";
                    IEnumerable<Emp01> lstEmployees = objConnection.Query<Emp01>(query);
                    return lstEmployees;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a specific employee from the database based on the employee ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>An Emp01 object representing the employee details.</returns>
        public override Emp01 GetByID(int id)
        {
            Emp01 employee = null;

            using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
            {
                objConnection.Open();
                string query = "SELECT " +
                                        "p01f01," +
                                        "p01f02," +
                                        "p01f03," +
                                        "p01f04 " +
                                        
                                 "FROM " +
                                        "emp01 " +
                                 "WHERE " +
                                        "p01f01 = @id";
                using (MySqlCommand objcmd = new MySqlCommand(query, objConnection))
                {
                    objcmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader objReader = objcmd.ExecuteReader())
                    {
                        if (objReader.Read())
                        {
                            employee = new Emp01
                            {
                                p01f01 = Convert.ToInt32(objReader["p01f01"]),
                                p01f02 = objReader["p01f02"].ToString(),
                                p01f03 = objReader["p01f03"].ToString(),
                                p01f04 = objReader["p01f04"].ToString()
                            };
                        }
                    }
                }
            }
            return employee;
        }

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="objEmployee">An Emp01 object representing the employee to be added.</param>
        /// <returns>A string indicating the result of the operation.</returns>
        public override string AddEmployee(Emp01 objEmployee)
        {
            using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
            {
                objConnection.Open();

                // SQL query to insert employee data into the database
                string query = "INSERT INTO emp01 (" +
                                        "p01f01, " +
                                        "p01f02, " +
                                        "p01f03, " +
                                        "p01f04) " +
                               "VALUES (" +
                                       "@p01f01, " +
                                       "@p01f02, " +
                                       "@p01f03, " +
                                       "@p01f04)";

                using (MySqlCommand objcmd = new MySqlCommand(query, objConnection))
                {
                    // Add parameters to the SQL command
                    objcmd.Parameters.AddWithValue("@p01f01", objEmployee.p01f01);
                    objcmd.Parameters.AddWithValue("@p01f02", objEmployee.p01f02);
                    objcmd.Parameters.AddWithValue("@p01f03", objEmployee.p01f03);
                    objcmd.Parameters.AddWithValue("@p01f04", objEmployee.p01f04);

                    try
                    {
                        // Execute the SQL query and get the number of rows affected
                        int rowsAffected = objcmd.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            // Return a success response 
                            return "Employee has been Added Successfully";
                        }
                        else
                        {
                            // Return an InternalServerError if the insertion was not successful
                            return "Failed to insert employee";
                        }
                    }
                    catch (Exception)
                    {
                        // Return InternalServerError with the caught exception
                        return "Internal server error";
                    }
                }
            }
        }

        /// <summary>
        /// Updates an existing employee in the database.
        /// </summary>
        /// <param name="id">The ID of the employee to be updated.</param>
        /// <param name="objEmployee">An Emp01 object representing the updated employee details.</param>
        public override string UpdateEmployee(int id, Emp01 objEmployee)
        {
            using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
            {
                objConnection.Open();

                string query = "UPDATE " +
                                    "emp01 " +
                               "SET " +
                                   "p01f02 = @p01f02, " +
                                   "p01f03 = @p01f03, " +
                                   "p01f04 = @p01f04 " +  
                               "WHERE " +
                                    "p01f01 = @p01f01";

                using (MySqlCommand objcmd = new MySqlCommand(query, objConnection))
                {
                    objcmd.Parameters.AddWithValue("@p01f01", id);
                    objcmd.Parameters.AddWithValue("@p01f02", objEmployee.p01f02);
                    objcmd.Parameters.AddWithValue("@p01f03", objEmployee.p01f03);
                    objcmd.Parameters.AddWithValue("@p01f04", objEmployee.p01f04);
                    try
                    {
                        int rowsAffected = objcmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            // Return a success response with the updated resource
                            return "Updtaed Successfull";
                        }
                        else
                        {
                            // Return NotFound if the specified ID is not found
                            return $"Specific ID = {id} not found";
                        }
                    }
                    catch (Exception)
                    {
                        // Handle the exception appropriately (e.g., log it)
                        return "Internal Servert Error";
                    }
                }
            }
        }

        /// <summary>
        /// Removes an employee from the database based on the employee ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be removed.</param>
        /// <returns>A string indicating the result of the operation.</returns>
        public string RemoveEmployee(int id)
        {
            using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
            {
                objConnection.Open();

                string query = "DELETE " +
                                "FROM " +
                                    "emp01 " +
                                "WHERE " +
                                    "p01f01 = @p01f01";

                using (MySqlCommand command = new MySqlCommand(query, objConnection))
                {
                    command.Parameters.AddWithValue("@p01f01", id);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Return a success response with the deleted resource
                            return $"Employee with ID = {id} has been deleted";
                        }
                        else
                        {
                            // Return NotFound if the specified ID is not found
                            return $"Specific ID = {id} not found";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception appropriately (e.g., log it)
                        return ex.Message;
                    }
                }
            }
        }
    }
}