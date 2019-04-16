using Zenject;
using RSG;
using System;

namespace game.core.installer
{

    public class CoreSceneInstaller : MonoInstaller
    {
        private Type _lastValidOpenState = null;
        public Type LastValidOpenState { get { return _lastValidOpenState; } }

        public override void InstallBindings()
        {
            Container.Bind<CoreSceneInstaller>().FromInstance(this);
        }

        public void OnNewValidOpenState(Type openState)
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
