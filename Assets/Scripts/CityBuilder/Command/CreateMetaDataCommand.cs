using Zenject;
using System;
using PG.CityBuilder.Context.Bootstrap;
using PG.CityBuilder.Model.Context;
using PG.Core.Command;

namespace PG.CityBuilder.Command
{
    public class CreateMetaDataCommand : RemoteCommand
    {
        [Inject] private readonly BootstrapModel _bootstrapModel;

        public void Execute(CreateMetaDataSignal commandParams)
        {
            try
            {
                Service.CreateMetaData(commandParams.MetaData)
                    .Catch(commandParams.OnMetaCreated.Reject)
                    .Done(commandParams.OnMetaCreated.Resolve);
            }
            catch (Exception ex)
            {
                commandParams.OnMetaCreated.Reject(ex);
            }
        }
    }
}