using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenericController.Controller.Interface;
using GenericController.ControllerFactory;

namespace GenericController.Controller.Implementation
{
    [ApiController]
    [GenericRestControllerNameConvention]
    [Route("api/[controller]")]
    public class GenericController<T, TRequest, TResponse> : ControllerBase, IGenericController<T, TRequest, TResponse>
    {
        public GenericController()
        {

        }

        [HttpGet]
        public async Task<TResponse> Get(int key)
        {
            return default;
        }


        [HttpPost]
        public async Task<TResponse> Create(TRequest request)
        {
            return default;
        }
    }
}