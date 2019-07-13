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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warkey.Core;
using Warkey.Core.Presenter;

namespace Warkey.View.Pages
{
    /// <summary>
    /// Interaction logic for EditAutoChatPage.xaml
    /// </summary>
    public partial class EditAutoChatPage : Page
    {
        private AutoChatViewModel _viewModel;
        private Services _services;
        int? index;

        public EditAutoChatPage(Services services)
        {
            InitializeComponent();
            _services = services;
            _viewModel = new AutoChatViewModel();
            this.DataContext = _viewModel;
        }

        public EditAutoChatPage(Services services, AutoChatViewModel item, int index)
        {
            InitializeComponent();
            _services = services;
            _viewModel = item;
            this.DataContext = _viewModel;

            this.index = index;       
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void hotkeyTb_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.Key = e.Key;
        }

        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(hotkeyTb.Text != "" && _viewModel.Message != "")
            {
                var list =_services.Autochats;
                if (index == null)
                {
                    list.Add(new AutoChatViewModel()
                    {
                        Key = _viewModel.Key,
                        Message = _viewModel.Message
                    });
                }
                else
                {
                    list[index.Value].Key = _viewModel.Key;
                    list[index.Value].Message = _viewModel.Message;
                }

                await _services.SaveSettingsAsync();
                NavigationService.GoBack();
            }
        }
    }
}
