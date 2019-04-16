using UniRx;
using UnityEngine;

namespace game.core
{
    public class StateBehaviour
    {
        protected CompositeDisposable Disposables;

        public virtual void OnStateEnter()
        {
            Debug.Log(string.Format("{0} , OnStateEnter()", this));

            Disposables = new CompositeDisposable();
        }

        public virtual void OnStateExit()
        {
            Debug.Log(string.Format("{0} , OnStateExit()", this));

            Disposables.Dispose();
        }

        public virtual bool IsValidOpenState()
        {
            return false;
        }

        public virtual void Tick()
        {

        }
    }
}
