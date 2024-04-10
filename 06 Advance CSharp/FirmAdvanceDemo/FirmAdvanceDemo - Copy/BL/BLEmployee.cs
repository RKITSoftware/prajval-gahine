using FirmAdvanceDemo.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using System.Linq;
using System;

namespace FirmAdvanceDemo.BL
{
    public class BLEmployee : BLResource<EMP01>
    {
        public static int FetchEmployeeIdByUserId(int userId)
        {
            try
            {
                using(IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    int employeeId = db.Select<USR01EMP01>(ue => ue.p01f01 == userId)
                            .Select(ue => ue.p01f02)
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