using System.Linq;
using PG.City.Context;
using PG.City.Model.data;
using UniRx;
using UnityEngine;
using Zenject;

namespace PG.City.Model.remote
{
    public class RemoteDataModel
    {
        [Inject] private readonly StaticDataModel _staticDataModel;
        [Inject] private ModuleRemoteDataModel.Factory _moduleFactory;

        public UserData UserData { get; private set; }

        public readonly ReactiveProperty<long> Gold;
        public readonly ReactiveProperty<long> Wood;
        public readonly ReactiveProperty<long> Iron;
        
        public readonly ReactiveDictionary<long, ModuleRemoteDataModel> ModuleRemoteDatas;

        public RemoteDataModel()
        {
            Gold = new ReactiveProperty<long>(0);
            Wood = new ReactiveProperty<long>(0);
            Iron = new ReactiveProperty<long>(0);
            
            ModuleRemoteDatas = new ReactiveDictionary<long, ModuleRemoteDataModel>();
        }

        public void SeedUserData(UserData userData)
        {
            UserData = userData;

            foreach (var module in userData.ModulesInGrid)
            {
                AddModuleRemoteData(module);
            }
            
            Gold.Value = userData.Gold;
            Wood.Value = userData.Wood;
            Iron.Value = userData.Iron;
        }

        public void AddModuleRemoteData(ModuleRemoteData module)
        {
            // Seeding the Meta to GameState Instances.
            module.GridEntityData =
                _staticDataModel.MetaData.Modules.First(a => a.moduleType.Equals(module.moduleType));
            
            // Creating Models respecting to GameStateEntries.
            var tmp = _moduleFactory.Create();
                
            // Seeding the GameStateData to the in-memory Model.
            tmp.SeedModuleRemoteData(module);
                
            // Adding it to the Modules List.
            ModuleRemoteDatas.Add(module.Id, tmp);
        }

        public bool OccupySpaceForModule(ModuleData moduleData, out Vector3 pos)
        {   
            for (int wi = 0; wi < UserData.Grid.Count; wi++)
            {
                for (int li = 0; li < UserData.Grid[wi].Row.Length; li++)
                {
                    if (IsEmptyForModule(wi, li, moduleData.Width, moduleData.Length))
                    {
                        pos = new Vector3(wi*10, 0, -10 * li);
                        
                        MarkTheGrip(wi, li, moduleData.Width, moduleData.Length);
                        
                        return true;
                    }
                }
            }
            
            pos = Vector3.one;
            
            return false;
        }

        public bool IsEmptyForModule(int wi, int li, int wm, int lm)
        {
            if (wi + wm > Constants.GridWidth || li + lm > Constants.GridLength)
            {
                return false;
            }
            
            for (int w = wi; w < wi + wm; w++)
            {
                for (int l = li; l < li + lm; l++)
                {
                    if (UserData.Grid[w][l])
                        return false;
                }
            }

            return true;
        }

        public void MarkTheGrip(int wi, int li, int wm, int lm)
        {
            for (int w = wi; w < wi + wm; w++)
            {
                for (int l = li; l < li + lm; l++)
                {
                    UserData.Grid[w][l] = true;
                }
            }
        }
    }
}

