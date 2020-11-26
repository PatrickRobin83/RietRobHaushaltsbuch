/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   AddBrandViewModel.cs
 *   Date			:   2020-11-24 13:43:58
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Prism.Commands;
using Prism.Events;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltbuch.Core.DataAccess;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels.Base;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class AddBrandViewModel : BaseViewModel, IViewModelHelper
    {
        private readonly IEventAggregator _eventAggregator;

        #region Fields

        private int _height = 180;
        private int _width = 380;
        private string _txtBrandName;
        private bool _hasCharacters = false;

        #endregion

        #region Properties

        public bool HasCharacters
        {
            get { return _hasCharacters; }
            set { SetProperty(ref _hasCharacters, value); }
        }

        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }
        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
        public string TxtBrandName
        {
            get { return _txtBrandName; }
            set { SetProperty(ref _txtBrandName, value); }
        }

        #endregion

        #region Constructor

        public AddBrandViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegisterCommands();
        }
        #endregion

        #region Methods

        public void RegisterCommands()
        {
            AddBrandCommand = new DelegateCommand(AddBrand).ObservesProperty(() => HasCharacters);
            TextChangedCommand = new DelegateCommand(BrandTextChanged).ObservesProperty(() => HasCharacters);
            CancelAddBrandCommand = new DelegateCommand(CancelAddBrand);
        }

        private void CancelAddBrand()
        {
            Close?.Invoke();
        }
        public void BrandTextChanged()
        {
            if (!string.IsNullOrWhiteSpace(TxtBrandName) && TxtBrandName.Length >= 2)
            {
                HasCharacters = true;
            }
            else
            {
                HasCharacters = false;
            }
        }
        private void AddBrand()
        {
            BrandModel modelToAdd = new BrandModel {BrandName = TxtBrandName};
            modelToAdd.Id = SqLiteDataAccessCarRefuelTrackerModule.AddBrand(modelToAdd).Id;
            _eventAggregator.GetEvent<ObjectEvent>().Publish(modelToAdd);
            Close?.Invoke();
        }
        #endregion

        #region Commands
        public DelegateCommand AddBrandCommand { get; set; }
        public DelegateCommand CancelAddBrandCommand { get; set; }
        public DelegateCommand TextChangedCommand { get; set; }
        #endregion

    }
}