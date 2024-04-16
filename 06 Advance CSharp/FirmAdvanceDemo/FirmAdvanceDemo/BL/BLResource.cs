using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace FirmAdvanceDemo.BL
{

    /// <summary>
    /// Business logic class for Resource - defines all props and methods to support all that are controller
    /// </summary>
    /// <typeparam name="RSE01"></typeparam>
    public class BLResource<RSE01> where RSE01 : IModel
    {
        /// <summary>
        /// Ormlite Connection Factory instance from Connection class - that represent a connection with a particular database
        /// </summary>
        protected readonly OrmLiteConnectionFactory _dbFactory;

        internal readonly Response _statusInfo;

        public BLResource()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }

        public BLResource(Response statusInfo) : this()
        {
            _statusInfo = statusInfo;
        }

        /// <summary>
        /// Method used to fetch the all Resource of RSE01 class
        /// </summary>
        /// <returns>ResponseStatusInfo instance containing list_of_resource (of RSE01 class) if successful or null if any exception</returns>
        public Response RetrieveResource()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    List<RSE01> lstResource = db.Select<RSE01>();
                    return new Response()
                    {
                        IsError = true,
                        Message = $"Existing {db.GetQuotedTableName<RSE01>()}s",
                        Data = new { Resources = lstResource }
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        IsError = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }

        /// <summary>
        /// Method to fetch a resource based on resource id
        /// </summary>
        /// <param name="ResourceId">resource id</param>
        /// <returns>ResponseStatusInfo instance containing resource of RSE01 type if successful or null if any exception</returns>
        public Response FetchResource(int ResourceId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    RSE01 Resource = db.SingleById<RSE01>(ResourceId);
                    return new Response()
                    {
                        IsError = true,
                        Message = Resource == null ? $"No Resource found for id {ResourceId}" : $"{db.GetQuotedTableName<RSE01>()} Details",
                        Data = new { Resources = Resource }
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        IsError = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }


        /// <summary>
        /// Method to add a resource of RSE01 type to database
        /// </summary>
        /// <param name="Resource">An instance of RSE01 type</param>
        /// <returns>ResponseStatusInfo instance containing ResourceId if successful or null if any exception</returns>
        public Response AddResource(RSE01 Resource)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    long ResourceId = db.Insert<RSE01>(Resource, selectIdentity: true);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"{db.GetQuotedTableName<RSE01>()} created successfully",
                        Data = new { Id = ResourceId }
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        IsError = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }

        /// <summary>
        /// Method to update an resource on database using resoure id an part_to_update
        /// </summary>
        /// <param name="ResourceId">Resource id</param>
        /// <param name="toUpdateJson">Json object that contains part to update</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public Response UpdateResource(int ResourceId, JObject toUpdateJson)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    Dictionary<string, object> toUpdateDictionary = toUpdateJson.ToObject<Dictionary<string, object>>();

                    db.UpdateOnly<RSE01>(updateFields: toUpdateDictionary, obj: p => p.t01f01 == ResourceId);

                    return new Response()
                    {
                        IsError = true,
                        Message = $"{db.GetQuotedTableName<RSE01>()} with id {ResourceId} upadted",
                        Data = null
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        IsError = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }

        /// <summary>
        /// Method to delete a resource (of RSE01 type) from datatbase
        /// </summary>
        /// <param name="ResourceId">Resource id</param>
        /// <returns>ResponseStatusInfo instance containing null</returns>
        public Response RemoveResource(int ResourceId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.DeleteById<RSE01>(ResourceId);
                    return new Response()
                    {
                        IsError = true,
                        Message = $"{db.GetQuotedTableName<RSE01>()} deleted with id: {ResourceId}",
                        Data = null
                    };
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        IsError = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
        }

        /// <summary>
        /// Method to populate data into _statusInfo object
        /// </summary>
        /// <param name="isSuccess">Response status isSuccess or not</param>
        /// <param name="statusCode">Http status code of request</param>
        /// <param name="message">Response message</param>
        /// <param name="data">Data of response</param>
        public void PopulateRSI(bool isSuccess, HttpStatusCode statusCode, string message, object data)
        {
            if(_statusInfo != null)
            {
                _statusInfo.IsAlreadySet = true;
                _statusInfo.IsError = isSuccess;
                _statusInfo.HttpStatusCode = statusCode;
                _statusInfo.Message = message;
                _statusInfo.Data = data;
            }
        }
    }
}