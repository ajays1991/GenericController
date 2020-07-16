using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GenericController.Controller.Interface;
using AutoMapper;
using GenericController.ControllerFactory;

using Data.Repositories;

namespace TestGenericController.Controllers
{
    [ApiController]
    [GenericRestControllerNameConvention(typeof(RestController<,,>))]
    [Route("api/[controller]")]
    public class RestController<T, TRequest, TResponse> : IGenericController<T, TRequest, TResponse> where T : class where TRequest : class where TResponse : class
    {
        private IGenericRepository<T> Repository { get; set; }

        private IMapper Mapper { get; set; }


        public RestController(IGenericRepository<T> _repository, IMapper _mapper)
        {
            Repository = _repository;
            Mapper = _mapper;
        }

        [HttpGet]
        public async Task<TResponse> Get(int key)
        {
            var result = await Repository.Get(key);
            return Mapper.Map<T, TResponse>(result);
        }

        [HttpPost]
        public async Task<string> Create(TRequest request)
        {
            T db_model = Mapper.Map<TRequest, T>(request);
            var result = await Repository.Add(db_model);
            Mapper.Map<T, TResponse>(result);
            return "the hero";

        }
        
    }
}
