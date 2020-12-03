using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

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
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}