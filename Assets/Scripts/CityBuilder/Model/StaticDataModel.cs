using System.Linq;
using PG.City.Model.Data;

namespace PG.City.Model
{
    public class StaticDataModel
    {
        public MetaData MetaData;

        public ModuleData GetModuleData(EModuleType moduleType)
        {
            return MetaData.Modules.First(m => m.moduleType.Equals(moduleType));
        }
        
        public void SeedMetaData(MetaData metaData)
        {
            MetaData = metaData;
        }
    }
}

