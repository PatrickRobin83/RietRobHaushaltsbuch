using Prism.Mvvm;

namespace RietRobHaushaltsbuch.Modules.OverView.ViewModels
{
    public class OverViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public OverViewModel()
        {
            Message = "OverView from your OverView Module";
        }
    }
}
