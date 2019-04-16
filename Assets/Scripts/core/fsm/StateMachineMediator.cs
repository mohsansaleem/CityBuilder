using System;
using System.Collections.Generic;
using game.core.installer;
using Zenject;
using UniRx;

namespace game.core
{
    public class StateMachineMediator : IInitializable, ITickable, IDisposable
    {
        protected StateBehaviour CurrentStateBehaviour;
        protected Dictionary<Type, StateBehaviour> StateBehaviours = new Dictionary<Type, StateBehaviour>();


        protected CompositeDisposable Disposables;

        [Inject] protected CoreSceneInstaller SceneInstaller;
        [Inject] protected readonly SignalBus SignalBus;

        public virtual void Initialize()
		{
		    Disposables = new CompositeDisposable();
            SignalBus.Subscribe<RequestStateChangeSignal>(GoToState);
        }

        public virtual void GoToState(RequestStateChangeSignal signal)
        {
            GoToState(signal.stateType);
        }

        public virtual void GoToState(Type stateType)
        {
            if (StateBehaviours.ContainsKey(stateType))
            {
                if (CurrentStateBehaviour != null)
                {
                    CurrentStateBehaviour.OnStateExit();
                }
                CurrentStateBehaviour = StateBehaviours[stateType];
                if (SceneInstaller != null && CurrentStateBehaviour.IsValidOpenState())
                {
                    SceneInstaller.OnNewValidOpenState(stateType);
                }
                CurrentStateBehaviour.OnStateEnter();
            }
        }

        public virtual void Tick()
        {
            if (CurrentStateBehaviour != null)
            {
                CurrentStateBehaviour.Tick();
            }
        }

        public virtual void Dispose()
        {
            SignalBus.Unsubscribe<RequestStateChangeSignal>(GoToState);

            if (CurrentStateBehaviour != null)
            {
                CurrentStateBehaviour.OnStateExit();
            }

            Disposables.Dispose();

            StateBehaviours.Clear();
        }
    }
}
