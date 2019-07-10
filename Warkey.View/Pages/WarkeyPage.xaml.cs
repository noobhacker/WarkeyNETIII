using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for WarkeyPage.xaml
    /// </summary>
    public partial class WarkeyPage : Page
    {
        // by reference
        WarkeyViewModel vm = MainWindow.WarkeyVm;

        public WarkeyPage()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void itemSlots_Keydown(object sender, KeyEventArgs e)
        {
            var textbox = (TextBox)sender;
            var tag = textbox.Tag.ToString();

            bool alt = Keyboard.IsKeyDown(Key.LeftAlt) ? true : false;
            var key = alt ? e.SystemKey : e.Key;

            var prop = vm.GetType().GetProperty(tag);
            prop.SetValue(vm, new HotkeyItem()
            {
                Alt = alt,
                Key = key
            }, null);

        }
        
    }
}
