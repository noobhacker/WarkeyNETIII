using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Presenter;
using Warkey.Infrastructure;
using static Warkey.Infrastructure.KeyboardDetector;

namespace Warkey.Core
{
    public class Settings
    {
        public static bool IsStartMinimized { get; set; }
        public static bool IsAutoStartWar3 { get; set; }
        public static bool IsAutoCloseWithWar3 { get; set; }

        private const string FILENAME = "WarkeyNETIII.json";
        private readonly SettingsManager _manager = new SettingsManager(FILENAME);

        public async Task<SettingsModel> LoadAsync()
        {

            // try catch whole block to prevent broken json setting file
            var result = await _manager.GetAsync<SettingsModel>();
            if(result.Warkeys.Slots.Count == 0)
            {
                // to resolve this ugliness, perhaps in viewmodel do
                // = new ObservableCollection<> {new HotkeyModel(), ...};
                // six times
                for (int i = 0; i < 6; i++)
                {
                    result.Warkeys.Slots.Add(new HotkeyModel());
                }
            }

            IsStartMinimized = result.IsStartMinimized;
            IsAutoStartWar3 = result.IsAutoStartWar3;
            IsAutoCloseWithWar3 = result.IsAutoCloseWithWar3;

            return result;
        }

        public async Task SaveAsync(SettingsModel model)
        {
            model.IsStartMinimized = IsStartMinimized;
            model.IsAutoStartWar3 = IsAutoStartWar3;
            model.IsAutoCloseWithWar3 = IsAutoCloseWithWar3;

            await _manager.SaveAsync(model);
        }

        public class SettingsModel
        {
            public WarkeyViewModel Warkeys { get; set; }
            public ObservableCollection<AutoChatViewModel> Autochats { get; set; }
            public bool IsStartMinimized { get; set; }
            public bool IsAutoStartWar3 { get; set; }
            public bool IsAutoCloseWithWar3 { get; set; }

            public SettingsModel()
            {
                Warkeys = new WarkeyViewModel();
                Autochats = new ObservableCollection<AutoChatViewModel>();
            }
        }
    }
}
