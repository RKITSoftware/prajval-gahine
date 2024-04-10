using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TempConvPocoDto
{
    public class ModelConversion<POCO, DTO> 
    {
        public static DTO ToDTO(POCO poco)
        {
            PropertyInfo[] pocoProps = typeof(POCO).GetProperties()
                .Where(prop => prop.IsDefined(typeof(JsonPropertyNameAttribute), false))
                .ToArray();

            Type dtoType = typeof(DTO);
            DTO dto = (DTO)Activator.CreateInstance(dtoType);

            foreach (PropertyInfo pocoProp in pocoProps)
            {
                JsonPropertyNameAttribute nameAttr = pocoProp.GetCustomAttribute(typeof(JsonPropertyNameAttribute), false) as JsonPropertyNameAttribute;
                string name = nameAttr.Name;

                PropertyInfo propDto = dtoType.GetProperty(name);
                propDto.SetValue(dto, pocoProp.GetValue(poco));
            }

            return dto;
        }

        public static POCO ToPOCO(DTO dto)
        {
            PropertyInfo[] dtoProps = typeof(DTO).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToArray();

            Type PocoType = typeof(POCO);
            POCO poco = (POCO)Activator.CreateInstance(PocoType);

            foreach (PropertyInfo dtoProp in dtoProps)
            {
                string name = dtoProp.Name;
                name = name.Remove(3, 1)
                    .Insert(3, "f");

                PropertyInfo propPoco = PocoType.GetProperty(name);
                propPoco.SetValue(poco, dtoProp.GetValue(dto));
            }

            return poco;
        }
    }
}
