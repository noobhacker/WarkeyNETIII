using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Warkey.Core.Queries;

namespace Warkey.View.Pages
{
    /// <summary>
    /// Interaction logic for LoadGamePage.xaml
    /// </summary>
    public partial class LoadGamePage : Page
    {
        private LoadGameViewModel _viewModel = new LoadGameViewModel();
        GameSavesQuery _query = new GameSavesQuery();
        public LoadGamePage(Services _services)
        {
            InitializeComponent();
            _viewModel.Saves = _services.Saves;
            this.DataContext = _viewModel;
        }

        private void startAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        public async void LoadSaveFilesAsync(int count)
        {
            _viewModel.Saves.Clear();
            var saves = await _query.GetTkokSaveFilesAsync(count);
            _viewModel.Saves = new ObservableCollection<Infrastructure.GameSaves.TkokSave>(saves);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.ExtraCommandVisibility = Visibility.Collapsed;
            LoadSaveFilesAsync(5);
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadSaveFilesAsync(5);
        }

        private void allBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadSaveFilesAsync(200);
        }

        private void copyBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = listBox.SelectedIndex;
            var password = _viewModel.Saves[index].Password;
            System.Windows.Forms.Clipboard.SetDataObject(
                 password, //text to store in clipboard
                 false,       //do not keep after our app exits
                 5,           //retry 5 times
                 200);        //200ms delay between retries
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (commandBar.Height == 50)
                startAnimationByName("BarOpen");
            else if (commandBar.Height == 70)
                startAnimationByName("BarClose");
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.ExtraCommandVisibility = Visibility.Visible;
        }
    }
}
