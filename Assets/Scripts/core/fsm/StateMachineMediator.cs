using System;
using System.Collections.Generic;
using PG.Core.Installer;
using UniRx;
using Zenject;

namespace PG.Core.FSM
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
                CurrentStateBehaviour?.OnStateExit();
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
            CurrentStateBehaviour?.Tick();
        }

        public virtual void Dispose()
        {
            SignalBus.Unsubscribe<RequestStateChangeSignal>(GoToState);

            CurrentStateBehaviour?.OnStateExit();

            Disposables.Dispose();

            StateBehaviours.Clear();
        }
    }
}
