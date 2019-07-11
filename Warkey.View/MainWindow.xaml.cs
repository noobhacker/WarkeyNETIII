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
using Warkey.View.Pages;

namespace Warkey.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // public them so services can access
        public static MainViewModel vm = new MainViewModel();
        public static WarkeyViewModel WarkeyVm;
        public static AutoChatViewModel AutoChatVm;
        public static LoadGameViewModel LoadGameViewModel = new LoadGameViewModel();

        public Services _services = new Services();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this will load vm's
            await _services.InitializeAsync();

            if (Settings.IsStartMinimized)
            {
                WindowState = WindowState.Minimized;
            }

            if (!SaveFileService.IsLoadFunctionAvailable())
            {
                loadBtn.Visibility = Visibility.Collapsed;
            }

            navFrame.Navigate(new WarkeyPage());                  
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await Settings.SaveSettingsAsync();

            // disposing mainservice is unhooking windows api
            // should be last
            MainService.Dispose();
        }

        private void removeUIElementsHighlights(UIElementCollection items)
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

        private void removeMenuItemHighlights()
        {
            removeUIElementsHighlights(hamMenu.Children);
            removeUIElementsHighlights(menuList.Children);
        }

        private void startAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private SolidColorBrush menuHighlightedColor = 
            new SolidColorBrush(Color.FromArgb(255, 190, 230, 253));

        private void menuItems_Click(object sender, RoutedEventArgs e)
        {            
            removeMenuItemHighlights();

            var button = (Button)sender;
            button.Background = menuHighlightedColor;

            // this must be done manually, because the animation might be completed
            // before the registration of the Completed event
            var fadeAwayAnimation = this.FindResource("FadeAway") as Storyboard;

            fadeAwayAnimation.Completed += (s, _e) =>
            {
                switch (button.Tag.ToString())
                {
                    case "Warkey":
                        navFrame.Navigate(new WarkeyPage());
                        break;
                    case "AutoChat":
                        navFrame.Navigate(new AutoChatPage());
                        break;
                    case "LoadGame":
                        navFrame.Navigate(new LoadGamePage());
                        break;
                    case "Settings":
                        navFrame.Navigate(new SettingsPage());
                        break;
                    case "About":
                        navFrame.Navigate(new AboutPage());
                        break;
                }

                startAnimationByName("FadeIn");
            };

            fadeAwayAnimation.Begin();
        }
    }
}
