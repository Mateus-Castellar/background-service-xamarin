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
    }
}
