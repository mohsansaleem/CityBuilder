using System.Linq;
using game.city.model.data;

namespace game.city.model
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

