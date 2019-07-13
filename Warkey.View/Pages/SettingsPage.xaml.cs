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
using Warkey.Core.Queries;
using static Warkey.Infrastructure.Monitor;

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
        private GameSettingsQuery _query = new GameSettingsQuery();

        public SettingsPage(Services services)
        {
            InitializeComponent();
            _services = services;
            this.DataContext = _viewModel;
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // refactor to CQRS handler in Warkey.Core in the fiuchiah
            _viewModel.ScreenResolution = _query.GetScreenResolution();
            _viewModel.GameResolution = _query.GetGameResolution();
            _viewModel.IsLockFbNeedsOptimize = _query.GetLockFb();
            _viewModel.IsGameResolutionNeedsOptimize = CheckIfResolutionNeedsOptimize();

            _viewModel.IsStartMinimized = Settings.IsStartMinimized;
            _viewModel.IsAutoStartWar3 = Settings.IsAutoStartWar3;
            _viewModel.IsAutoCloseWar3 = Settings.IsAutoCloseWithWar3;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
        
        // temporary workaround for checkbox property changes!
        // I currently prefer clean ViewModel
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(_viewModel.IsStartMinimized):
                    Settings.IsStartMinimized = _viewModel.IsStartMinimized;
                    break;
                case nameof(_viewModel.IsAutoStartWar3):
                    Settings.IsStartMinimized = _viewModel.IsAutoStartWar3;
                    break;
                case nameof(_viewModel.IsAutoCloseWar3):
                    Settings.IsStartMinimized = _viewModel.IsAutoCloseWar3;
                    break;
            }
        }

        private void OptimizeGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            setResolution(_viewModel.ScreenResolution.Width, _viewModel.ScreenResolution.Height);
            _viewModel.IsGameResolutionNeedsOptimize = false;
        }

        private void OptimizeLockFbbtn_Click(object sender, RoutedEventArgs e)
        {
            _command.WriteLockFb();
            _viewModel.IsLockFbNeedsOptimize = false;
        }

        private void FullHDGameResolutionbtn_Click(object sender, RoutedEventArgs e)
        {
            setResolution(1920, 1080);
            _viewModel.IsGameResolutionNeedsOptimize = CheckIfResolutionNeedsOptimize();
        }

        private bool CheckIfResolutionNeedsOptimize()
        {
            if(_viewModel.ScreenResolution.Height != _viewModel.GameResolution.Height)
            {
                return true;
            }

            return false;
        }

        private void setResolution(int width, int height)
        {
            if (_services.War3Hwnd == IntPtr.Zero)
            {
                _command.WriteResolution(width, height);
                _viewModel.GameResolution.Width = width;
                _viewModel.GameResolution.Height = height;
                _viewModel.GameResolution =new ScreenResolution
                {
                    Width = width,
                    Height = height
                };

            }
            else
            {
                MessageBox.Show("Warcraft III is running, please close it before changing resolution.");
            }
        }

    }
}
