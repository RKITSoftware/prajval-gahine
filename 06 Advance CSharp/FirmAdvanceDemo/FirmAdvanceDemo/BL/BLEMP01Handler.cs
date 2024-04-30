using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utility.GeneralUtility;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to employee operations.
    /// </summary>
    public class BLEMP01Handler : BLUSR01Handler
    {
        /// <summary>
        /// Instance of EMP01 POCO model.
        /// </summary>

        private EMP01 _objEMP01;

        /// <summary>
        /// Context for Employee handler.
        /// </summary>
        private readonly DBEMP01Context _context;

        private bool _isAdmin;

        /// <summary>
        /// Default constructor for BLEMP01Handler.
        /// </summary>
        public BLEMP01Handler()
        {
            _context = new DBEMP01Context();
        }

        /// <summary>
        /// Method to validate the provided DTOUMP01 instance before processing.
        /// </summary>
        /// <param name="objDTOUMP">DTOUMP01 instance containing user and employee data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response Prevalidate(DTOUMP01 objDTOUMP)
        {
            Response response = new Response();

            DTOUSR01 objDTOUSR01 = objDTOUMP.ObjDTOUSR01;
            DTOEMP01 objDTOEMP01 = objDTOUMP.ObjDTOEMP01;

            response = PrevalidateUser(objDTOUSR01);

            if (!response.IsError)
            {
                // check if position ID exists in db (for A and E both)
                bool isPositionExists;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isPositionExists = db.Exists<PSN01>(new { N01F01 = objDTOEMP01.P01F06 });
                }
                if (!isPositionExists)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Position not found with {objDTOEMP01.P01F06}";

                    return response;
                }

                // in case of E, check employee ID already exists?
                if (Operation == EnmOperation.E)
                {
                    int employeeID = objDTOEMP01.P01F01;
                    int employeeCount;
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        employeeCount = (int)db.Count<EMP01>(emp01 => emp01.P01F01 == employeeID);
                    }

                    if (employeeCount == 0)
                    {
                        response.IsError = true;
                        response.HttpStatusCode = HttpStatusCode.NotFound;
                        response.Message = $"Employeem {employeeID} not found.";

                        return response;
                    }

                    // check if given mapping (userID-employeeID) is correct
                    employeeID = GeneralHandler.RetrieveemployeeIDByUserID(objDTOUSR01.R01F01);

                    if (employeeID != objDTOEMP01.P01F01)
                    {
                        response.IsError = true;
                        response.HttpStatusCode = HttpStatusCode.Conflict;
                        response.Message = $"The employee ID retrieved for the user ID does not match the employee ID provided in the request.";

                        return response;
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Sets the data for the employee before saving it.
        /// </summary>
        /// <param name="objUSREMP">DTO containing user and employee data.</param>
        public void Presave(DTOUMP01 objUSREMP)
        {
            // presave USR01
            DTOUSR01 objDTOUSR01 = objUSREMP.ObjDTOUSR01;
            DTOEMP01 objDTOEMP01 = objUSREMP.ObjDTOEMP01;

            _isAdmin = objDTOEMP01.P01X07;

            PresaveUser(objDTOUSR01);

            _objEMP01 = objDTOEMP01.ConvertModel<EMP01>();

            if (Operation == EnmOperation.A)
            {
                _objEMP01.P01F01 = 0;
                _objEMP01.P01F07 = DateTime.Now;
            }
            else
            {
                _objEMP01.P01F08 = DateTime.Now;
            }
        }

        /// <summary>
        /// Saves the employee data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();
            DateTime now = DateTime.Now;

            if (Operation == EnmOperation.A)
            {
                int userId;
                int employeeID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction tnx = db.OpenTransaction())
                    {
                        try
                        {
                            // save user
                            userId = (int)db.Insert(_objUSR01, selectIdentity: true);

                            List<EnmRole> lstEnmRole = new List<EnmRole> { EnmRole.E };

                            if (_isAdmin)
                            {
                                lstEnmRole.Add(EnmRole.A);
                            }

                            SqlExpression<RLE01> sqlExp = db.From<RLE01>()
                                .Where(role => Sql.In(role.E01F02, lstEnmRole))
                                .Select(role => role.E01F01);

                            List<ULE02> lstULE02 = db.Select<int>(sqlExp).Select(roleID => new ULE02()
                            {
                                E02F02 = userId,
                                E02F03 = roleID,
                                E02F04 = now
                            }).ToList();
                            db.InsertAll<ULE02>(lstULE02);


                            // save employee
                            employeeID = (int)db.Insert(_objEMP01, selectIdentity: true);

                            // save user employee id's
                            UMP02 objUMP02 = new UMP02()
                            {
                                P02F02 = userId,
                                P02F03 = employeeID,
                            };
                            db.Insert<UMP02>(objUMP02);

                            tnx.Commit();
                        }
                        catch
                        {
                            tnx.Rollback();
                            throw;
                        }
                    }
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Employee created with user-id: {userId} and employee-id: {employeeID}.";
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction tnx = db.OpenTransaction())
                    {
                        try
                        {
                            db.Update<USR01>(_objUSR01);

                            // update employee
                            db.Update<EMP01>(_objEMP01);

                            tnx.Commit();
                        }
                        catch
                        {
                            tnx.Rollback();
                            throw;
                        }
                    }
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Employee data updated.";
            }
            return response;
        }

        /// <summary>
        /// Retrieves an employee with the specified employee ID.
        /// </summary>
        /// <param name="employeeID">The ID of the employee to retrieve.</param>
        /// <returns>A response containing the retrieved employee data.</returns>
        public Response RetrieveEmployee(int employeeID)
        {
            Response response = new Response();
            DataTable dtEmployee = _context.FetchEmployee(employeeID);

            if (dtEmployee.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No employee found with id: {employeeID}";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtEmployee;
            return response;
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A response containing the retrieved employee data.</returns>
        public Response RetrieveEmployee()
        {
            Response response = new Response();
            DataTable dtEmployee = _context.FetchEmployee();

            if (dtEmployee.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No employee found";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtEmployee;
            return response;
        }
    }
}