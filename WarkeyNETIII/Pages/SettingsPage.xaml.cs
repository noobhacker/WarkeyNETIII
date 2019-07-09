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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WarkeyNETIII.Services;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        SettingsViewModel vm = new SettingsViewModel();

        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void OptimizeGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryService.WriteResolution(vm.CurrentResolution.Width, vm.CurrentResolution.Height);
            vm.GameResolution.Width = vm.CurrentResolution.Width;
            vm.GameResolution.Height = vm.CurrentResolution.Height;
            // trigger onpropertychanged
            vm.GameResolution = vm.CurrentResolution;

            vm.IsGameResolutionNeedsOptimize = false;
        }

        private void OptimizeLockFbbtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryService.WriteLockFb();
            vm.LockFbStatus = "Optimized";
            vm.IsLockFbNeedsOptimize = false;
        }

        private void FullHDGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryService.WriteResolution(1920, 1080);
            vm.GameResolution.Width = 1920;
            vm.GameResolution.Height = 1080;
            // trigger onpropertychanged
            vm.GameResolution = vm.CurrentResolution;

            vm.IsGameResolutionNeedsOptimize = false;
        }
    }
}
