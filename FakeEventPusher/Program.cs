using Topshelf;

namespace FakeEventPusher
{
    class Program
    {
        public static void Main(string[] args)
        {

            HostFactory.Run(hc =>
            {

                hc.Service<IServer>(sc =>
                {
                    sc.ConstructUsing(ServerFactory.CreateServer);
                    sc.WhenStarted(s => s.Start());
                    sc.WhenStopped(s => s.Stop());
                });

                hc.RunAsLocalSystem();

                hc.SetDescription("Publishes fake notification events at a rate of 10/s to an azure event hub");
                hc.SetServiceName("DoctorHibbert.FakeEventPusher");
                hc.SetDisplayName("DoctorHibbert FakeEventPusher");

                hc.EnableServiceRecovery(rc =>
                {
                    rc.RestartService(1); //First
                    rc.RestartService(1); //Second
                    rc.RestartService(1); //Subsequent
                    rc.SetResetPeriod(0);
                });
            });
        }
    }
}
