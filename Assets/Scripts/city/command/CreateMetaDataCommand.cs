using Zenject;
using System;
using game.city.installer;
using game.city.model.scene;
using game.core.command;

namespace game.city.command
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