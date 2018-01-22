using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class TrackRoute : Attribute
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
