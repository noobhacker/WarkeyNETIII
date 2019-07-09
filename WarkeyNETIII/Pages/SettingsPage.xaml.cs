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
            setResolution(vm.CurrentResolution.Width, vm.CurrentResolution.Height);
        }

        private void OptimizeLockFbbtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryService.WriteLockFb();
            vm.LockFbStatus = "Optimized";
            vm.IsLockFbNeedsOptimize = false;
        }

        private void FullHDGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            setResolution(1920, 1080);
        }

        private void setResolution(int width, int height)
        {
            if (MainWindowHandleService.GetWar3HWND() == null)
            {
                RegistryService.WriteResolution(width, height);
                vm.GameResolution.Width = width;
                vm.GameResolution.Height = height;
                // trigger onpropertychanged
                vm.GameResolution = vm.CurrentResolution;

                vm.IsGameResolutionNeedsOptimize = false;
            }
            else
            {
                MessageBox.Show("Warcraft III is running, please close it before changing resolution.");
            }
        }
    }
}
