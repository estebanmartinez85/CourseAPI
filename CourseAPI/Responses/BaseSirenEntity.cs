using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CourseAPI.Responses
{
    public abstract class BaseSirenEntity : EntityBuilder, ISubEntityBuilder
    {
        protected string GetBaseURL()
        {
            return GlobalConstants.URL;
        }
    }
}
