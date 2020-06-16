using Zenject;
using System;
using PG.City.Context.Bootstrap;
using PG.City.Model.scene;
using PG.Core.command;

namespace PG.City.Command
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