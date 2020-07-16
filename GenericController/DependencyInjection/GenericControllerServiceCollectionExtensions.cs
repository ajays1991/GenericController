using GenericController.ControllerFactory;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerGenServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericController(
            this IServiceCollection services,
            Dictionary<TypeInfo, List<TypeInfo>> entities = null)
        {
            var mvcBuilder = services.AddMvcCore();
            mvcBuilder.AddMvcOptions(o => o.Conventions.Add(new GenericRestControllerNameConvention()));
            mvcBuilder.ConfigureApplicationPartManager(c =>
            {
                c.FeatureProviders.Add(new GenericRestControllerFeatureProvider(entities));
            });
            return services;
        }
    }
}
