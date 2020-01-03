using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Validator.Notification;

namespace Arquitetura.Services.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IRepository<User> repository, INotification notification) : base(repository, notification)
        {

        }
    }
}
