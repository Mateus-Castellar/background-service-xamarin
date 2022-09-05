using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using ForegroundServiceDemo.Droid.Services;
using ForegroundServiceDemo.Interfaces;
using Java.Lang;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ForegroundService))]
namespace ForegroundServiceDemo.Droid.Services
{
    [Service]
    internal class ForegroundService : Service, IForegroundService
    {
        public static bool IsForegroundServiceRunning;

        public override IBinder OnBind(Intent intent) => throw new System.NotImplementedException();

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent,
            [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    System.Diagnostics.Debug.WriteLine("serviço de plano de fundo está rodando");
                    Thread.Sleep(2000);
                }
            });

            string channelId = "ForegroundServiceChannel";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var notificationChannel = new NotificationChannel(channelId, channelId, NotificationImportance.Low);
                notificationManager.CreateNotificationChannel(notificationChannel);
            }

            var notificationBuilder = new NotificationCompat.Builder(this, channelId)
                .SetContentTitle("Servico iniciado")
                .SetSmallIcon(Resource.Mipmap.icon)
                .SetContentText("Servico rodando em 2 plano")
                .SetPriority(1)
                .SetOngoing(true)
                .SetChannelId(channelId)
                .SetAutoCancel(true);

            StartForeground(1001, notificationBuilder.Build());
            return base.OnStartCommand(intent, flags, startId);
        }

        public void StartMyForegroundService()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
            Android.App.Application.Context.StartForegroundService(intent);
        }

        public void StopMyForegroundService()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
            Android.App.Application.Context.StopService(intent);
        }

        public override void OnCreate()
        {
            base.OnCreate();
            IsForegroundServiceRunning = true;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            IsForegroundServiceRunning = false;
        }

        public bool IsForegroundEnabled() => IsForegroundServiceRunning;
    }
}