using Zenject;
using System;
using RSG;

namespace game.core.installer
{
    public class RequestStateChangeSignal
    {
        public Type stateType;
    }


    public class LoadSceneSignal
    {
        public string Scene;
        public readonly Promise OnComplete;

        public LoadSceneSignal()
        {
            OnComplete = new Promise();
        }

        public static IPromise Load(SignalBus signalBus, string scene)
        {
            LoadSceneSignal loadParams = new LoadSceneSignal()
            {
                Scene = scene
            };
            
            signalBus.Fire(loadParams);

            return loadParams.OnComplete;
        }
    }

    public class LoadUnloadScenesSignal
    {
        public string[] LoadScenes;
        public string[] UnloadScenes;
        public Promise OnComplete;

        public LoadUnloadScenesSignal()
        {
            OnComplete = new Promise();
        }

        public static IPromise Load(SignalBus signalBus, string[] loadScenes)
        {
            LoadUnloadScenesSignal loadUnloadParams = new LoadUnloadScenesSignal()
            {
                LoadScenes = loadScenes,
                UnloadScenes = null
            };

            signalBus.Fire(loadUnloadParams);

            return loadUnloadParams.OnComplete;
        }

        public static IPromise Load(SignalBus signalBus, string loadScene)
        {
            LoadUnloadScenesSignal loadUnloadParams = new LoadUnloadScenesSignal()
            {
                LoadScenes = new[] { loadScene },
                UnloadScenes = null,
                OnComplete = new Promise()
            };

            signalBus.Fire(loadUnloadParams);

            return loadUnloadParams.OnComplete;
        }

        public static IPromise Unload(SignalBus signalBus, string[] unloadScenes)
        {
            LoadUnloadScenesSignal loadUnloadParams = new LoadUnloadScenesSignal()
            {
                LoadScenes = null,
                UnloadScenes = unloadScenes
            };

            signalBus.Fire(loadUnloadParams);

            return loadUnloadParams.OnComplete;
        }

        public static IPromise Unload(SignalBus signalBus, string unloadScene)
        {
            LoadUnloadScenesSignal loadUnloadParams = new LoadUnloadScenesSignal()
            {
                LoadScenes = null,
                UnloadScenes = new[] { unloadScene }
            };

            signalBus.Fire(loadUnloadParams);

            return loadUnloadParams.OnComplete;
        }

        public static IPromise LoadUnload(SignalBus signalBus, string[] loadScenes, string[] unloadScenes)
        {
            LoadUnloadScenesSignal loadUnloadParams = new LoadUnloadScenesSignal()
            {
                LoadScenes = loadScenes,
                UnloadScenes = unloadScenes
            };

            signalBus.Fire(loadUnloadParams);

            return loadUnloadParams.OnComplete;
        }

        public static IPromise LoadUnload(SignalBus signalBus, string loadScene, string unloadScene)
        {
            LoadUnloadScenesSignal loadUnloadParams = new LoadUnloadScenesSignal()
            {
                LoadScenes = new[] { loadScene },
                UnloadScenes = new[] { unloadScene }
            };

            signalBus.Fire(loadUnloadParams);

            return loadUnloadParams.OnComplete;
        }
    }

    public class UnloadSceneSignal
    {
        public string Scene;
        public readonly Promise OnComplete;

        public UnloadSceneSignal()
        {
            OnComplete = new Promise();
        }

        public static IPromise Unload(SignalBus signalBus, string scene)
        {
            UnloadSceneSignal unloadParams = new UnloadSceneSignal()
            {
                Scene = scene
            };

            signalBus.Fire(unloadParams);

            return unloadParams.OnComplete;
        }
    }

    public class UnloadAllScenesExceptSignal
    {
        public string Scene;
        public readonly Promise OnComplete;

        public UnloadAllScenesExceptSignal()
        {
            OnComplete = new Promise();
        }

        public static IPromise UnloadAllExcept(SignalBus signalBus, string scene)
        {
            UnloadAllScenesExceptSignal unloadParams = new UnloadAllScenesExceptSignal()
            {
                Scene = scene
            };

            signalBus.Fire(unloadParams);

            return unloadParams.OnComplete;
        }
    }
}