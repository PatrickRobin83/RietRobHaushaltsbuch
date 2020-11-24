using System.Windows;
using MahApps.Metro.Controls;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views
{
    /// <summary>
    /// Interaktionslogik für AddBrandView.xaml
    /// </summary>
    public partial class AddBrandView : MetroWindow
    {
        public AddBrandView()
        {
            InitializeComponent();
            Loaded += AddBrandViewLoaded;
        }

        private void AddBrandViewLoaded(object sender, RoutedEventArgs e)
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
