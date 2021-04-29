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

        private string _message;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Properties
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion

        #region Constructor

        public GarbageTrackerOverViewModel()
        {
            Message = "Garbage Tracker";
            RegisterCommands();
        }
        #endregion

        #region Methods

        #endregion

        #region Implementation of IViewModelHelper

        public void RegisterCommands()
        {
        }

        #endregion
    }
}