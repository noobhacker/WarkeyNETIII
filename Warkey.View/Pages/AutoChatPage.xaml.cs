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
using Warkey.Core.Items;
using Warkey.Core.ViewModels;

namespace Warkey.Core.Pages
{
    /// <summary>
    /// Interaction logic for AutoChatPage.xaml
    /// </summary>
    public partial class AutoChatPage : Page
    {
        AutoChatViewModel vm = MainWindow.AutoChatVm;

        public AutoChatPage()
        {
            InitializeComponent();
            this.DataContext = vm;

            // do it here so navigate from another page will keep collapsed
            vm.ExtraCommandVisibility = Visibility.Collapsed;
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

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAutoChatPage());
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = (ListBox)sender;
            if (listbox.SelectedItems.Count != 0)
                vm.ExtraCommandVisibility = Visibility.Visible;
            else
                vm.ExtraCommandVisibility = Visibility.Collapsed;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = listBox.SelectedIndex;
            NavigationService.Navigate(new EditAutoChatPage(new AutoChatItem()
            {
                Key = vm.ListOfAutoChats[index].Key,
                Message = vm.ListOfAutoChats[index].Message
            }, index));
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Warkey.NET III", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var index = listBox.SelectedIndex;
                vm.ListOfAutoChats.RemoveAt(index);

                await Settings.SaveSettingsAsync();
            }           
        }
    }
}
