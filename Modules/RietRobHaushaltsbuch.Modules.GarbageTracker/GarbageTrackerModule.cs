using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltsbuch.Modules.GarbageTracker.ViewModels;
using RietRobHaushaltsbuch.Modules.GarbageTracker.Views;

namespace RietRobHaushaltsbuch.Modules.GarbageTracker
{
    public class GarbageTrackerModule : IModule
    {
        private readonly IRegionManager _regionManager;
        
        public GarbageTrackerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(GarbageTrackerOverView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<GarbageTrackerOverView, GarbageTrackerOverViewModel>();
        }
    }
}