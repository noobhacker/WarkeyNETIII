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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;

            NavFrame.Navigate(new WarkeyPage());
        }

        private void startAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private void hamButton_Click(object sender, RoutedEventArgs e)
        {
            if (hamMenu.Width == 50)
                startAnimationByName("SideOpen");
            else if (hamMenu.Width == 200)
                startAnimationByName("SideClose");
        }

        private void menuItems_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            switch(button.Tag.ToString())
            {

            }
        }
    }
}
