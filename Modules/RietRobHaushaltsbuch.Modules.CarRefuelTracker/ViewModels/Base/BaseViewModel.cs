/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ViewModelBase.cs
 *   Date			:   2020-11-25 10:57:22
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels.Base
{
    public class BaseViewModel : BindableBase, ICloseWindows
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public Action Close { get; set; }
        #endregion


    }
}