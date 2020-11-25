using System.Windows;
using MahApps.Metro.Controls;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views
{
    /// <summary>
    /// Interaktionslogik für FuelTypeView.xaml
    /// </summary>
    public partial class AddFuelTypeView : MetroWindow
    {
        public AddFuelTypeView()
        {
            InitializeComponent();
            Loaded += AddFuelTypeViewLoaded;
        }

        private void AddFuelTypeViewLoaded(object sender, RoutedEventArgs e)
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
