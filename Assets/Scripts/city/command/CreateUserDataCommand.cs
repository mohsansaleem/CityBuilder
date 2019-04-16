using Zenject;
using System;
using game.city.installer;
using game.city.model.scene;
using game.core.command;

namespace game.city.command
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
