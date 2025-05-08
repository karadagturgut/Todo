using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Todo.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IStringLocalizer<Lang> _localizer;
        private IToastNotification _notification;

        public BaseController(IStringLocalizer<Lang> localizer, IToastNotification notification)
        {
            _localizer = localizer;
            _notification = notification;
        }



        internal void UIInfo(string message, string title)
        {
            _notification.AddInfoToastMessage(message, new ToastrOptions
            {
                Title = title,
                CloseButton = true,
                IconClass = ""
            });
        }

        internal void UIInfo(string message)
        {
            UIInfo(message, _localizer["InfoTitle"].Value);

        }

        internal void UIWarning(string message, string title)
        {
            _notification.AddWarningToastMessage(message, new ToastrOptions
            {
                Title = title,
                CloseButton = true,
                IconClass = "",
            });
        }

        internal void UIWarning(string message)
        {
            UIWarning(message, _localizer["WarningTitle"].Value);
        }

        internal void UIError(string message, string title)
        {
            _notification.AddErrorToastMessage(message, new ToastrOptions
            {
                Title = title,
                CloseButton = true,
                IconClass = ""
            });
        }

        internal void UIError(string message)
        {
            UIError(message, _localizer["ErrorTitle"].Value);
        }

        internal void UIAlert(string message, string title)
        {
            _notification.AddAlertToastMessage(message, new ToastrOptions
            {
                Title = title,
                CloseButton = true,
                IconClass = ""
            });
        }

        internal void UIAlert(string message)
        {
            UIAlert(message, _localizer["AlertTitle"].Value);
        }

        internal void UISuccess(string message, string title)
        {
            _notification.AddSuccessToastMessage(message, new ToastrOptions
            {
                Title = title,
                CloseButton = true,
                IconClass = ""
            });
        }

        internal void UISuccess(string message)
        {
            UISuccess(message, _localizer["SuccessTitle"].Value);
        }
    }
}
