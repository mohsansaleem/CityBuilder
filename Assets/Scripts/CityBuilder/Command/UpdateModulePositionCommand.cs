using Zenject;
using System;
using PG.City.Context.Gameplay;
using PG.City.Model.remote;
using PG.City.Model.scene;
using PG.Core.command;

namespace PG.City.Command
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