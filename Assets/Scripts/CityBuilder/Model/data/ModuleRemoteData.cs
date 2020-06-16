using System;
using Newtonsoft.Json;

namespace PG.City.Model.data
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