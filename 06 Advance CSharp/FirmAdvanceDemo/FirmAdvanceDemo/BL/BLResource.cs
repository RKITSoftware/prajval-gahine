using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirmAdvanceDemo.BL
{
    public class BLResource<RSE01> where RSE01 : IModel
    {
        protected static readonly OrmLiteConnectionFactory _dbFactory;

        static BLResource()
        {
            _dbFactory = Connection.dbFactory;
        }

        public static ResponseStatusInfo FetchResource()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    List<RSE01> lstResource = db.Select<RSE01>();
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Existing {db.GetQuotedTableName<RSE01>()}s",
                        Data = new { Resources = lstResource }
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


        public static ResponseStatusInfo FetchResource(int ResourceId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    RSE01 Resource = db.SingleById<RSE01>(ResourceId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = Resource == null ? $"No Resource found for id {ResourceId}" : $"{db.GetQuotedTableName<RSE01>()} Details",
                        Data = new { Resources = Resource }
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



        public static ResponseStatusInfo AddResource(RSE01 Resource)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    long ResourceId = db.Insert<RSE01>(Resource, selectIdentity: true);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"{db.GetQuotedTableName<RSE01>()} created successfully",
                        Data = new { Id = ResourceId }
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


        public static ResponseStatusInfo UpdateResource(int ResourceId, JObject toUpdateJson)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    Dictionary<string, object> toUpdateDictionary = toUpdateJson.ToObject<Dictionary<string, object>>();

                    db.UpdateOnly<RSE01>(updateFields: toUpdateDictionary, obj: p => p.Id == ResourceId);

                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"{db.GetQuotedTableName<RSE01>()} with id {ResourceId} upadted",
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


        public static ResponseStatusInfo RemoveResource(int ResourceId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.DeleteById<RSE01>(ResourceId);
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"{db.GetQuotedTableName<RSE01>()} deleted with id: {ResourceId}",
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