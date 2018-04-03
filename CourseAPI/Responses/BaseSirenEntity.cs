using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Data.Common.Models;
using Courses.Models;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CourseAPI.Responses
{
    public abstract class BaseSirenEntity : EntityBuilder, ISubEntityBuilder
    {
        protected Controller _controller;
        protected string _controllerName;

        public BaseSirenEntity(Controller controller)
        {
            _controller = controller;
            _controllerName = _controller.RouteData.Values["controller"].ToString();
        }
        protected string GetBaseURL()
        {
            return $"{_controller.Request.Scheme}://{_controller.Request.Host}/api/v1/";
        }
    }
}
