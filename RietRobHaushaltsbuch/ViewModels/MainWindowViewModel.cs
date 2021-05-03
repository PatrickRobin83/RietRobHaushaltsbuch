using System;
using System.Collections;
using System.Windows.Media;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.Enum;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Helper;
using Unity.Injection;

namespace RietRobHaushaltsbuch.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private string _title = "RietRob Haushalts-Buch";
        private int _height = 600;
        private int _width = 1200;
        private readonly IRegionManager _regionManager;
        private bool _flyOutOpen;
        private IEventAggregator _eventAggregator;
        private static NLog.Logger appLogger = NLog.LogManager.GetCurrentClassLogger();
        #endregion

        #region Properties
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool FlyOutOpen
        {
            get { return _flyOutOpen; }
            set { SetProperty(ref _flyOutOpen, value); }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }

        #endregion

        #region Commands
        public DelegateCommand OpenFlyOutCommand { get; set; }
        public DelegateCommand<string> OpenViewCommand { get; set; }
        public DelegateCommand CloseApplicationCommand { get; set; }

        #endregion

        #region Constructor
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggragator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggragator;
            RegisterCommands();
        }
        #endregion

        #region Methods
        private void CloseApplication()
        {
            LogHelper.WriteToLog(appLogger,"Exit Application", LogState.Debug);
            Environment.Exit(0);
        }
        private void OpenFlyOut()
        {
            FlyOutOpen = true;
        }


        private void OpenView(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException();
            _regionManager.RequestNavigate(RegionNames.MainRegion, parameter);
            if (parameter.Equals("OverView"))
            {
                _eventAggregator.GetEvent<NewsEvent>().Publish(parameter);
            }
            FlyOutOpen = false;
            LogHelper.WriteToLog(appLogger, $"{parameter} loaded", LogState.Debug);
        }
        #endregion

        #region Overrides of ViewModelBase

        public void RegisterCommands()
        {
            OpenFlyOutCommand = new DelegateCommand(OpenFlyOut);
            OpenViewCommand = new DelegateCommand<string>(OpenView);
            CloseApplicationCommand = new DelegateCommand(CloseApplication);
        }

        #endregion
    }
}
