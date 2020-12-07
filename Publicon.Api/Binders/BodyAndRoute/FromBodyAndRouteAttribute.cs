using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Publicon.API.Binders.BodyandRoute
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FromBodyAndRouteAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BodyAndRouteBindingSource.BodyAndRoute;
    }
}
