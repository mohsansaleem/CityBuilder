
using Newtonsoft.Json;
using System;

namespace game.city.model.data
{
    [Serializable]
    public class ModuleRemoteData : GridEntityRemoteData
    {
        public long Id;
        public EModuleType moduleType;
        public DateTime LastCommandTime;
        public bool IsInProduction;
        public bool IsInConstruction;
        public bool IsFull;
        
        [JsonIgnore]
        public ModuleData ModuleData => this.GridEntityData as ModuleData;
    }
}