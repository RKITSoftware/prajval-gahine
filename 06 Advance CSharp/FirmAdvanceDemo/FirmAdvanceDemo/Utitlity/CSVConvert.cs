using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FirmAdvanceDemo.Utitlity
{
    public class CSVConvert<T>
    {
        public static string ConvertToCSV(List<T> lstResource, Type resourceType)
        {
            string csvContent = string.Empty;


            // get resource's all public and instance props
            PropertyInfo[] props = resourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // add those props Name as csv header
            List<string> lstCsvHeader = props.Select(prop => prop.Name).ToList<string>();
            string csvHeaders = string.Join(",", lstCsvHeader) + "\n";
            string csvBody = string.Empty;

            // create a memory stream
            List<string> lstRowData = null;
            lstResource.ForEach(resource =>
            {
                lstRowData = new List<string>(props.Length);
                foreach (PropertyInfo prop in props)
                {
                    string data = null;
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        data = ((DateTime)prop.GetValue(resource, null)).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        data = prop.GetValue(resource, null).ToString();

                    }
                    lstRowData.Add(data);
                }
                string row = string.Join(",", lstRowData) + "\n";
                csvBody += row;
            });

            return $"{csvHeaders}{csvBody}";
        }
    }
}