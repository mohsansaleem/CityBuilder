﻿using Zenject;
using UnityEngine;
using System;
using PG.City.Context.Gameplay;
using PG.City.Model.Remote;
using PG.Core.Command;

namespace PG.City.Command
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
                Debug.LogError("Error while Resetting: "+ ex);
            }
        }
    }

}
