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

using RietRobHaushaltbuch.Core.Base;

namespace RietRobHaushaltsbuch.Modules.GarbageTracker.ViewModels
{
    public class GarbageTrackerOverViewModel :ViewModelBase
    {
        #region Fields

        private string _message;
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
        }
        #endregion

        #region Methods

        #endregion

    }
}