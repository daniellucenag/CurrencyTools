using MediatR;
using System.Threading.Tasks;

namespace Infrastructure.Publisher
{
    public interface IDomainEventHandler
    {
        void AddDomainEvent(params INotification[] notifications);
        Task Handler();
    }
}
