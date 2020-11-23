using RietRobHaushaltsbuch.Modules.OverView.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltsbuch.Modules.OverView.ViewModels;

namespace RietRobHaushaltsbuch.Modules.OverView
{
    public class OverViewModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public OverViewModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.OverView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<Views.OverView, OverViewModel>();
        }


    }
}