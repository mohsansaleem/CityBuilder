using Zenject;
using UnityEngine;
using System;
using game.city.installer;
using game.city.model.remote;
using game.core.command;

namespace game.city.command
{
    public class SaveGameCommand : RemoteCommand
    {
        [Inject] private RemoteDataModel _remoteDataModel;

        public void Execute(SaveGameSignal param)
        {
            try
            {
                Service.SaveUserData(_remoteDataModel.UserData);
            }
            catch(Exception ex)
            {
                Debug.LogError("Error while Resetting: "+ ex.ToString());
            }
        }
    }

}
