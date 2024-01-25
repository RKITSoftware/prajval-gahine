using FinalDemo.Connection;
using FinalDemo.Models;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace FinalDemo.Service
{
    public class BLApplicationsRepository
    {
    
        public List<app01> GetApplicationsForJob(int jobId)
        {
            using (IDbConnection db = Connections.dbFactory.OpenDbConnection())
            {
                return db.Select<app01>(x => x.p01f02 == jobId);
            }
        }

        public app01 GetApplicationById(int id)
        {
            using (IDbConnection db = Connections.dbFactory.OpenDbConnection())
            {
                return db.SingleById<app01>(id);
            }
        }

        public string SubmitJobApplication()
        {
            try
            {
                // Accessing form fields
                var appid = Convert.ToInt32(HttpContext.Current.Request.Form["p01f01"]);
                var jobid = Convert.ToInt32(HttpContext.Current.Request.Form["p01f02"]);
                var cadidatename = HttpContext.Current.Request.Form["p01f03"];

                // Accessing uploaded file
                var resumeFile = HttpContext.Current.Request.Files["p01f04"];
                var Coverletter = HttpContext.Current.Request.Files["p01f05"];



                if (resumeFile != null && resumeFile.ContentLength > 0)
                {
                    // Save the resume file to a specific location
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/");
                    var fileName1 = Guid.NewGuid().ToString() + Path.GetExtension(resumeFile.FileName);
                    var fileName2 = Guid.NewGuid().ToString() + Path.GetExtension(Coverletter.FileName);
                    var filePath = Path.Combine(uploadPath, fileName1);
                    resumeFile.SaveAs(filePath);
                    var filePath2 = Path.Combine(uploadPath, fileName2);
                    resumeFile.SaveAs(filePath2);

                    // Store data in the database using OrmLite
                    using (IDbConnection db = Connections.dbFactory.OpenDbConnection())
                    {
                        var resumeRecord = new app01
                        {
                            p01f01 = appid,
                            p01f02 = jobid,
                            p01f03 = cadidatename,
                            p01f04 = fileName1, // Store the file path in the database
                            p01f05 = fileName2
                        };

                        // Insert the record into the database
                        db.Insert(resumeRecord);
                    }

                    // Return a success message or any other response
                    return "Resume uploaded successfully.";
                }
                else
                {
                    // Handle the case where no file was uploaded
                    return "No file uploaded.";
                }
            }
            catch (Exception)
            {
                // Handle exceptions and return an appropriate response
                return "InternalServer error";
            }
        }
    }
}