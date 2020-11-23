using RietRobHaushaltsbuch.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Regions;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker;
using RietRobHaushaltsbuch.Modules.OverView;

namespace RietRobHaushaltsbuch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {

            //ToDo: Register the Modules here!
            moduleCatalog.AddModule<CarRefuelTrackerModule>();
            moduleCatalog.AddModule<OverViewModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
        }

    }
}
