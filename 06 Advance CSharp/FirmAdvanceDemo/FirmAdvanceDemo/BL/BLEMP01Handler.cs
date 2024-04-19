using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utitlity.GeneralUtility;

namespace FirmAdvanceDemo.BL
{
    public class BLEMP01Handler
    {

        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Instance of EMP01 model
        /// </summary>
        private EMP01 _objEMP01;

        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Instance of BLUser
        /// </summary>
        private BLUSR01Handler _objBLUSR01Handler;

        private DBEMP01Context _context;

        public BLEMP01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _context = new DBEMP01Context();
        }

        /// <summary>
        /// Method to validate DTOUMP instance
        /// </summary>
        /// <param name="objDTOUMP">instance of DTOUMP</param>
        /// <param name="operation">Operation to perform</param>
        /// <returns>True if instance of DTOUMP is in valid format, else false</returns>
        public Response Prevalidate(DTOUMP01 objDTOUMP)
        {
            Response response;

            DTOUSR01 objDTOUSR01 = objDTOUMP.ObjDTOUSR01;
            DTOEMP01 dTOEMP01 = objDTOUMP.ObjDTOEMP01;

            _objBLUSR01Handler.Operation = Operation;
            response = _objBLUSR01Handler.Prevalidate(objDTOUSR01);

            if (!response.IsError)
            {
                // check if position ID exists in db (for A and E both)
                int positionId = dTOEMP01.P01F06;
                int positionCount;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    positionCount = (int)db.Count<EMP01>(emp01 => emp01.P01F06 == positionId);
                }
                if (positionCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Position not found wqith {positionId}";

                    return response;
                }

                // in case of E, check employee ID already exists?
                if (Operation == EnmOperation.E)
                {
                    int employeeId = dTOEMP01.P01F01;
                    int employeeCount;
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        employeeCount = (int)db.Count<EMP01>(emp01 => emp01.P01F01 == employeeId);
                    }

                    if (employeeCount == 0)
                    {
                        response.IsError = true;
                        response.HttpStatusCode = HttpStatusCode.NotFound;
                        response.Message = $"Employee not found with id: {employeeId}";

                        return response;
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Method to convert DTOEMP01 instance to EMP01 instance
        /// </summary>
        /// <param name="objDTOEMP01">Instance of DTOEMP01</param>
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
                _objEMP01.P01F07 = DateTime.Now;
            }
        }

        /// <summary>
        /// Method to validate the EMP01 instance
        /// </summary>
        /// <returns>True if EMP01 instance is valid else false</returns>
        public Response Validate()
        {
            Response response = _objBLUSR01Handler.Validate();
            // nothing to validate for employee as of now
            return response;
        }

        /// <summary>
        /// Method to create or update a record of emp01 table in DB
        /// </summary>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int userId;
                    int employeeId;
                    using (IDbTransaction tnx = db.BeginTransaction())
                    {
                        try
                        {
                            USR01 objUSR01 = _objBLUSR01Handler.ObjUSR01;

                            // save user
                            userId = (int)db.Insert(objUSR01, selectIdentity: true);

                            // save user role
                            ULE02 objULE02 = new ULE02()
                            {
                                E02F02 = userId,
                                E02F03 = EnmRole.E,
                                E02F04 = DateTime.Now
                            };
                            db.Insert<ULE02>(objULE02);

                            // save employee
                            employeeId = (int)db.Insert(_objEMP01, selectIdentity: true);

                            // save user employee id's
                            UMP02 objUMP02 = new UMP02()
                            {
                                P01F02 = userId,
                                P01F03 = employeeId,
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
                    response.Message = $"Employee created with user-id: {userId} and employee-id: {employeeId}.";
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

        public Response RetrieveEmployee(int employeeId)
        {
            Response response = new Response();
            DataTable dtEmployee = _context.FetchEmployee(employeeId);

            if (dtEmployee.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No employee found with id: {employeeId}";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtEmployee;
            return response;
        }

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