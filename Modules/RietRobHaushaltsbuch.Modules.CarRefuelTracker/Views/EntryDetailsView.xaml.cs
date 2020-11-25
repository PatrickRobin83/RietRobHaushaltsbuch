using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using RietRobHaushaltbuch.Core.Interfaces;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views
{
    /// <summary>
    /// Interaktionslogik für EntryDetailsView.xaml
    /// </summary>
    public partial class EntryDetailsView : MetroWindow
    {
        public EntryDetailsView()
        {
            InitializeComponent();
            Loaded += EntryDetailsViewLoaded;
        }

        private void EntryDetailsViewLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindows vm)
            {
                vm.Close += () => { this.Close(); };
            }
        }
    }
}
