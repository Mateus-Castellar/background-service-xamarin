using ForegroundServiceDemo.Interfaces;
using System;
using Xamarin.Forms;

namespace ForegroundServiceDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void IniciarServico(object sender, EventArgs e)
        {
            DependencyService.Resolve<IForegroundService>().StartMyForegroundService();
        }

        private void PararServico(object sender, EventArgs e)
        {
            DependencyService.Resolve<IForegroundService>().StopMyForegroundService();
        }

        private void ChecarStatus(object sender, EventArgs e)
        {
            if (DependencyService.Resolve<IForegroundService>().IsForegroundEnabled())
                DisplayAlert("Serviço", "Atenção o serviço já esta rodando", "Fechar");
            else
                DependencyService.Resolve<IForegroundService>().StartMyForegroundService();
        }
    }
}
