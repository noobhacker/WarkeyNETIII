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
        private GameSavesQuery _gameSavesQuery;

        private static Stopwatch _stopwatch = new Stopwatch();
        public MainWindow()
        {
            _stopwatch.Start();
            InitializeComponent();

            _services = new Services();
            _gameSavesQuery = new GameSavesQuery();
            _services.ApplicationExitCommand += Services_ApplicationExitCommand;
            Debug.Print(_stopwatch.ElapsedMilliseconds.ToString() + "ms");
            _stopwatch.Stop();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _services.InitializeAsync();          

            if (Settings.IsStartMinimized)
            {
                WindowState = WindowState.Minimized;
            }

            if (!_gameSavesQuery.IsLoadFunctionAvailable())
            {
                loadBtn.Visibility = Visibility.Collapsed;
            }

            navFrame.Navigate(new WarkeyPage(_services));
            StartAnimationByName("FadeIn");
        }

        private void Services_ApplicationExitCommand(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await _services.SaveSettingsAsync();

            // disposing will unhook windows api
            _services.Dispose();
        }

        private void RemoveUIElementsHighlights(UIElementCollection items)
        {
            foreach (var item in items)
            {
                if (item.GetType() == typeof(Button))
                {
                    var button = (Button)item;
                    if (button.Tag != null)
                    {
                        button.Background = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
        }

        private void RemoveMenuItemHighlights()
        {
            RemoveUIElementsHighlights(hamMenu.Children);
            RemoveUIElementsHighlights(menuList.Children);
        }

        private void StartAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private readonly SolidColorBrush _menuHighlightedColor = 
            new SolidColorBrush(Color.FromArgb(255, 190, 230, 253));

        private void MenuItems_Click(object sender, RoutedEventArgs e)
        {            
            RemoveMenuItemHighlights();

            var button = (Button)sender;
            button.Background = _menuHighlightedColor;

            // this must be done manually, because the animation might be completed
            // before the registration of the Completed event
            var fadeAwayAnimation = this.FindResource("FadeAway") as Storyboard;

            fadeAwayAnimation.Completed += (s, _e) =>
            {
                switch (button.Tag.ToString())
                {
                    case "Warkey":
                        navFrame.Navigate(new WarkeyPage(_services));
                        StartAnimationByName("FadeIn");
                        break;
                    case "AutoChat":
                        navFrame.Navigate(new AutoChatPage(_services));
                        StartAnimationByName("FadeIn");
                        break;
                    case "LoadGame":
                        navFrame.Navigate(new LoadGamePage(_services));
                        StartAnimationByName("FadeIn");
                        break;
                    case "Settings":
                        navFrame.Navigate(new SettingsPage(_services));
                        StartAnimationByName("FadeInWithMotion");
                        break;
                    case "About":
                        navFrame.Navigate(new AboutPage());
                        StartAnimationByName("FadeIn");
                        break;
                }

            };

            fadeAwayAnimation.Begin();
        }
    }
}
