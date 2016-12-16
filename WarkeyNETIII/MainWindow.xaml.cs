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
using WarkeyNETIII.Pages;
using WarkeyNETIII.Services;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // public them so services can access
        public static MainViewModel vm = new MainViewModel();
        public static WarkeyViewModel WarkeyVm = new WarkeyViewModel();
        public static AutoChatViewModel autoChatVm = new AutoChatViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainService.InitializeServicesAsync();
            navFrame.Navigate(new WarkeyPage());          
        }

        private const double paneWidth = 200;

        private void startAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private void hamButton_Click(object sender, RoutedEventArgs e)
        {
            if (hamMenu.Width == 50)
                startAnimationByName("SideOpen");
            else if (hamMenu.Width == paneWidth)
                startAnimationByName("SideClose");
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

        private SolidColorBrush menuHighlightedColor = 
            new SolidColorBrush(Color.FromArgb(255, 190, 230, 253));

        private void menuItems_Click(object sender, RoutedEventArgs e)
        {
            removeMenuItemHighlights();

            var button = (Button)sender;
            button.Background = menuHighlightedColor;

            switch (button.Tag.ToString())
            {
                case "Warkey":
                    navFrame.Navigate(new WarkeyPage());
                    break;
                case "AutoChat":
                    navFrame.Navigate(new AutoChatPage());
                    break;
                case "Settings":
                    navFrame.Navigate(new SettingsPage());
                    break;
                case "About":
                    navFrame.Navigate(new AboutPage());
                    break;
            }

            if (hamMenu.Width == paneWidth)
                startAnimationByName("SideClose");
        }

    }
}
