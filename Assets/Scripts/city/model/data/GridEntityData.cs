using System;
using Sirenix.OdinInspector;

namespace game.city.model.data
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