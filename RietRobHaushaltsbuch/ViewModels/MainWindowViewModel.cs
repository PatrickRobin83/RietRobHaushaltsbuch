﻿using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.Enum;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Helper;
using System;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the MainWindow of the Application
    /// </summary>
    public class MainWindowViewModel : ViewModelBase, IViewModelHelper
    {
        #region Fields
        /// <summary>
        /// Title for the Window
        /// </summary>
        private string _title = "RietRob Haushalts-Buch";
        /// <summary>
        /// Height of the application window
        /// </summary>
        private int _height = 600;
        /// <summary>
        /// Width of the application window
        /// </summary>
        private int _width = 1200;
        /// <summary>
        /// RegionManager for Prism functionality
        /// </summary>
        private readonly IRegionManager _regionManager;
        /// <summary>
        /// Indicates the state of the FlyOut Menu Open = true, Closed = false
        /// </summary>
        private bool _flyOutOpen;
        /// <summary>
        /// Needed to recieve messages from other parts of the application
        /// </summary>
        private IEventAggregator _eventAggregator;
        /// <summary>
        /// Needed for logging functionality
        /// </summary>
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
        /// <summary>
        /// Command to open the flyout menu 
        /// </summary>
        public DelegateCommand OpenFlyOutCommand { get; set; }
        /// <summary>
        /// Command to open a view in the content region of the application
        /// </summary>
        public DelegateCommand<string> OpenViewCommand { get; set; }
        /// <summary>
        /// Command to Exit the whole application
        /// </summary>
        public DelegateCommand CloseApplicationCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="eventAggragator"></param>
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggragator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggragator;
            RegisterCommands();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Closes the Application
        /// </summary>
        private void CloseApplication()
        {
            LogHelper.WriteToLog(appLogger,"Exit Application", LogState.Debug);
            Environment.Exit(0);
        }
        /// <summary>
        /// Opens the FlyOut Menu
        /// </summary>
        private void OpenFlyOut()
        {
            FlyOutOpen = true;
        }

        /// <summary>
        /// Loads a View into the content region
        /// </summary>
        /// <param name="parameter"></param>
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

        /// <summary>
        /// Method to register the Commands
        /// </summary>
        public void RegisterCommands()
        {
            OpenFlyOutCommand = new DelegateCommand(OpenFlyOut);
            OpenViewCommand = new DelegateCommand<string>(OpenView);
            CloseApplicationCommand = new DelegateCommand(CloseApplication);
        }

        #endregion
    }
}
