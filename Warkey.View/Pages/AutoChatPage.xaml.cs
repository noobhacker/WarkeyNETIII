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

namespace Warkey.View.Pages
{
    /// <summary>
    /// Interaction logic for AutoChatPage.xaml
    /// </summary>
    public partial class AutoChatPage : Page
    {
        private AutoChatsViewModel _viewModel = new AutoChatsViewModel();
        private readonly Services _services;

        public AutoChatPage(Services services)
        {
            InitializeComponent();
            _services = services;
            _viewModel.ListOfAutoChats = _services.Autochats;
            DataContext = _viewModel;

            // do it here so navigate from another page will keep collapsed
            _viewModel.ExtraCommandVisibility = Visibility.Collapsed;
        
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
            NavigationService.Navigate(new EditAutoChatPage(_services));
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = (ListBox)sender;
            if (listbox.SelectedItems.Count != 0)
                _viewModel.ExtraCommandVisibility = Visibility.Visible;
            else
                _viewModel.ExtraCommandVisibility = Visibility.Collapsed;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = listBox.SelectedIndex;
            NavigationService.Navigate(new EditAutoChatPage(_services, new AutoChatViewModel()
            {
                Key = _viewModel.ListOfAutoChats[index].Key,
                Message = _viewModel.ListOfAutoChats[index].Message
            }, index));
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Warkey.NET III", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var index = listBox.SelectedIndex;
                _viewModel.ListOfAutoChats.RemoveAt(index);

                await _services.SaveSettingsAsync();
            }           
        }
    }
}
