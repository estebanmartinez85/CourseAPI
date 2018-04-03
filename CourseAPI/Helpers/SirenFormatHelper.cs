using CourseAPI.Responses;
using FluentSiren.Builders;
using FluentSiren.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CourseAPI.Helpers
{
    public static class SirenFormatHelper
    {
        public static string EntityToJson(this EntityBuilder entity)
        {
             return JsonConvert.SerializeObject(entity.Build(), Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
