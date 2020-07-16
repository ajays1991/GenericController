# Generic Controller ApplicationFeatureProvider

## Alpha

This library is in Alpha. We will make an effort to support the library, but we reserve the right to make incompatible
changes when necessary.

## Current Version is 1.0.0

## Installation

Click Manage Nuget Packages on the project dependencies

This package is registered with IServiceCollection and injected into container using Microsoft.Extensions.DependencyInjection;

### Basic working information

This package takes in Dictionary class where each ModelType is mapped to its corresponding Request and Response models

Create an static EntityTypes class in your project with method model_types which returns Dictionary<TypeInfo, List<TypeInfo>>. I am showing an example  where i have two database entities name Album and Artist and where each model type points to its own Request and Response Types.
```C#

using System.Collections.Generic;
using System.Reflection;
using Data.entities;
using TestGenericController.Entities;

namespace YourNameSpace
{
    public static class EntityTypes
    {
        public static Dictionary<TypeInfo, List<TypeInfo>> model_types()
        {
            return new Dictionary<TypeInfo, List<TypeInfo>>() {
                { typeof(Albums).GetTypeInfo(), new List<TypeInfo>() { typeof(AlbumRequest).GetTypeInfo(), typeof(AlbumResponse).GetTypeInfo() } },
                { typeof(Artists).GetTypeInfo(), new List<TypeInfo>() { typeof(ArtistRequest).GetTypeInfo(), typeof(ArtistResponse).GetTypeInfo() } }
            };
        }
    }
}

```

### Now we will add GenericController service container in the ConfigureService method and pass our entity types static dictionary. This will create controllers for all our models i.e it will add AlbumsController and ArtistsController.

```C#

 public void ConfigureServices(IServiceCollection services)
{
    services.AddGenericController(TestGenericController.ControllerFactory.EntityTypes.model_types());
}

```

### RestController Implementation.

Now we implement RestController(open type generic controller) which implement IGenericController from GenericController.Controller.Interface.
Here i am sharing my example where i am implementing my create, get method and on run time T, TRequest, TResponse will be resolved to proper types, like if you hit api/albums url T will be Albums, TRequest will be AlbumsRequest, TResponse will be AlbumsResponse.

I have used automapper for transation and Generic Rep
You can use/contruct/implement any datacontext strategy and your own authentication and authorization strategies and decorators.

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GenericController.Controller.Interface;
using AutoMapper;

using Data.Repositories;

namespace TestGenericController.Controllers
{
    public class RestController<T, TRequest, TResponse> : IGenericController<T, TRequest, TResponse> where T : class where TRequest : class where TResponse : class
    {
        private IGenericRepository<T> Repository { get; set; }

        private IMapper Mapper { get; set; }

        public RestController(IGenericRepository<T> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        public async Task<TResponse> Get(int key)
        {
            var result = await Repository.Get(key);
            return Mapper.Map<T, TResponse>(result);
        }

        public async Task<TResponse> Create(TRequest request)
        {
            T db_model = Mapper.Map<TRequest, T>(request);
            var result = await Repository.Add(db_model);
            return Mapper.Map<T, TResponse>(result);
        }
        
    }
}


```

This Repository have a project TestGenericController for fully working solution using this Package.


### To Do
 Make GenericController to take dynamic number of types so that like if you want to implement Mediator it between persistence layer it should be flexible enough to take random generic types, right now it take only 3 types(T, TRequest, TResponse).

## Support

Please [report bugs at the project on Github](https://github.com/ajays1991/GenericController/issues). Please don't hesistate to ask questions as issues in the repository. I am open to hear suggestions to improve this furthur and in version 2.0.0 version i am planning to rollout my first  Todo.