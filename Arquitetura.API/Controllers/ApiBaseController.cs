using Arquitetura.Services.Validator.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Arquitetura.API.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly INotification _notification;

        protected ApiBaseController(INotification notification)
        {
            _notification = notification;
        }

        protected bool ValidOperation()
        {
            return !_notification.HasNotification();
        }
    }
}