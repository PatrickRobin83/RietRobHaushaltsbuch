/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   AddModelTypeModel.cs
 *   Date			:   2020-11-25 10:49:28
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Models;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class AddModelTypeViewModel : ViewModelBase, IViewModelHelper
    {


        #region Fields

        private IEventAggregator _eventAggregator;
        private int _height;
        private int _width;
        private string _title;
        private BrandModel _brandModel;
        private string _txtBrandName;
        private string _txtModelName;
        private bool _hasCharacters = false;
        public bool HasCharacters
        {
            get { return _hasCharacters; }
            set { SetProperty(ref _hasCharacters, value); }
        }

        public string TxtModelName
        {
            get { return _txtModelName; }
            set { SetProperty(ref _txtModelName, value); }
        }
        public string TxtBrandName
        {
            get { return _txtBrandName; }
            set { SetProperty(ref _txtBrandName, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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
        #endregion

        #region Properties

        #endregion

        #region Constructor

        public AddModelTypeViewModel(IEventAggregator eventAggregator,BrandModel brandModel)
        {
            _brandModel = brandModel;
            _eventAggregator = eventAggregator;
            Height = 280;
            Width = 550;
            Title = "Fahrzeug Model hinzufügen";
            TxtBrandName = brandModel.BrandName;
            RegisterCommands();
        }
        #endregion

        #region Methods
        public void RegisterCommands()
        {
           TextChangedCommand = new DelegateCommand(TextChanged).ObservesProperty(() => HasCharacters); ;
           CancelAddModelCommand = new DelegateCommand(AddModelCanceled);
           AddModelTypeCommand = new DelegateCommand(AddModelType).ObservesProperty(() => HasCharacters);
        }

        private void AddModelType()
        {
            MessageBox.Show($"Model to Brand {_brandModel.BrandName} added");
            //ToDo: implement the Logic to add a Model to Database and Update the ListBox in CarDetailsView
        }

        private void AddModelCanceled()
        {
            Close?.Invoke();
        }

        private void TextChanged()
        {
            if (!string.IsNullOrWhiteSpace(TxtModelName) && TxtModelName.Length >= 2)
            {
                HasCharacters = true;
            }
            else
            {
                HasCharacters = false;
            }
        }

        #endregion

        #region Commands    
        public DelegateCommand TextChangedCommand { get; set; }
        public DelegateCommand CancelAddModelCommand { get; set; }
        public DelegateCommand AddModelTypeCommand { get; set; }
        #endregion
    }
}