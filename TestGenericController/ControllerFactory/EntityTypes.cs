using System;
using System.Collections.Generic;
using System.Reflection;
using Data.entities;
using TestGenericController.Entities;

namespace TestGenericController.ControllerFactory
{
    public static class EntityTypes
    {
        public static Dictionary<TypeInfo, Type[]> model_types()
        {
            return new Dictionary<TypeInfo, Type[]>()
            {
                { typeof(Albums).GetTypeInfo(), new Type[] { typeof(Albums) ,typeof(AlbumRequest).GetTypeInfo(), typeof(AlbumResponse).GetTypeInfo() } }
            };
        }
    }
}
