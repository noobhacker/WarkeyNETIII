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
using Warkey.Core.Items;
using Warkey.Core.ViewModels;

namespace Warkey.Core.Pages
{
    /// <summary>
    /// Interaction logic for EditAutoChatPage.xaml
    /// </summary>
    public partial class EditAutoChatPage : Page
    {
        EditAutoChatViewModel vm = new EditAutoChatViewModel();
        int? index;

        public EditAutoChatPage()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        public EditAutoChatPage(AutoChatItem item, int index)
            : this()
        {
            vm.Key = item.Key;
            vm.Message = item.Message;
            this.index = index;       
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void hotkeyTb_KeyDown(object sender, KeyEventArgs e)
        {
            vm.Key = e.Key;
        }

        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(hotkeyTb.Text != "" && vm.Message != "")
            {
                var list = MainWindow.AutoChatVm.ListOfAutoChats;
                if (index == null)
                {
                    list.Add(new AutoChatItem()
                    {
                        Key = vm.Key,
                        Message = vm.Message
                    });
                }
                else
                {
                    list[index.Value].Key = vm.Key;
                    list[index.Value].Message = vm.Message;
                }

                await Settings.SaveSettingsAsync();
                NavigationService.GoBack();
            }
        }
    }
}
