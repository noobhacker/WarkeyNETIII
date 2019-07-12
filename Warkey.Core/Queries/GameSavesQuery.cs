using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Infrastructure;
using static Warkey.Infrastructure.GameSaves;

namespace Warkey.Core.Queries
{
    public class GameSavesQuery
    {
        private readonly GameSaves _gameSaves;

        public GameSavesQuery()
        {
            _gameSaves = new GameSaves();
        }

        public bool IsLoadFunctionAvailable()
        {
            return _gameSaves.IsLoadFunctionAvailable();
        }

        // this should be served as viewmodel
        public async Task<IEnumerable<TkokSave>> GetTkokSaveFilesAsync(int count)
        {
            return await _gameSaves.GetTkokSaveFilesAsync(count);
        }
    }
}
