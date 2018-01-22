using CourseAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Extensions
{
    public static class EnumExtensions
    {
        public static TrackRoute GetTrackRoute<T>(this T e) where T : IConvertible
        {
            TrackRoute trackRoute = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var Attributes = memInfo[0].GetCustomAttributes(typeof(TrackRoute), false);
                        if (Attributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            trackRoute = ((TrackRoute)Attributes[0]);
                        }

                        break;
                    }
                }
            }

            return trackRoute;
        }
    }
}
