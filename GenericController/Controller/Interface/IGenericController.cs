using System.Threading.Tasks;

namespace GenericController.Controller.Interface
{
    public interface IGenericController<T, TRequest, TResponse> where T : class where TRequest : class where TResponse : class
    {
        Task<TResponse> Get(int key);

        Task<string> Create(TRequest request);

    }
}
