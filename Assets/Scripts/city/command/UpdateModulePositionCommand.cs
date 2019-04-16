using Zenject;
using System;
using game.city.installer;
using game.city.model.remote;
using game.city.model.scene;
using game.core.command;

namespace game.city.command
{
    public class UpdateModulePositionCommand : RemoteCommand
    {
        [Inject] private RemoteDataModel _remoteDataModel;
        [Inject] private GamePlayModel _gamePlayModel;

        public void Execute(UpdateModulePositionSignal param)
        {
            try
            {
                Service.UpdateModulePosition(param.Model, param.Model.CurrentPosition.Value, param.Position)
                    .Catch(param.OnModuleAdded.Reject)
                    .Done(() =>
                    {
                        param.Model.RemoteData.CurrentPosition = param.Position;
                        param.Model.CurrentPosition.Value = param.Position;
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