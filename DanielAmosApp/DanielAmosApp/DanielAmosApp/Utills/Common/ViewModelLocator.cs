using DanielAmosApp.ViewModels;
using DanielAmosApp.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DanielAmosApp.Utills.Common
{

    public class ViewModelLocator
    {

        private readonly IServiceProvider serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider)
        {

            serviceProvider = ((App)Application.Current).serviceProvider;
        }

        public ILogInViewModel LogInViewModel =>
        ((App)Application.Current).serviceProvider.GetRequiredService<ILogInViewModel>();

        public IRegisterViewModel RegisterViewModel =>
       ((App)Application.Current).serviceProvider.GetRequiredService<IRegisterViewModel>();

        public IAddViewModel AddViewModel =>
       ((App)Application.Current).serviceProvider.GetRequiredService<IAddViewModel>();

        public IMachineViewModel MachineViewModel =>
       ((App)Application.Current).serviceProvider.GetRequiredService<IMachineViewModel>();

        public IMachinesContainerViewModel MachinesContainerViewModel =>
      ((App)Application.Current).serviceProvider.GetRequiredService<IMachinesContainerViewModel>();
    }
}
