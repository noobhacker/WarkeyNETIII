using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Warkey.Core.ViewModels;
using Warkey.Infrastructure;
using Warkey.Infrastructure.Game;
using Warkey.Infrastructure.Keyboard;
using static Warkey.Infrastructure.Game.Saves;
using static Warkey.Infrastructure.Keyboard.Detector;

namespace Warkey.Core
{
    public class MainServices
    {
        private const int HWND_UPDATE_INTERVAL = 1;
        private static IntPtr war3Hwnd;
        private static bool IsWar3Foreground;      

        private static readonly Detector _keyboardDetector = new Detector(CheckKeyboardDetectorPrecondition);
        private static readonly Sender _keyboardSender = new Sender();
        private static readonly Window _gameWindow = new Window();

        public WarkeyViewModel Warkeys;
        public ObservableCollection<AutoChatItem> Autochats;
        public ObservableCollection<TkokSave> Saves;
        public Settings Settings;

        public async void InitializeAsync()
        {
            // read settings first or initialize data


            // cannot task.run on timer
            // tried put breakpoint on timer event
            updateWar3Hwnd();
            var hwndListener = new DispatcherTimer();
            hwndListener.Interval = TimeSpan.FromSeconds(HWND_UPDATE_INTERVAL);
            hwndListener.Tick += (sender, e) => updateWar3Hwnd();
            hwndListener.Start();

            // cannot run in another thread
            // because windows API cannot return 
            // to task.run in another thread
            _keyboardDetector.GlobalKeyDown += KeyboardHookService_GlobalKeyDown;

            //UpdateService.InitializeAsync();

        }

        private static bool CheckKeyboardDetectorPrecondition()
        {
            return IsWar3Foreground && !_keyboardSender.IsChatting;
        }
   
        private void KeyboardHookService_GlobalKeyDown(object sender, HotkeyModel e)
        {
            // check iswar3foreground already in keyboard hook class            
            foreach (var i in Enumerable.Range(0, 6))
            {
                // if key is not set
                if (warkeys[i] == null)
                    continue;

                if (warkeys[i].Key == e.Key && warkeys[i].Alt == e.Alt)
                    _keyboardSender.SendItemMessageToHwnd(war3Hwnd, i, e.Alt);
            }

            foreach (var item in Autochats)
            {
                if (item.Key == e.Key && !e.Alt)
                    _keyboardSender.SendMessageToChatbox(item.Message);
            }

            if (Saves.Count != 0)
            {
                if (e.Key == System.Windows.Input.Key.F8)
                    _keyboardSender.SendMessageToChatbox("-load{enter}{enter}" + Saves[0].Password);
            }
        }

        static bool wentRunning = false;
        private void updateWar3Hwnd()
        {
            var hwnd = _gameWindow.GetWar3HWND();
            if (hwnd != null)
            {
                war3Hwnd = hwnd.Value;
                MainWindow.vm.Title = "Running";
                wentRunning = true;

                IsWar3Foreground = _gameWindow.IsWar3Foreground(war3Hwnd);
            }
            else
            {
                if (wentRunning && Settings.IsAutoCloseWithWar3)
                    App.Current.MainWindow.Close();

                MainWindow.vm.Title = "";
            }
        }

        public void Dispose()
        {
            // prevent memory leakage by not unhooking from windows API
            _keyboardDetector.Dispose();
        }

    }
}
