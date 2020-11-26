using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.OverView.ViewModels
{
    public class OverViewModel : ViewModelBase, IViewModelHelper
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public OverViewModel()
        {
            Message = "Message from your OverView Module";
            RegisterCommands();
        }

        #region Implementation of IViewModelHelper
        public void RegisterCommands()
        {
            //ToDO: Initalize here the DelegateCommands
        }
        #endregion
    }
}
