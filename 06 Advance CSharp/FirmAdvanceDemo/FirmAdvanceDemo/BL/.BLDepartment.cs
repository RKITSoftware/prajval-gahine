using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace FirmAdvanceDemo.BL
{
    public class BLDepartment
    {
        private static readonly OrmLiteConnectionFactory _dbFactory;
        static BLDepartment()
        {
            _dbFactory = Connection.dbFactory;
        }

        public static ResponseStatusInfo GetDepartments()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    List<DPT01> lstDepartment = db.Select<DPT01>();
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = "Existing Departments",
                        Data = new { Departments = lstDepartment }
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }

        public static ResponseStatusInfo GetDepartment(int DepartmentId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    DPT01 Department = db.SingleById<DPT01>(DepartmentId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = Department == null ? $"No Department found for id {DepartmentId}" : "Department Details",
                        Data = new { Departments = Department }
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }

        public static ResponseStatusInfo AddDepartment(DPT01 Department)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    long DepartmentId = db.Insert<DPT01>(Department, selectIdentity: true);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = "Department created successfully",
                        Data = new { DepartmentId = DepartmentId }
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }


        public static ResponseStatusInfo RemoveDepartment(int DepartmentId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.DeleteById<DPT01>(DepartmentId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Department deleted with DepartmentId: {DepartmentId}",
                        Data = null
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }
    }
}