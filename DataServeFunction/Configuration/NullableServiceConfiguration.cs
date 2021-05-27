using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataServeFunction.Configuration
{
    public class NullableServiceConfiguration
    {
        public string? StorageAccountConnectionString { get; set; }
       
        public ServiceConfiguration Validate()
        {
            foreach (PropertyInfo pi in GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(this);
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ServiceConfigurationException($"AppSetting http:{pi.Name} is not set");

                }
            }

            return new ServiceConfiguration(StorageAccountConnectionString);
        }
    }
}
