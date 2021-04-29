using RietRobHaushaltsbuch.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Unity;
using RietRobHaushaltbuch.Core.Enum;
using RietRobHaushaltbuch.Core.Helper;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker;
using RietRobHaushaltsbuch.Modules.GarbageTracker;
using RietRobHaushaltsbuch.Modules.OverView;
using NLog;

namespace RietRobHaushaltsbuch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected override Window CreateShell()
        {
            LogHelper.WriteLogOnStartup();
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //ToDo: Register the Modules here!
            moduleCatalog.AddModule<OverViewModule>();
            moduleCatalog.AddModule<CarRefuelTrackerModule>();
            moduleCatalog.AddModule<GarbageTrackerModule>();
        }
    }
}
