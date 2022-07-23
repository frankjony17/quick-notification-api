using Quick.Notification.Domain.Notify.Commands.Requests;
using Quick.Notification.Domain.Notify.Commands.Responses;
using System.Threading.Tasks;

namespace Quick.Notification.Domain.Notify.Services
{
    public interface INotifyService
    {
        NotifyCommandResponse Process(NotifyCommandRequest notifyCommandRequest);
    }
}
