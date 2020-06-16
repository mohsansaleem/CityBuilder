using PG.Core.Installer;
using RSG;
using UnityEngine;
using Zenject;

namespace PG.Core.Command
{
    public class LoadUnloadScenesCommand : BaseCommand
    {
        [Inject] private readonly ISceneLoader _sceneLoader;

        //First loads an optional array of scenes, then 
        //unloads a separate optional array of scenes.
        //Each call to load/unload is added into a 
        //promise chain. When complete, the last promise 
        //fires an optional OnComplete delegate.
        public void Execute(LoadUnloadScenesSignal loadUnloadParams)
        {
            IPromise lastPromise = null;

            //Load scenes
            if (loadUnloadParams.LoadScenes != null)
            {
                for (int i = 0; i < loadUnloadParams.LoadScenes.Length; i++)
                {
                    string sceneName = loadUnloadParams.LoadScenes[i];
                    lastPromise = lastPromise != null ? lastPromise.Then(() => _sceneLoader.LoadScene(sceneName)) : _sceneLoader.LoadScene(sceneName);
                }
            }

            //Unload scenes
            if (loadUnloadParams.UnloadScenes != null)
            {
                for (int i = 0; i < loadUnloadParams.UnloadScenes.Length; i++)
                {
                    string sceneName = loadUnloadParams.UnloadScenes[i];
                    lastPromise = lastPromise != null ? lastPromise.Then(() => _sceneLoader.UnloadScene(sceneName)) : _sceneLoader.UnloadScene(sceneName);
                }
            }

            //Add promise to resolve OnComplete
            if (lastPromise != null)
            {
                lastPromise.Done(
                    () =>
                    {
                        Debug.Log(string.Format("{0} , scene loading/unloading completed!", this));

                        loadUnloadParams.OnComplete?.Resolve();
                    },
                    exception =>
                    {
                        loadUnloadParams.OnComplete?.Reject(exception);
                    }
                );
            }
            else
            {
                Debug.Log($"{this} , no scenes loaded/unloaded!");

                loadUnloadParams.OnComplete?.Resolve();
            }
        }
    }
}

