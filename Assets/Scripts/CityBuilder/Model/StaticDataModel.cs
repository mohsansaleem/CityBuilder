using System.Linq;
using PG.CityBuilder.Model.Data;

namespace PG.CityBuilder.Model
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

