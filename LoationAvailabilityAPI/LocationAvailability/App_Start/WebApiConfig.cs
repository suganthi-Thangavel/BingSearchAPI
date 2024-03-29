﻿using LocationAvailability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;

namespace LocationAvailability
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var container = new UnityContainer();

            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IDbContext, AppDbcontext>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
