using System;
using Newtonsoft.Json;
using UnityEngine;

namespace PG.City.Model.data
{
    [Serializable]
    public class GridEntityRemoteData
    {
        public Vector3 CurrentPosition;

        [JsonIgnore]
        public int Width => GridEntityData.Width;
        [JsonIgnore]
        public int Length => GridEntityData.Length;
        
        [JsonIgnore] public GridEntityData GridEntityData { get; set; }
    }
}