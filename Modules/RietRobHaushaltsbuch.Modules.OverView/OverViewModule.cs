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
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Properties
        #endregion

        #region Methods
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.OverView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<Views.OverView, OverViewModel>();
        }
        #endregion

        #region Constructor
        public OverViewModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        #endregion

        #region Commands
        #endregion

        #region Events
        #endregion
    }
}