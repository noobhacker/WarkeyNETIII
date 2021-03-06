﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Warkey.Infrastructure
{
    public class KeyboardDetector : IDisposable
    {
        internal class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr GetModuleHandle(string lpModuleName);
        }

        private const int WH_KEYBOARD_LL = 13;

        //LEFT ALT for lParam
        private const int VK_LMENU = 0x00A4;

        private const int WM_KEYDOWN = 0x0100;
        //private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;

        private static readonly LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public static Func<bool> Precondition { get; set; }

        public KeyboardDetector(Func<bool> precondition)
        {
            if (_hookID != IntPtr.Zero) throw new InvalidOperationException("Can't hook more than once.");
            _hookID = SetHook(_proc);
            Precondition = precondition;
        }

        public void Dispose()
        {
            NativeMethods.UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //preprocess: check iswar3forground and ischatting
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // iswar3foreground have to put here
            // because if not in foreground no need to do anything
            // else need to have pointer to mainservice iswar3foreground
            // and pass in here when initialize
            // which is not possible with C#
            if (Precondition() && nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    GlobalKeyDown(null, new HotkeyModel()
                    {
                        Alt = false,
                        Key = KeyInterop.KeyFromVirtualKey(vkCode)
                    });
                }
                else if (wParam == (IntPtr)WM_SYSKEYDOWN)
                {
                    if (vkCode != VK_LMENU)
                    {
                        GlobalKeyDown(null, new HotkeyModel()
                        {
                            Alt = true,
                            Key = KeyInterop.KeyFromVirtualKey(vkCode)
                        });
                    }
                }

            }
            return NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public static event EventHandler<HotkeyModel> GlobalKeyDown;

        public class HotkeyModel
        {
            public bool Alt { get; set; }
            public Key Key { get; set; }

            public override string ToString()
            {
                var keyConverter = new KeyConverter();
                var key = keyConverter.ConvertToString(Key);
                var alt = Alt ? "ALT + " : "";

                return $"{alt}{key}";
            }
        }
    }
}
