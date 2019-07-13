using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Warkey.Core.Presenter;
using Warkey.Infrastructure;
using static Warkey.Core.Settings;
using static Warkey.Infrastructure.GameSaves;
using static Warkey.Infrastructure.KeyboardDetector;

namespace Warkey.Core
{
    public class Services : IDisposable
    {
        private bool sleep = false;
        private bool IsWar3Foreground;

        private readonly KeyboardDetector _keyboardDetector;
        private readonly KeyboardSender _keyboardSender;
        private readonly GameWindow _gameWindow;
        private readonly GameSaves _gameSaves;

        public IntPtr War3Hwnd { get; set; }
        public WarkeyViewModel Warkeys { get; set; }
        public ObservableCollection<AutoChatViewModel> Autochats { get; set; }
        public ObservableCollection<TkokSave> Saves { get; set; }
        public Settings Settings { get; set; }

        public Services()
        {
            _keyboardDetector = new KeyboardDetector(KeyboardDetectorPrecondition);
            _keyboardSender = new KeyboardSender();
            _gameWindow = new GameWindow();
            _gameSaves = new GameSaves();
            Settings = new Settings();
        }

        public async Task InitializeAsync()
        {
            // read settings first or initialize data
            var settings = await Settings.LoadAsync();
            Warkeys = settings.Warkeys;
            Autochats = settings.Autochats;

            var saves = await _gameSaves.GetTkokSaveFilesAsync(5);
            Saves = new ObservableCollection<TkokSave>(saves);

            if (Settings.IsAutoStartWar3 && File.Exists("war3.exe") && _gameWindow.GetWar3HWND() == null)
            {
                Process.Start("war3.exe");
            }

            // cannot task.run on timer, won't hit breakpoint on timer event
            UpdateWar3Hwnd();
            var sleepTimer = new DispatcherTimer();
            sleepTimer.Interval = TimeSpan.FromMilliseconds(1500);
            sleepTimer.Tick += SleepTimer_Tick;
            sleepTimer.Start();

            // cannot run in another thread
            // because windows API cannot return to task.run in another thread
            // !!! BECAREFUL! refactoring this to dynamic, not sure if system hook still works !!! 
            KeyboardDetector.GlobalKeyDown += KeyboardHookService_GlobalKeyDown;
        }

        // Sleep will keep adding the timer
        private void SleepTimer_Tick(object sender, EventArgs e)
        {
            sleep = true;
        }

        public async Task SaveSettingsAsync()
        {
            var model = new SettingsModel
            {
                 Warkeys = Warkeys,
                 Autochats = Autochats
            };

            await Settings.SaveAsync(model);
        }

        private bool KeyboardDetectorPrecondition()
        {
            if(sleep)
            {
                UpdateWar3Hwnd();
                sleep = false;
            }

            return IsWar3Foreground && !_keyboardSender.IsChatting;
        }

        private void KeyboardHookService_GlobalKeyDown(object sender, HotkeyModel e)
        {
            var slots = Warkeys.Slots;

            // check iswar3foreground already in keyboard hook class            
            foreach (var i in Enumerable.Range(0, 6))
            {
                if (slots[i] != null && slots[i].Key == e.Key && slots[i].Alt == e.Alt)
                {
                    _keyboardSender.SendItemMessageToHwnd(War3Hwnd, i, e.Alt);
                }
            }

            foreach (var item in Autochats)
            {
                if (item.Key == e.Key && !e.Alt)
                {
                    _keyboardSender.SendMessageToChatbox(item.Message);
                }
            }

            if (Saves.Count != 0 && e.Key == System.Windows.Input.Key.F8)
            {
                _keyboardSender.SendMessageToChatbox("-load{enter}{enter}" + Saves[0].Password);
            }
        }

        private bool wentRunning = false;
        public void UpdateWar3Hwnd()
        {
            var hwnd = _gameWindow.GetWar3HWND();
            if (hwnd != null)
            {
                War3Hwnd = hwnd.Value;
                wentRunning = true;

                IsWar3Foreground = _gameWindow.IsWar3Foreground(War3Hwnd);
            }
            else
            {
                War3Hwnd = IntPtr.Zero;
                if (wentRunning && Settings.IsAutoCloseWithWar3)
                {
                    ApplicationExitCommand?.Invoke(this, null);                  
                }              
            }
        }

        public event EventHandler ApplicationExitCommand;

        public void Dispose()
        {
            // prevent memory leakage by not unhooking from windows API
            _keyboardDetector.Dispose();
        }

    }
}
