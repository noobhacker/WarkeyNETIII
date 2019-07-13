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
using Warkey.Core.Presenter;
using Warkey.Core;
using static Warkey.Infrastructure.KeyboardDetector;

namespace Warkey.View.Pages
{
    /// <summary>
    /// Interaction logic for WarkeyPage.xaml
    /// </summary>
    public partial class WarkeyPage : Page
    {
        // by reference
        private readonly WarkeyViewModel _viewModel;


        public WarkeyPage(Services services)
        {
            InitializeComponent();
            _viewModel = services.Warkeys;
            this.DataContext = _viewModel;
        }

        private void itemSlots_Keydown(object sender, KeyEventArgs e)
        {
            var textbox = (TextBox)sender;
            var tag = Convert.ToInt32(textbox.Tag);

            bool alt = Keyboard.IsKeyDown(Key.LeftAlt) ? true : false;
            var key = alt ? e.SystemKey : e.Key;

            _viewModel.Slots[tag] = new HotkeyModel()
            {
                Alt = alt,
                Key = key
            };

        }
        
    }
}
