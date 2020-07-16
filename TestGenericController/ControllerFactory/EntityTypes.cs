using System.Collections.Generic;
using System.Reflection;
using Data.entities;
using TestGenericController.Entities;

namespace TestGenericController.ControllerFactory
{
    public static class EntityTypes
    {
        public static Dictionary<TypeInfo, List<TypeInfo>> model_types()
        {
            return new Dictionary<TypeInfo, List<TypeInfo>>() {
                { typeof(Albums).GetTypeInfo(), new List<TypeInfo>() { typeof(AlbumRequest).GetTypeInfo(), typeof(AlbumResponse).GetTypeInfo() } }
            };
        }
    }
}
