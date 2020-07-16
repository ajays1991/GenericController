using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenericController.Controller.Interface;
using GenericController.ControllerFactory;

namespace GenericController.Controller.Implementation
{
    public class GenericController<T, TRequest, TResponse> : ControllerBase, IGenericController<T, TRequest, TResponse> where T : class where TRequest : class where TResponse : class
    {

        public async Task<TResponse> Get(int key)
        {
            return default;
        }

        public async Task<string> Create(TRequest request)
        {
            return "default";
        }
    }
}