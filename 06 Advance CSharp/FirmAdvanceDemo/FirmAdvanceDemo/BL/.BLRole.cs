using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FirmAdvanceDemo.Models;
using ServiceStack.OrmLite;
using FirmAdvanceDemo.Utitlity;
using System.Web.Security;

namespace FirmAdvanceDemo.BL
{
    
    public class BLRole
    {
        private static readonly OrmLiteConnectionFactory _dbFactory;
        static BLRole()
        {
            _dbFactory = Connection.dbFactory;
        }

        public static ResponseStatusInfo GetRoles()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    List<RLE01> lstRole = db.Select<RLE01>();
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = "Existing roles",
                        Data = new { Roles = lstRole }
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

        public static ResponseStatusInfo GetRole(int roleId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    RLE01 role = db.SingleById<RLE01>(roleId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = role == null ? $"No role found for id {roleId}" : "Role Details",
                        Data = new { Roles = role }
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

        public static ResponseStatusInfo AddRole(RLE01 role)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    long roleId = db.Insert<RLE01>(role, selectIdentity: true);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = "Role created successfully",
                        Data = new { RoleId = roleId }
                    };
                }
                catch(Exception ex)
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


        public static ResponseStatusInfo RemoveRole(int RoleId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.DeleteById<RLE01>(RoleId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Role deleted with RoleId: {RoleId}",
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