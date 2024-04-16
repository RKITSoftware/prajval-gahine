using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utitlity.GeneralUtility;

namespace FirmAdvanceDemo.BL
{
    public class BLEMP01Handler : BLResource<EMP01>
    {
        /// <summary>
        /// Instance of EMP01 model
        /// </summary>
        private EMP01 _objEMP01;

        /// <summary>
        /// Instance of BLUser
        /// </summary>
        private BLUSR01Handler _objBLUser;

        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Default constructor for BLEmployee, initializes EMP01 instance
        /// </summary>
        public BLEMP01Handler(Response _statusInfo) : base(_statusInfo)
        {
        }

        public BLEMP01Handler()
        {

        }

        /// <summary>
        /// Method to validate DTOUMP instance
        /// </summary>
        /// <param name="objDTOUMP">instance of DTOUMP</param>
        /// <param name="operation">Operation to perform</param>
        /// <returns>True if instance of DTOUMP is in valid format, else false</returns>
        public bool Prevalidate(DTOUMP objDTOUMP, EnmOperation operation)
        {
            _objBLUser = new BLUSR01Handler(_statusInfo);

            if (_objBLUser.Prevalidate(objDTOUMP, EnmRole.E, operation))
            {
                bool isValid = false;
                if(operation == EnmOperation.A)
                {
                    isValid = ValidateGender(objDTOUMP.p01f04)
                        && !objDTOUMP.p01f02.IsNullOrEmpty() && ValidateName(objDTOUMP.p01f02)
                        && !objDTOUMP.p01f03.IsNullOrEmpty() && ValidateName(objDTOUMP.p01f03)
                        && ValidateDOB(objDTOUMP.p01f05);
                }
                else
                {
                    isValid = ValidateGender(objDTOUMP.p01f04)
                        && (objDTOUMP.p01f02.IsNullOrEmpty() || ValidateName(objDTOUMP.p01f02))
                        && (objDTOUMP.p01f03.IsNullOrEmpty() || ValidateName(objDTOUMP.p01f03))
                        && ValidateDOB(objDTOUMP.p01f05);
                }
                if (!isValid)
                {
                    PopulateRSI(false,
                        HttpStatusCode.BadGateway,
                        "Enter employee data in valid format",
                        null);
                }
                return isValid;
            }
            return false;
        }


        /// <summary>
        /// Method to convert DTOEMP01 instance to EMP01 instance
        /// </summary>
        /// <param name="objDTOEMP01">Instance of DTOEMP01</param>
        public void Presave(DTOUMP objUSREMP, EnmOperation operation)
        {
            _objEMP01 = objUSREMP.ConvertModel<EMP01>();

            DTOUSR01 objDTOUSR01 = objUSREMP.ConvertModel<DTOUSR01>();

            _objBLUser.Presave(objDTOUSR01, operation);

            DateTime currentDateTime = DateTime.Now;

            _objEMP01.p01f08 = currentDateTime;

            if (operation == EnmOperation.A)
            {
                _objEMP01.p01f07 = currentDateTime;
            }
        }

        /// <summary>
        /// Method to validate the EMP01 instance
        /// </summary>
        /// <returns>True if EMP01 instance is valid else false</returns>
        public bool Validate()
        {
            // validation for _objUSR01, username already exists
            return _objBLUser.Validate();
        }

        /// <summary>
        /// Method to create or update a record of emp01 table in DB
        /// </summary>
        public void Save(EnmOperation operation)
        {
            if(operation == EnmOperation.A)
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    _objBLUser.Save(EnmOperation.A, out int newUserId);

                    // create employee
                    int employeeId = (int)db.Insert<EMP01>(_objEMP01, selectIdentity: true);

                    // create user - employee
                    UMP02 objUMP02 = new UMP02()
                    {
                        p02f02 = newUserId,
                        p02f03 = employeeId,
                        p02f04 = _objEMP01.p01f07,
                        p02f05 = _objEMP01.p01f08
                    };
                    db.Insert(objUMP02);
                }
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    Dictionary<string, object> dictToUpdate = GetDictionary(_objEMP01);
                    db.UpdateOnly<EMP01>(dictToUpdate, e => e.t01f01 == _objEMP01.t01f01);

                    PopulateRSI(
                        true,
                        HttpStatusCode.OK,
                        "Data updated successfully",
                        null
                    );
                }
            }
        }

        public int FetchEmployeeIdByUserId(int userId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    int employeeId = (int)db.Select<UMP02>(ue => ue.p02f02 == userId)
                            .Select(ue => ue.p02f03)
                            .SingleOrDefault();
                    return employeeId;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
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