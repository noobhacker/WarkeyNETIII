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
using Warkey.Core;
using Warkey.Core.Commands;
using Warkey.Core.Presenter;

namespace Warkey.View.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private SettingsViewModel _viewModel = new SettingsViewModel();
        private Services _services;
        private GameSettingsCommand _command = new GameSettingsCommand();

        public SettingsPage(Services services)
        {
            InitializeComponent();
            _services = services;
            this.DataContext = _viewModel;
        }

        private void OptimizeGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            setResolution(_viewModel.CurrentResolution.Width, _viewModel.CurrentResolution.Height);
        }

        private void OptimizeLockFbbtn_Click(object sender, RoutedEventArgs e)
        {
            _command.WriteLockFb();
            _viewModel.IsLockFbNeedsOptimize = false;
        }

        private void FullHDGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            setResolution(1920, 1080);
        }

        private void setResolution(int width, int height)
        {
            if (_services.War3Hwnd == null)
            {
                _command.WriteResolution(width, height);
                _viewModel.GameResolution.Width = width;
                _viewModel.GameResolution.Height = height;
                // trigger onpropertychanged
                _viewModel.GameResolution = _viewModel.CurrentResolution;

                _viewModel.IsGameResolutionNeedsOptimize = false;
            }
            else
            {
                MessageBox.Show("Warcraft III is running, please close it before changing resolution.");
            }
        }
    }
}
