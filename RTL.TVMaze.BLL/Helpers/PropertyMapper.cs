using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RTL.TVMaze.BLL.Helpers
{
    public class PropertyMapper
    {
        public static TargetType Map<SourceType, TargetType>(SourceType source, TargetType target, bool ignoreNull = true)
        {
            foreach (PropertyInfo sourceProp in source.GetType().GetProperties())
            {
                PropertyInfo targetProp = target.GetType().GetProperties().Where(p => p.Name == sourceProp.Name).FirstOrDefault();
                if (targetProp != null && targetProp.GetType().Name.Equals(sourceProp.GetType().Name))
                {
                    if (!targetProp.PropertyType.Name.Equals(sourceProp.PropertyType.Name)) { continue; }

                    var value = sourceProp.GetValue(source);

                    if (ignoreNull)
                    {
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            targetProp.SetValue(target, value);
                        }
                    }
                    else
                    {
                        targetProp.SetValue(target, value);
                    }
                }
            }
            return target;
        }
    }
}
