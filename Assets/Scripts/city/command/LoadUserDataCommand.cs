using Zenject;
using System;
using game.city.installer;
using game.city.model.remote;
using game.city.model.scene;
using game.core.command;

namespace game.city.command
{
    public class LoadUserDataCommand : RemoteCommand
    {
        [Inject] private RemoteDataModel _remoteDataModel;
        [Inject] private readonly BootstrapModel _bootstrapModel;

        public void Execute(LoadUserDataSignal loadParam)
        {
            try
            {
                Service.GetUserData()
                    .Catch(loadParam.DataLoadPromise.Reject)
                    .Done(userDate =>
                    {
                        _remoteDataModel.SeedUserData(userDate);
                        loadParam.DataLoadPromise.Resolve();
                    });
            }
            catch (Exception ex)
            {
                loadParam.DataLoadPromise.Reject(ex);
            }
        }
    }
}