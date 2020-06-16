using Zenject;
using System;
using PG.City.Context.Bootstrap;
using PG.City.Model.remote;
using PG.City.Model.scene;
using PG.Core.command;

namespace PG.City.Command
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