﻿using System;
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
using Warkey.Core.Queries;
using Warkey.View.Pages;

namespace Warkey.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainViewModel _viewModel;
        private Services _services;
        private GameSavesQuery _gameSavesQuery;

        public MainWindow()
        {
            InitializeComponent();

            _services = new Services();
            _gameSavesQuery = new GameSavesQuery();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            _viewModel.Title = "Warkey.NET III";

            _services.WarcraftStatusChanged += Services_WarcraftStatusChanged;
            _services.ApplicationExitCommand += Services_ApplicationExitCommand;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _services.InitializeAsync();          

            if (Settings.IsStartMinimized)
            {
                WindowState = WindowState.Minimized;
            }

            if (!_gameSavesQuery.IsLoadFunctionAvailable())
            {
                loadBtn.Visibility = Visibility.Collapsed;
            }

            navFrame.Navigate(new WarkeyPage(_services));                  
        }

        private void Services_ApplicationExitCommand(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Services_WarcraftStatusChanged(object sender, bool e)
        {
            _viewModel.Title = e ? "Warkey.NET III - Running" : "Warkey.NET III";
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await _services.SaveSettingsAsync();

            // disposing will unhook windows api
            _services.Dispose();
        }

        private void RemoveUIElementsHighlights(UIElementCollection items)
        {
            foreach (var item in items)
            {
                if (item.GetType() == typeof(Button))
                {
                    var button = (Button)item;
                    if (button.Tag != null)
                    {
                        button.Background = new SolidColorBrush(Colors.Transparent);
                    }
                }
            }
        }

        private void RemoveMenuItemHighlights()
        {
            RemoveUIElementsHighlights(hamMenu.Children);
            RemoveUIElementsHighlights(menuList.Children);
        }

        private void StartAnimationByName(string name)
        {
            Storyboard sb = this.FindResource(name) as Storyboard;
            sb.Begin();
        }

        private readonly SolidColorBrush _menuHighlightedColor = 
            new SolidColorBrush(Color.FromArgb(255, 190, 230, 253));

        private void MenuItems_Click(object sender, RoutedEventArgs e)
        {            
            RemoveMenuItemHighlights();

            var button = (Button)sender;
            button.Background = _menuHighlightedColor;

            // this must be done manually, because the animation might be completed
            // before the registration of the Completed event
            var fadeAwayAnimation = this.FindResource("FadeAway") as Storyboard;

            fadeAwayAnimation.Completed += (s, _e) =>
            {
                switch (button.Tag.ToString())
                {
                    case "Warkey":
                        navFrame.Navigate(new WarkeyPage(_services));
                        break;
                    case "AutoChat":
                        navFrame.Navigate(new AutoChatPage(_services));
                        break;
                    case "LoadGame":
                        navFrame.Navigate(new LoadGamePage(_services));
                        break;
                    case "Settings":
                        navFrame.Navigate(new SettingsPage(_services));
                        break;
                    case "About":
                        navFrame.Navigate(new AboutPage());
                        break;
                }

                StartAnimationByName("FadeIn");
            };

            fadeAwayAnimation.Begin();
        }
    }
}
