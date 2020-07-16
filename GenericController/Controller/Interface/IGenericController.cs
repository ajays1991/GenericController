using System.Threading.Tasks;

namespace GenericController.Controller.Interface
{
    public interface IGenericController<T, TRequest, TResponse>
    {
        Task<TResponse> Get(int key);

        Task<TResponse> Create(TRequest request);

    }
}
