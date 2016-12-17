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
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Pages
{
    /// <summary>
    /// Interaction logic for AutoChatPage.xaml
    /// </summary>
    public partial class AutoChatPage : Page
    {
        AutoChatViewModel vm = MainWindow.autoChatVm;

        public AutoChatPage()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void startAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (commandBar.Height == 50)
                startAnimationByName("BarOpen");
            else if (commandBar.Height == 70)
                startAnimationByName("BarClose");
        }
    }
}
