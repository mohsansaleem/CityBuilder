using Zenject;
using System;
using RSG;

namespace PG.Core.Installer
{

    public class CoreSceneInstaller : MonoInstaller
    {
        private int _lastValidOpenState = -1;
        public int LastValidOpenState { get { return _lastValidOpenState; } }

        public override void InstallBindings()
        {
            Container.Bind<CoreSceneInstaller>().FromInstance(this);
        }

        public void OnNewValidOpenState(int openState)
        {
            _lastValidOpenState = openState;
        }

        public virtual Type GetDefaultState()
        {
            return null;
        }

        public virtual IPromise Open()
        {
            Promise promise = new Promise();
            promise.Resolve();
            return promise;
        }

        public virtual IPromise Close()
        {
            Promise promise = new Promise();
            promise.Resolve();
            return promise;
        }

        public virtual void OnSceneWake()
        {

        }

        public virtual void OnSceneSleep()
        {
            
        }
    }
}
