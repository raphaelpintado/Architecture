using Arquitetura.Services.Validator.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Arquitetura.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotification _notification;

        protected MainController(INotification notification)
        {
            _notification = notification;
        }

        protected bool ValidOperation()
        {
            return !_notification.HasNotification();
        }
    }
}