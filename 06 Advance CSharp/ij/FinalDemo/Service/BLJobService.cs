using FinalDemo.Connection;
using FinalDemo.Models;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;

namespace FinalDemo.Service
{
    public class BLJobService
    {
        public IEnumerable<Job01> GetJobs()
        {
            try
            {
                using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
                {
                    objConnection.Open();

                    string query = "SELECT " +
                                       "b01f01," +
                                       "b01f02," +
                                       "b01f03 " +
                                  "FROM " +
                                       "job01";

                    IEnumerable<Job01> lstJobs = objConnection.Query<Job01>(query);

                    return lstJobs;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Job01 GetJobById(int jobId)
        {
            try
            {
                using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
                {
                    objConnection.Open();

                    string query = "SELECT " +
                                       "b01f01," +
                                       "b01f02," +
                                       "b01f03 " +
                                  "FROM " +
                                       "job01 " +
                                  "WHERE " +
                                       "b01f01 = @JobId";

                    Job01 job = objConnection.QueryFirstOrDefault<Job01>(query, new { JobId = jobId });

                    return job;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Job01 AddJob(Job01 objJob)
        {
            try
            {
                using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
                {
                    objConnection.Open();

                    string query = "INSERT INTO Job01 (" +
                                            "b01f02," +
                                            "b01f03) " +
                                    "VALUES (" +
                                            "@b01f02," +
                                            "@b01f03)";

                    using (MySqlCommand objcmd = new MySqlCommand(query, objConnection))
                    {
                        objcmd.Parameters.AddWithValue("@b01f02", objJob.b01f02);
                        objcmd.Parameters.AddWithValue("@b01f03", objJob.b01f03);

                        int rowsAffected = objcmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return objJob;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Job01 EditJob(int jobId, Job01 updatedJob)
        {
            try
            {
                using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
                {
                    objConnection.Open();

                    string query = "UPDATE Job01 " +
                                   "SET " +
                                   "b01f02 = @b01f02, " +
                                   "b01f03 = @b01f03 " +
                                   "WHERE " +
                                   "b01f01 = @JobId";

                    using (MySqlCommand objcmd = new MySqlCommand(query, objConnection))
                    {
                        objcmd.Parameters.AddWithValue("@JobId", jobId);
                        objcmd.Parameters.AddWithValue("@b01f02", updatedJob.b01f02);
                        objcmd.Parameters.AddWithValue("@b01f03", updatedJob.b01f03);

                        int rowsAffected = objcmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return updatedJob;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteJob(int jobId)
        {
            try
            {
                using (MySqlConnection objConnection = new MySqlConnection(Connections.connection))
                {
                    objConnection.Open();

                    string query = "DELETE FROM Job01 " +
                                   "WHERE " +
                                   "b01f01 = @JobId";

                    using (MySqlCommand objcmd = new MySqlCommand(query, objConnection))
                    {
                        objcmd.Parameters.AddWithValue("@JobId", jobId);

                        int rowsAffected = objcmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
