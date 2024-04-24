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
    public class BLEMP01Handler
    {
        /// <summary>
        /// Factory for creating OrmLite database connections.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Instance of EMP01 POCO model.
        /// </summary>

        private EMP01 _objEMP01;
        /// <summary>
        /// Context for Employee handler.
        /// </summary>
        private readonly DBEMP01Context _context;

        /// <summary>
        /// Gets or sets the operation type for employee handling.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Instance of BLUSR01Handler
        /// </summary>
        private BLUSR01Handler _objBLUSR01Handler;

        /// <summary>
        /// Default constructor for BLEMP01Handler.
        /// </summary>
        public BLEMP01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
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

            // only admin can perform add and this is handled using authorization
            // attribute on POST method
            /*
            if(Operation == EnmOperation.A && !GeneralUtility.IsAdmin())
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "Not an admin.";

                return response;
            }
            */

            // edit operation must be done by admin or authorized employee
            // this is also handled in PUT using ValidateAccess method
            /*
            if(Operation == EnmOperation.E && !GeneralUtility.IsAdmin() && !GeneralUtility.IsValidEmployee(dTOEMP01.P01F01))
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                response.Message = $"You are not authorized to access employee {dTOEMP01.P01F01}.";

                return response;
            }
            */

            _objBLUSR01Handler = new BLUSR01Handler();

            _objBLUSR01Handler.Operation = Operation;
            response = _objBLUSR01Handler.Prevalidate(objDTOUSR01);

            if (!response.IsError)
            {
                // check if position ID exists in db (for A and E both)
                int positionId = objDTOEMP01.P01F06;
                int positionCount;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    positionCount = (int)db.Count<PSN01>(position => position.N01F01 == positionId);
                }
                if (positionCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Position not found with {positionId}";

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
                    employeeID = GeneralHandler.RetrieveEmployeeIDByUserID(objDTOUSR01.R01F01);

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

            _objBLUSR01Handler.Operation = Operation;
            _objBLUSR01Handler.Presave(objDTOUSR01);

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
        /// Validates the employee data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = _objBLUSR01Handler.Validate();
            // nothing to validate for employee as of now
            return response;
        }

        /// <summary>
        /// Saves the employee data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int userId;
                    int employeeID;
                    using (IDbTransaction tnx = db.OpenTransaction())
                    {
                        try
                        {
                            USR01 objUSR01 = _objBLUSR01Handler.ObjUSR01;

                            // save user
                            userId = (int)db.Insert(objUSR01, selectIdentity: true);

                            // get role id
                            //int roleID = db.Scalar<RLE01, int>(role => role.E01F01, role => role.E01F02 == EnmRole.A);

                            SqlExpression<RLE01> sqlExp = db.From<RLE01>()
                                .Where(role => Sql.In(role.E01F02, _objBLUSR01Handler.LstRole))
                                .Select(role => role.E01F01);

                            DateTime now = DateTime.Now;

                            List<ULE02> lstUserRole = db.Select<int>(sqlExp).Select(roleID => new ULE02()
                            {
                                E02F02 = userId,
                                E02F03 = roleID,
                                E02F04 = now
                            }).ToList();
                            //ULE02 objULE02 = new ULE02()
                            //{
                            //    E02F02 = userId,
                            //    E02F03 = roleID,
                            //    E02F04 = DateTime.Now
                            //};
                            db.InsertAll<ULE02>(lstUserRole);

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
                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Employee created with user-id: {userId} and employee-id: {employeeID}.";
                    return response;
                }
                else
                {
                    // update user
                    USR01 objUSR01 = _objBLUSR01Handler.ObjUSR01;
                    db.Update<USR01>(objUSR01);

                    // update employee
                    db.Update<EMP01>(_objEMP01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Employee data updated.";
                    return response;
                }
            }
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

//{
//    "r01102": "test1",
//  "r01103": "test1@123",
//  "r01104": "test1@gmail.com",
//  "r01105": "9999999999",
//  "r01106": [
//    1, 2
//  ],
//  "p01102": "Test1",
//  "p01103": "Gahine",
//  "p01104": "m",
//  "p01105": "2001-12-07",
//  "p01106": 1
//}

//{
//    "r01102": "prajvalgahine",
//  "r01103": "Prajval@123",
//  "r01104": "prajvalgahine@gmail.com",
//  "r01105": "9924380554",
//  "r01106": [
//    0
//  ],
//  "p01102": "Prajval",
//  "p01103": "Gahine",
//  "p01104": "m",
//  "p01105": "2001-12-07",
//  "p01106": 1
//}