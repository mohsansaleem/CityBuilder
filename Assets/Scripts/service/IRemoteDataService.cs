using game.city.model.data;
using game.city.model.remote;
using RSG;
using UnityEngine;

namespace game.service
{
    public interface IRemoteDataService
    {
        IPromise CreateMetaData(MetaData metaData);
        IPromise<MetaData> GetMetaData();
        IPromise SaveUserData(UserData userData);
        IPromise<UserData> GetUserData();
        IPromise<ModuleRemoteData> AddNewModule(EModuleType moduleType);
        IPromise UpdateModulePosition(ModuleRemoteDataModel moduleModel, Vector3 oldPos, Vector3 newPos);
    }
}