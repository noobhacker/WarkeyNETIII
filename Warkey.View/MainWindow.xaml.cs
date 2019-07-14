using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warkey.Core;
using Warkey.Core.Presenter;
using Warkey.Core.Queries;
using Warkey.View.Pages;

namespace Warkey.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Services _services;

        public MainWindow()
        {
            InitializeComponent();           
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            _services = new Services();
            _services.ApplicationExitCommand += Services_ApplicationExitCommand;
            await _services.InitializeAsync();

            if (Settings.IsStartMinimized)
            {
                WindowState = WindowState.Minimized;
            }

            MainFrame.Navigate(new MainPage(_services));
            base.OnContentRendered(e);
        }

        private void Services_ApplicationExitCommand(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await _services.SaveSettingsAsync();
            _services.Dispose();
        }
    
    }
}
