/*
*----------------------------------------------------------------------------------
*          Filename:	ViewModelBase.cs
*          Date:        2020.11.22 23:26:55
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using Prism.Mvvm;

namespace RietRobHaushaltsbuch.ViewModels.Base

{
    public abstract class ViewModelBase : BindableBase
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

        protected virtual void RegisterCommands() { }

        public virtual void RegisterCollections() { }

        #endregion

    }
}