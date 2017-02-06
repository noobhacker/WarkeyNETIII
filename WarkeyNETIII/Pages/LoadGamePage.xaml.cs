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
using WarkeyNETIII.Services;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Pages
{
    /// <summary>
    /// Interaction logic for LoadGamePage.xaml
    /// </summary>
    public partial class LoadGamePage : Page
    {
        LoadGameViewModel vm = MainWindow.LoadGameViewModel;
        public LoadGamePage()
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        private void startAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        public async void LoadSaveFilesAsync(int count)
        {
            vm.Saves.Clear();
            var saves = await SaveFileService.LoadTkokSaveFilesAsync(count);

            foreach (var item in saves)
                vm.Saves.Add(item);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm.ExtraCommandVisibility = Visibility.Collapsed;
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
            var password = vm.Saves[index].Password;
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
            vm.ExtraCommandVisibility = Visibility.Visible;
        }
    }
}
