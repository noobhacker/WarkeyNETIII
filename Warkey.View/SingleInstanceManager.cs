using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Warkey.View
{
    public class SingleInstanceManager : WindowsFormsApplicationBase
    {
        private App _app;

        public SingleInstanceManager()
        {
            IsSingleInstance = true;
        }

        protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {
            // First time app is launched
            _app = new App();
            _app.InitializeComponent();
            _app.Run();
            return false;
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            // Subsequent launches
            base.OnStartupNextInstance(eventArgs);
            ActivateWindow(); 

        }
    
        public void ActivateWindow()
        {
            // Reactivate application's main window
            if (_app.MainWindow.WindowState == WindowState.Minimized)
            {
                _app.MainWindow.WindowState = WindowState.Normal;
            }

            _app.MainWindow.Activate();
            _app.MainWindow.Topmost = true;
            _app.MainWindow.Topmost = false;
            _app.MainWindow.Focus();
        }
    }
}
