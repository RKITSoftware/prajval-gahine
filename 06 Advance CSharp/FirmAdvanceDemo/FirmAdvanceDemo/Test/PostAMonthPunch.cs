using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Test
{
    public class PostAMonthPunch
    {
        public static void Run()
        {
            // 1st jan to 31st jan
            DateTime inMorning = new DateTime(2022, 4, 1, 9, 30, 0);
            DateTime outAfternoon = new DateTime(2023, 1, 1, 13, 30, 0);
            DateTime inAfternoon = new DateTime(2022, 4, 1, 14, 30, 0);
            DateTime outEvening = new DateTime(2022, 4, 1, 18, 30, 0);

            DateTime endDate = DateTime.Now;
            Random random = new Random();
            int prob;

            while(inMorning <= endDate)
            {
                if (inMorning.DayOfWeek != DayOfWeek.Saturday && inMorning.DayOfWeek != DayOfWeek.Sunday)
                {
                    List<PCH01> lstPunch = new List<PCH01>();

                    prob = random.Next(100);
                    if(prob > 7)
                    {
                        // punch in morning
                        for (int j = 1; j <= 3; j++)
                        {
                            lstPunch.Add(new PCH01
                            {
                                H01F02 = j,
                                H01F03 = inMorning,
                                H01F04 = Enums.EnmPunchType.U,
                                H01F05 = false,
                                H01F06 = inMorning
                            });
                        }
                    }

                    prob = random.Next();
                    if(prob > 7)
                    {
                        // punch out afternoon
                        for (int j = 1; j <= 3; j++)
                        {
                            lstPunch.Add(new PCH01
                            {
                                H01F02 = j,
                                H01F03 = outAfternoon,
                                H01F04 = Enums.EnmPunchType.U,
                                H01F05 = false,
                                H01F06 = outAfternoon
                            });
                        }
                    }

                    prob = random.Next();
                    if(prob > 7)
                    {
                        // punch in afternoon
                        for (int j = 1; j <= 3; j++)
                        {
                            lstPunch.Add(new PCH01
                            {
                                H01F02 = j,
                                H01F03 = inAfternoon,
                                H01F04 = Enums.EnmPunchType.U,
                                H01F05 = false,
                                H01F06 = inAfternoon
                            });
                        }
                    }

                    prob = random.Next();
                    if(prob > 7)
                    {
                        // punch out evening
                        for (int j = 1; j <= 3; j++)
                        {
                            lstPunch.Add(new PCH01
                            {
                                H01F02 = j,
                                H01F03 = outEvening,
                                H01F04 = Enums.EnmPunchType.U,
                                H01F05 = false,
                                H01F06 = outEvening
                            });
                        }
                    }

                    lstPunch = lstPunch.OrderBy(punch => punch.H01F02)
                        .ThenBy(punch => punch.H01F03)
                        .ToList();

                    BLPCH01Handler handler = new BLPCH01Handler();
                    handler._punchProcessingData.LstAllPunch = lstPunch;
                    handler.ProcessUnprocessedPunches();
                    OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
                    IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=127.0.0.1;uid=Admin;pwd=gs@123;database=firmadvance378");

                    lstPunch = lstPunch.OrderBy(punch => punch.H01F03).ToList();

                    using (IDbConnection db = dbFactory.OpenDbConnection())
                    {
                        db.InsertAll<PCH01>(lstPunch);
                    }

                }
                inMorning = inMorning.AddDays(1);
                outAfternoon = outAfternoon.AddDays(1);
                inAfternoon = inAfternoon.AddDays(1);
                outEvening = outEvening.AddDays(1);
            }
        }


        public static void Run2()
        {
            // 1st jan to 31st jan
            DateTime inMorning = new DateTime(2022, 1, 1, 9, 30, 0);
            DateTime outAfternoon = new DateTime(2022, 1, 1, 13, 30, 0);
            DateTime inAfternoon = new DateTime(2022, 1, 1, 14, 30, 0);
            DateTime outEvening = new DateTime(2022, 1, 1, 18, 30, 0);

            DateTime endDate = DateTime.Now;
            Random random = new Random();

            int[] willGoOut = new int[4];

            int[] isOnLeave = new int[4];

            while (inMorning <= endDate)
            {
                if (inMorning.DayOfWeek != DayOfWeek.Saturday && inMorning.DayOfWeek != DayOfWeek.Sunday)
                {
                    List<PCH01> lstPunch = new List<PCH01>();

                    isOnLeave[1] = random.Next(100);
                    isOnLeave[2] = random.Next(100);
                    isOnLeave[3] = random.Next(100);

                    willGoOut[1] = random.Next(100);
                    willGoOut[2] = random.Next(100);
                    willGoOut[3] = random.Next(100);

                    // punch in morning
                    for (int j = 1; j <= 3; j++)
                    {
                        if (isOnLeave[j] < 5) continue;
                        lstPunch.Add(new PCH01
                        {
                            H01F02 = j,
                            H01F03 = inMorning,
                            H01F04 = Enums.EnmPunchType.U,
                            H01F05 = false,
                            H01F06 = inMorning
                        });
                    }

                    
                    // punch out afternoon
                    for (int j = 1; j <= 3; j++)
                    {
                        if (isOnLeave[j] < 5) continue;
                        if (willGoOut[j] < 10) continue;

                        lstPunch.Add(new PCH01
                        {
                            H01F02 = j,
                            H01F03 = outAfternoon,
                            H01F04 = Enums.EnmPunchType.U,
                            H01F05 = false,
                            H01F06 = outAfternoon
                        });
                    }


                    // punch in afternoon
                    for (int j = 1; j <= 3; j++)
                    {
                        if (isOnLeave[j] < 5) continue;
                        if (willGoOut[j] < 10) continue;

                        lstPunch.Add(new PCH01
                        {
                            H01F02 = j,
                            H01F03 = inAfternoon,
                            H01F04 = Enums.EnmPunchType.U,
                            H01F05 = false,
                            H01F06 = inAfternoon
                        });
                    }

                    // punch out evening
                    for (int j = 1; j <= 3; j++)
                    {
                        if (isOnLeave[j] < 5) continue;
                        lstPunch.Add(new PCH01
                        {
                            H01F02 = j,
                            H01F03 = outEvening,
                            H01F04 = Enums.EnmPunchType.U,
                            H01F05 = false,
                            H01F06 = outEvening
                        });
                    }

                    lstPunch = lstPunch.OrderBy(punch => punch.H01F02)
                        .ThenBy(punch => punch.H01F03)
                        .ToList();

                    BLPCH01Handler handler = new BLPCH01Handler();
                    handler._punchProcessingData.LstAllPunch = lstPunch;
                    handler.ProcessUnprocessedPunches();
                    OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
                    IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=127.0.0.1;uid=Admin;pwd=gs@123;database=firmadvance378");

                    lstPunch = lstPunch.OrderBy(punch => punch.H01F03).ToList();

                    using (IDbConnection db = dbFactory.OpenDbConnection())
                    {
                        db.InsertAll<PCH01>(lstPunch);
                    }

                }
                inMorning = inMorning.AddDays(1);
                outAfternoon = outAfternoon.AddDays(1);
                inAfternoon = inAfternoon.AddDays(1);
                outEvening = outEvening.AddDays(1);
            }
        }
    }
}