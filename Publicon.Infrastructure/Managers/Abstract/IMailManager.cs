using System.Threading.Tasks;

namespace Publicon.Infrastructure.Managers.Abstract
{
    public interface IMailManager : IManager
    {
        Task SendMailAsync(string receiver, string subject, string message);
    }
}
