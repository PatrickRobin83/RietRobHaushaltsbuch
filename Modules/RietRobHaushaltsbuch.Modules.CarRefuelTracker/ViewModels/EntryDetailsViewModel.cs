/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   EntryDetailsViewModel.cs
 *   Date			:   2020-11-25 16:36:16
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Prism.Commands;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class EntryDetailsViewModel : BaseViewModel, IViewModelHelper
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public EntryDetailsViewModel()
        {
            RegisterCommands();
        }
        #endregion

        #region Methods
        public void RegisterCommands()
        {

        }
        #endregion

        #region Commands
        public DelegateCommand SelectionChangedCommand { get; set; }

        #endregion



    }
}