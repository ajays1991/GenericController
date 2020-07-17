using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Collections.Generic;
using GenericController.Controller.Implementation;

namespace GenericController.ControllerFactory
{
    public class GenericRestControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly Dictionary<TypeInfo, Type[]> EntityTypes;
        private readonly Type GenericController;
        public GenericRestControllerFeatureProvider(Dictionary<TypeInfo, Type[]> _entityTypes, Type _genericController)
        {
            this.EntityTypes = _entityTypes;
            this.GenericController = _genericController;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var model_type in EntityTypes)
            {
                var controller_type = GenericController.MakeGenericType(model_type.Value).GetTypeInfo();
                feature.Controllers.Add(controller_type);

            }
        }
    }
}
