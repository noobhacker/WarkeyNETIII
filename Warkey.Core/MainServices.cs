using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Warkey.Core.ViewModels;
using Warkey.Infrastructure;
using static Warkey.Infrastructure.GameSaves;
using static Warkey.Infrastructure.KeyboardDetector;

namespace Warkey.Core
{
    public class MainServices
    {
        private const int HWND_UPDATE_INTERVAL = 1;
        private IntPtr war3Hwnd;
        private bool IsWar3Foreground;

        private readonly KeyboardDetector _keyboardDetector;
        private readonly KeyboardSender _keyboardSender;
        private readonly GameWindow _gameWindow;

        public WarkeyViewModel Warkeys;
        public ObservableCollection<AutoChatItem> Autochats;
        public ObservableCollection<TkokSave> Saves;
        public Settings Settings;

        public MainServices()
        {
            _keyboardDetector = new KeyboardDetector(CheckKeyboardDetectorPrecondition);
            _keyboardSender = new KeyboardSender();
            _gameWindow = new GameWindow();
        }

        public async void InitializeAsync()
        {
            // read settings first or initialize data
            await Settings.LoadAsync();
            if (Settings.IsAutoStartWar3 && File.Exists("war3.exe") && _gameWindow.GetWar3HWND() == null)
            {
                Process.Start("war3.exe");
            }

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
            // !!! BECAREFUL! refactoring this to dynamic, not sure if system hook still works !!! 
            _keyboardDetector.GlobalKeyDown += KeyboardHookService_GlobalKeyDown;

            //UpdateService.InitializeAsync();

        }

        private bool CheckKeyboardDetectorPrecondition()
        {
            return IsWar3Foreground && !_keyboardSender.IsChatting;
        }

        // think of a way to map this to viewmodel that is not array
        HotkeyModel[] warkeys =
        {
            Warkeys.Slot1,
            Warkeys.Slot2,
            Warkeys.Slot3,
            Warkeys.Slot4,
            Warkeys.Slot5,
            Warkeys.Slot6,
        };

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
                WarcraftStatusChanged?.Invoke(this, true);
                wentRunning = true;

                IsWar3Foreground = _gameWindow.IsWar3Foreground(war3Hwnd);
            }
            else
            {
                if (wentRunning && Settings.IsAutoCloseWithWar3)
                {
                    ApplicationExitCommand?.Invoke(this, null);
                }

                WarcraftStatusChanged?.Invoke(this, false);
            }
        }

        public event EventHandler<bool> WarcraftStatusChanged;
        public event EventHandler ApplicationExitCommand;

        public void Dispose()
        {
            // prevent memory leakage by not unhooking from windows API
            _keyboardDetector.Dispose();
        }

    }
}
