using Prism.Commands;
using Prism.Mvvm;
using RietRobHaushaltsbuch.ViewModels.Base;

namespace RietRobHaushaltsbuch.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Prism Application";

        private bool _flyOutOpen;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool FlyOutOpen
        {
            get { return _flyOutOpen; }
            set { SetProperty(ref _flyOutOpen, value); }
        }

        public DelegateCommand OpenFlyOutCommand { get; set; }


        public MainWindowViewModel()
        {

        }

        private void OpenFlyOut()
        {
            FlyOutOpen = true;
        }

        #region Overrides of ViewModelBase

        protected override void RegisterCommands()
        {
            OpenFlyOutCommand = new DelegateCommand(OpenFlyOut);
        }

        #endregion
    }
}
