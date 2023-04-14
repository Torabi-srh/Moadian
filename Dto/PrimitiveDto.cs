using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    public abstract class PrimitiveDto
    {
        public Dictionary<string, object> ToArray()
        {
            var reflection = GetType();
            var properties = reflection.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            var array = new Dictionary<string, object>();

            foreach (var property in properties)
            {
                if (property.GetValue(this) != null)
                {
                    var value = property.GetValue(this);

                    if (value is string || value is int || value is null || value is decimal || value is bool)
                    {
                        array[property.Name] = value;
                    }
                    else
                    {
                        array[property.Name] = ((PrimitiveDto)value).ToArray();
                    }
                }
            }

            return array;
        }
    }

}
