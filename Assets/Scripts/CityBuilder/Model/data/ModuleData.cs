using System;

namespace PG.City.Model.data
{
    public enum EModuleType
    {
        Residence = 0,
        IronMine,
        Sawmill,
        Bench,
        Tree,
    }

    public enum ECurrencyType
    {
        None = 0,
        Gold,
        Wood,
        Iron
    }
    
    [Serializable]
    public class ModuleData: GridEntityData
    {
        public EModuleType moduleType;

        public ECurrencyType ProductionType;
        
        public long ProductionPerTimeUnit;
        public long ProductionTimeUnit;

        public bool AutoProduction;

        public long CostGold;
        public long CostWood;
        public long CostIron;

        public long ConstructionTime;

        public string GetCostString()
        {
            return (CostGold > 0 ? (" Gold :" + CostGold) : "") +
                   (CostWood > 0 ? (" Wood :" + CostWood) : "") +
                   (CostIron > 0 ? (" Iron :" + CostIron) : "");
        }
    }
}