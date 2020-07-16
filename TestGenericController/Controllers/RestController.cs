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
