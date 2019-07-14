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
