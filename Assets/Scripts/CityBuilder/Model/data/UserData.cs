using System;
using System.Collections.Generic;

namespace PG.CityBuilder.Model.Data
{
    [Serializable]
    public class UserData
    {
        public List<ModuleRemoteData> ModulesInGrid;
        
        public List<GridRow> Grid;
        
        public long Gold;
        public long Iron;
        public long Wood;
    }

    [Serializable]
    public class GridRow
    {
        public bool[] Row;

        public bool this[int i]
        {
            get { return Row[i]; }
            set { Row[i] = value; }
        }
    }
}