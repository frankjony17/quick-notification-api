using Quick.Notify.Domain.Notify.Commands.Requests;
using Quick.Notify.Domain.Notify.Commands.Responses;
using System.Threading.Tasks;

namespace Quick.Notify.Domain.Notify.Services
{
    public interface INotifyService
    {
        NotifyCommandResponse Process(NotifyCommandRequest notifyCommandRequest);
    }
}
