using game.city.model.data;
using RSG;
using Zenject;

namespace game.city.installer
{
    public class LoadStaticDataSignal
    {
        public Promise DataLoadPromise;

        public static Promise LoadStaticData(SignalBus signalBus)
        {
            LoadStaticDataSignal signal = new LoadStaticDataSignal();
            signal.DataLoadPromise = new Promise();

            signalBus.Fire(signal);

            return signal.DataLoadPromise;
        }
    }

    public class LoadUserDataSignal
    {
        public Promise DataLoadPromise;

        public static Promise LoadUserData(SignalBus signalBus)
        {
            LoadUserDataSignal signal = new LoadUserDataSignal();
            signal.DataLoadPromise = new Promise();

            signalBus.Fire(signal);

            return signal.DataLoadPromise;
        }
    }

    public class CreateMetaDataSignal
    {
        public MetaData MetaData;
        public Promise OnMetaCreated;

        public static Promise CreateMetaData(SignalBus signalBus, MetaData metaData)
        {
            CreateMetaDataSignal signal = new CreateMetaDataSignal();

            signal.MetaData = metaData;
            signal.OnMetaCreated = new Promise();

            signalBus.Fire(signal);

            return signal.OnMetaCreated;
        }
    }
    
    public class CreateUserDataSignal
    {
        public UserData UserData;
        public Promise OnUserCreated;

        public static Promise CreateUserData(SignalBus signalBus, UserData userData)
        {
            CreateUserDataSignal signal = new CreateUserDataSignal();

            signal.UserData = userData;
            signal.OnUserCreated = new Promise();

            signalBus.Fire(signal);

            return signal.OnUserCreated;
        }
    }
}