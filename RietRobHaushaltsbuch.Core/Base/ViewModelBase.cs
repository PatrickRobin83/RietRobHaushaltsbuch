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
using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltbuch.Core.Base

{
    public abstract class ViewModelBase : BindableBase, ICloseWindows
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public ViewModelBase()
        {
            RegisterCommands();
        }

        #endregion

        #region Methods

        public virtual void RegisterCommands() { }

        public virtual void RegisterCollections() { }

        #endregion

        #region Implementation of ICloseWindows
        public Action Close { get; set; }
        #endregion
    }
}