using game.city.model.data;
using game.city.model.remote;
using RSG;
using UnityEngine;
using Zenject;

namespace game.city.installer
{
    public class SaveGameSignal { }

    public class UpdateModulePositionSignal
    {
        public ModuleRemoteDataModel Model;
        public Vector3 Position;
        public Promise OnModuleAdded;

        public static Promise UpdateModulePosition(SignalBus signalBus, ModuleRemoteDataModel moduleModel, Vector3 pos)
        {
            UpdateModulePositionSignal signal = new UpdateModulePositionSignal
            {
                Model = moduleModel, Position = pos, OnModuleAdded = new Promise()
            };

            signalBus.Fire(signal);

            return signal.OnModuleAdded;
        }
    }
    public class UnloadGroupSignal { }

    public class AddNewModuleSignal
    {
        public EModuleType ModuleType;
        public Promise OnModuleAdded;

        public static Promise AddModule(SignalBus signalBus, EModuleType moduleType)
        {
            AddNewModuleSignal signal = new AddNewModuleSignal
            {
                ModuleType = moduleType, OnModuleAdded = new Promise()
            };

            signalBus.Fire(signal);

            return signal.OnModuleAdded;
        }
    }
}