using Zenject;
using System;
using game.city.installer;
using game.city.model;
using game.city.model.remote;
using game.city.model.scene;
using game.core.command;

namespace game.city.command
{
    public class AddNewModuleCommand : RemoteCommand
    {
        [Inject] private RemoteDataModel _remoteDataModel;
        [Inject] private StaticDataModel _staticDataModel;
        [Inject] private GamePlayModel _gamePlayModel;

        public void Execute(AddNewModuleSignal param)
        {
            try
            {
                Service.AddNewModule(param.ModuleType)
                    .Catch(param.OnModuleAdded.Reject)
                    .Done((moduleRemoteData) =>
                    {
                        // Adding the Module to the Remode Data Model.
                        _remoteDataModel.AddModuleRemoteData(moduleRemoteData);

                        var data = moduleRemoteData.ModuleData;
                        
                        // Updating the Resources
                        _remoteDataModel.Gold.Value -= data.CostGold;
                        _remoteDataModel.Wood.Value -= data.CostWood;
                        _remoteDataModel.Iron.Value -= data.CostIron;
                        
                        param.OnModuleAdded.Resolve();
                    });
            }
            catch (Exception ex)
            {
                param.OnModuleAdded.Reject(ex);
            }
        }
    }
}