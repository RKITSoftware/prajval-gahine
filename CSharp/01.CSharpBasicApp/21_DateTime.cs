using System;
using System.Globalization;

namespace CSharpBasicApp
{
    /// <summary>
    /// MyClassDateTime demonstrate DateTime in C#
    /// </summary>
    internal class MyClassDateTime
    {
        static void Main()
        {
            DateTime dt = DateTime.Now;

            // date time formate specifier in placeholder syntax
            //Console.WriteLine("short date: {0:d}", dt);
            //Console.WriteLine("long date: {0:D}", dt);
            //Console.WriteLine("short full time: {0:f}", dt);
            //Console.WriteLine("long full time: {0:F}", dt);
            //Console.WriteLine("general date time: {0:g}", dt);
            //Console.WriteLine("general date time long time: {0:G}", dt);
            //Console.WriteLine("month day: {0:M}", dt); // or m
            //Console.WriteLine("short time: {0:t}", dt);
            //Console.WriteLine("long time: {0:T}", dt);
            //Console.WriteLine("month year: {0:y}", dt);  // or Y
            //Console.WriteLine("year: {0:yy}", dt);  // or Y
            //Console.WriteLine("year: {0:yyyy}", dt);  // or Y
            //Console.WriteLine("abbriviated week day: {0:ddd}", dt);
            //Console.WriteLine("full week day: {0:dddd}", dt);
            //Console.WriteLine("two digit of seconds fraction: {0:FF}", dt);
            //Console.WriteLine("24 hr format hour: {0:HH}", dt);
            //Console.WriteLine("Month as number: {0:MM}", dt);
            //Console.WriteLine("abbriviated name of the month: {0:MMM}", dt);
            //Console.WriteLine("full name of the month: {0:MMMM}", dt);
            //Console.WriteLine("seconds: {0:ss}", dt);

            //Console.WriteLine("Custom 12hr format time: {0:hh:mm:ss tt}", dt);
            //Console.WriteLine("Custom 24hr format time: {0:HH:mm:ss}", dt);

            //Console.WriteLine("Custome date: {0:dd-MM-yyyy}", dt);

            DateTime minValue = DateTime.MinValue;
            DateTime maxValue = DateTime.MaxValue;

            Console.WriteLine(DateTime.Today);
            Console.WriteLine(dt.DayOfWeek);
            Console.WriteLine(dt.DayOfYear);
            Console.WriteLine(dt.IsDaylightSavingTime());
            Console.WriteLine(dt.Kind);
            Console.WriteLine(dt.Ticks);

            // converting to universal time
            Console.WriteLine(dt.ToUniversalTime());
            // dt.ToLocalTime();

            // adding to datetime => TimeSpan type - represents a span of time
            // you can add ticks, milliseconds, seconds, minutes, hours, day, month, years
            // you can add a negative value
            dt = dt.AddHours(30);
            Console.WriteLine(dt);

            // the string represent our time span (4hr 15min 13sec and 12345milliseconds)
            TimeSpan ts = TimeSpan.Parse("4:15:13.12345");
            dt = dt.Add(ts);
            // -5:4:15:13.12345 => negative 5days 4hr 15min 13sec and 12345milliseconds

            // subtracting from a datetime => two forms i) dt.Subtract(datetime) ii) dt.Subtract(timespan)
            DateTime dt1 = DateTime.Now;
            TimeSpan ts1 = TimeSpan.Parse("6:14:12:10");
            DateTime dt2 = dt1.Add(ts1);

            Console.WriteLine("subtracting timespan " + dt2.Subtract(ts1));
            Console.WriteLine("subtracting datetime " + dt2.Subtract(dt1));

            // halfway of timespan
            Console.WriteLine("half waypoint of dt1 is: " + dt1.Add(ts1.Divide(2)));

            // you can go twice or triple or... as far using ts1.multiply

            // total - specifies the total amount of specific timequanity in the timespan
            Console.WriteLine("Minute part of timespan: " + ts1.Minutes);
            Console.WriteLine("Total Minute of timespan: " + ts1.TotalMinutes);

            // duration - specifies the absolute time diffrence
            Console.WriteLine("duration of the timespan: " + ts.Duration());

            // comparing two datetimes
            // -1 => dt1 is less than dt2
            // 1 => dt1 is greater than dt2
            // 0 => dt1 is same as dt2
            Console.WriteLine($"Comparing dt1 to dt2: {DateTime.Compare(dt1, dt2)}");

            Console.WriteLine();
            Console.WriteLine($"Short Date: {dt1.ToString("d")}");
            Console.WriteLine($"Long Date: {dt1.ToString("D")}");
            Console.WriteLine($"Short Time: {dt1.ToString("t")}");
            Console.WriteLine($"Long Time: {dt1.ToString("T")}");

            Console.WriteLine($"Round Trip DateTime: {dt1.ToString("O")}");

            Console.WriteLine($"Full Short DateTime: {dt1.ToString("f")}");
            Console.WriteLine($"Full Long DateTime: {dt1.ToString("F")}");
            Console.WriteLine($"General Short DateTime: {dt1.ToString("g")}");
            Console.WriteLine($"General Long DateTime: {dt1.ToString("G")}");

            Console.WriteLine($"Sortable DateTime: {dt1.ToString("s")}");
            Console.WriteLine($"universal DateTime: {dt1.ToString("U")}");

            Console.WriteLine($"Custom: {dt1.ToString("dd/MMM/yyyy hh:mm:ss tt zzz")}");

            // parsing a date string
            DateTime dt3 = DateTime.Parse("3/11/2023");
            Console.WriteLine($"dt3: {dt3.ToString("D")}");

            // means we are not caring with the culture, instead we are forcing it through in whatever format we decide is best.
            DateTime dt4 = DateTime.ParseExact("3/11/2023", "d/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine($"dt4 {dt4.ToString("D")}");


            // two new types - these two are not just portions of DateTime, but they are separate structures. New release from C#10 or .NET6
            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now);  // donot store anything about time
            TimeOnly timeOnly = TimeOnly.FromDateTime(DateTime.Now);  // donot store anything about date

        }
    }
}
