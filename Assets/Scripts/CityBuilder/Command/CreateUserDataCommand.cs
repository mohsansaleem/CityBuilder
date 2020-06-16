using Zenject;
using System;
using PG.City.Context.Bootstrap;
using PG.City.Model.scene;
using PG.Core.command;

namespace PG.City.Command
{
    public class CreateUserDataCommand : RemoteCommand
    {
        [Inject] private readonly BootstrapModel _bootstrapModel;

        public void Execute(CreateUserDataSignal commandParams)
        {
            try
            {
               Service.SaveUserData(commandParams.UserData)
                   .Catch(commandParams.OnUserCreated.Reject)
                   .Done(commandParams.OnUserCreated.Resolve);
            }
            catch(Exception ex)
            {
                commandParams.OnUserCreated.Reject(ex);
            }
        }
    }
}
