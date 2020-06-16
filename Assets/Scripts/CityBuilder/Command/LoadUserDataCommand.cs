using Zenject;
using System;
using PG.CityBuilder.Context.Bootstrap;
using PG.CityBuilder.Model.Context;
using PG.CityBuilder.Model.Remote;
using PG.Core.Command;

namespace PG.CityBuilder.Command
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