using System.Threading.Tasks;

namespace FUTAutoBuyer.Requests
{
    public interface IFUTRequest<TResponse>
    {
        Task<TResponse> PerformRequestAsync();
    }
}
