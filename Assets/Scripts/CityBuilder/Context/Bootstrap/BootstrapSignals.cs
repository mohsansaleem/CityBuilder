using PG.City.Model.Data;
using RSG;
using Zenject;

namespace PG.City.Context.Bootstrap
{
    public class LoadStaticDataSignal
    {
        public Promise DataLoadPromise;

        public static Promise LoadStaticData(SignalBus signalBus)
        {
            LoadStaticDataSignal signal = new LoadStaticDataSignal {DataLoadPromise = new Promise()};

            signalBus.Fire(signal);

            return signal.DataLoadPromise;
        }
    }

    public class LoadUserDataSignal
    {
        public Promise DataLoadPromise;

        public static Promise LoadUserData(SignalBus signalBus)
        {
            LoadUserDataSignal signal = new LoadUserDataSignal {DataLoadPromise = new Promise()};

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
            CreateMetaDataSignal signal = new CreateMetaDataSignal {MetaData = metaData, OnMetaCreated = new Promise()};

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
            CreateUserDataSignal signal = new CreateUserDataSignal {UserData = userData, OnUserCreated = new Promise()};

            signalBus.Fire(signal);

            return signal.OnUserCreated;
        }
    }
}