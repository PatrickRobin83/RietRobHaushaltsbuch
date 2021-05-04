/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   GarbageTrackerOverViewModel.cs
 *   Date			:   2020-12-07 14:11:36
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using NLog;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.Enum;
using RietRobHaushaltbuch.Core.Helper;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.GarbageTracker.ViewModels
{
    public class GarbageTrackerOverViewModel :ViewModelBase, IViewModelHelper
    {
        #region Fields

        private string _title;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Properties
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Constructor

        public GarbageTrackerOverViewModel()
        {
            Title = "Garbage Tracker";
            RegisterCommands();
        }
        #endregion

        #region Methods
        public void RegisterCommands()
        {

        }
        #endregion
    }
}