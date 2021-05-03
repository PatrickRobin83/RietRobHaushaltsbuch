/*
*----------------------------------------------------------------------------------
*          Filename:	ViewModelBase.cs
*          Date:        2020.11.22 23:26:55
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System;
using NLog;
using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltbuch.Core.Base

{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ViewModelBase : BindableBase, ICloseWindows
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructor

        #endregion

        #region Methods

        #endregion

        #region Implementation of ICloseWindows
        public Action Close { get; set; }
        #endregion
    }
}