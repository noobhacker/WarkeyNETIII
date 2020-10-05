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
namespace Warkey.View.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private GameSavesQuery _gameSavesQuery;
        private Services _services;
        public MainPage(Services services)
        {
            InitializeComponent();
            _services = services;
            _gameSavesQuery = new GameSavesQuery();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_gameSavesQuery.IsLoadFunctionAvailable())
            {
                loadBtn.Visibility = Visibility.Collapsed;
            }

            navFrame.Navigate(new WarkeyPage(_services));
            StartAnimationByName("FadeIn");
        }

        private void RemoveMenuItemHighlights()
        {
            WarkeyHighlighter.Visibility = Visibility.Hidden;
            OptimizeHighlighter.Visibility = Visibility.Hidden;
            LoadGameHighlighter.Visibility = Visibility.Hidden;
            AutochatHighlighter.Visibility = Visibility.Hidden;
            AboutHighlighter.Visibility = Visibility.Hidden;
        }

        private void StartAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private void MenuItems_Click(object sender, RoutedEventArgs e)
        {
            RemoveMenuItemHighlights();

            var button = (Button)sender;
            // this must be done manually, because the animation might be completed
            // before the registration of the Completed event
            var fadeAwayAnimation = this.FindResource("FadeAway") as Storyboard;

            fadeAwayAnimation.Completed += (s, _e) =>
            {
                switch (button.Tag.ToString())
                {
                    case "Warkey":
                        WarkeyHighlighter.Visibility = Visibility.Visible;
                        navFrame.Navigate(new WarkeyPage(_services));
                        StartAnimationByName("FadeIn");
                        break;
                    case "AutoChat":
                        AutochatHighlighter.Visibility = Visibility.Visible;
                        navFrame.Navigate(new AutoChatPage(_services));
                        StartAnimationByName("FadeIn");
                        break;
                    case "LoadGame":
                        LoadGameHighlighter.Visibility = Visibility.Visible;
                        navFrame.Navigate(new LoadGamePage(_services));
                        StartAnimationByName("FadeIn");
                        break;
                    case "Settings":
                        OptimizeHighlighter.Visibility = Visibility.Visible;
                        navFrame.Navigate(new SettingsPage(_services));
                        StartAnimationByName("FadeInWithMotion");
                        break;
                    case "About":
                        AboutHighlighter.Visibility = Visibility.Visible;
                        navFrame.Navigate(new AboutPage());
                        StartAnimationByName("FadeIn");
                        break;
                }

            };

            fadeAwayAnimation.Begin();
        }
    }
}
