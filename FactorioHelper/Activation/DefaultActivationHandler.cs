using FactorioHelper.Services;
using FactorioHelper.ViewModels;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Activation
{
    public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
    {
        private readonly INavigationService _navigationService;

        public DefaultActivationHandler(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        {
            return _navigationService.Frame?.Content == null;
        }

        protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
        {
            _navigationService.NavigateTo(typeof(MainViewModel).FullName!, args.Arguments);

            await Task.CompletedTask;
        }
    }
}
