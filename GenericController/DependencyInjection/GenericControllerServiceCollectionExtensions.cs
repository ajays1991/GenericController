using GenericController.ControllerFactory;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerGenServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericController(this IServiceCollection services, Dictionary<TypeInfo, Type[]> entities, Type genericController)
        {
            var mvcBuilder = services.AddMvcCore();
            mvcBuilder.AddMvcOptions(o => o.Conventions.Add(new GenericRestControllerNameConvention(genericController)));
            mvcBuilder.ConfigureApplicationPartManager(c =>
            {
                c.FeatureProviders.Add(new GenericRestControllerFeatureProvider(entities, genericController));
            });
            return services;
        }
    }
}
