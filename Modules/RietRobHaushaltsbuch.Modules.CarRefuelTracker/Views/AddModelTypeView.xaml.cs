using System.Windows;
using MahApps.Metro.Controls;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views
{
    /// <summary>
    /// Interaktionslogik für AddModelTypeView.xaml
    /// </summary>
    public partial class AddModelTypeView : MetroWindow
    {
        public AddModelTypeView()
        {
            InitializeComponent();
            Loaded += AddModelTypeViewLoaded;
        }

        private void AddModelTypeViewLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindows vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
