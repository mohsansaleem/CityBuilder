using System;
using Zenject;
using UniRx;

namespace game.core.view
{
    public abstract class Mediator : IInitializable, IDisposable
    {
        [Inject] protected readonly SignalBus SignalBus;

        protected readonly CompositeDisposable _disposables;

        protected Mediator()
        {
            _disposables = new CompositeDisposable();
        }

        public virtual void Initialize()
        {
            // Initialize stuff here.
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}

