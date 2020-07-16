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
        private readonly Dictionary<TypeInfo, List<TypeInfo>> EntityTypes;
        public GenericRestControllerFeatureProvider(Dictionary<TypeInfo, List<TypeInfo>> _entityTypes)
        {
            this.EntityTypes = _entityTypes;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var model_type in EntityTypes)
            {
                var entity_type = model_type.Key;
                var entity_request_types = model_type.Value[0];
                Type[] typeArgs = { entity_type, model_type.Value[0], model_type.Value[1] };
                var controller_type = typeof(GenericController<,,>).MakeGenericType(typeArgs).GetTypeInfo();
                feature.Controllers.Add(controller_type);
            }
        }
    }
}
