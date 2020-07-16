using GenericController.Controller.Implementation;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;

namespace GenericController.ControllerFactory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericRestControllerNameConvention : Attribute, IControllerModelConvention
    {
        private Type GenericController { get; set; }
        public GenericRestControllerNameConvention(Type _genericController)
        {
            this.GenericController = _genericController;
        }
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType || controller.ControllerType.GetGenericTypeDefinition() != GenericController)
            {
                return;
            }
            var entityType = controller.ControllerType.GenericTypeArguments[0];
            controller.ControllerName = entityType.Name;
            controller.RouteValues["Controller"] = entityType.Name;
        }
    }
}