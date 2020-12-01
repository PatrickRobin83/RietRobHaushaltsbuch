using System;
using System.Collections;
using System.Windows.Media;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.Events;

namespace RietRobHaushaltsbuch.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Prism Application";
        private int _height = 600;
        private int _width = 1200;
        private readonly IRegionManager _regionManager;
        private bool _flyOutOpen;
        private IEventAggregator _eventAggregator;
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
            get{ return _height; }
        }

        public int Width
        {
            get { return _width; }
        }

        public DelegateCommand OpenFlyOutCommand { get; set; }
        public DelegateCommand<string> OpenViewCommand { get; set; }
        public DelegateCommand CloseApplicationCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggragator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggragator;
            RegisterCommands();
        }
        private void OpenFlyOut()
        {
            FlyOutOpen = true;
        }

        #region Overrides of ViewModelBase

        public void RegisterCommands()
        {
            OpenFlyOutCommand = new DelegateCommand(OpenFlyOut);
            OpenViewCommand = new DelegateCommand<string>(OpenView);
            CloseApplicationCommand = new DelegateCommand(CloseApplication);
        }

        private void CloseApplication()
        {
            Environment.Exit(0);
        }

        private void OpenView(string parameter)
        {
            if(string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException();
            _regionManager.RequestNavigate(RegionNames.MainRegion, parameter);
            if (parameter.Equals("OverView"))
            {
                _eventAggregator.GetEvent<NewsEvent>().Publish(parameter);
            }
            FlyOutOpen = false;
        }

        #endregion
    }
}
