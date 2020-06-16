using System;
using Sirenix.OdinInspector;

namespace PG.City.Model.data
{
    [Serializable]
    public class GridEntityData
    {
        [MinValue(1), MaxValue(10)]
        public int Width;
        [MinValue(1), MaxValue(10)]
        public int Length;
    }
}