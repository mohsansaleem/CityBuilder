using System;
using PG.CityBuilder.Model.Remote;
using PG.Core.PoolFactory;

namespace PG.CityBuilder.Context.Gameplay
{   
    public abstract class Movable : FactoryObject
    {
        public EntityRemoteDataModel Model;
        
        public Action<EntityRemoteDataModel> OnMouseDownAtModule;
        public Action<EntityRemoteDataModel> OnDragModule;
        public Action<EntityRemoteDataModel> OnMouseUpFromModule;
        
        private void OnMouseDown()
        {
            OnMouseDownAtModule?.Invoke(Model);
        }

        private void OnMouseDrag()
        {
            OnDragModule?.Invoke(Model);
        }

        private void OnMouseUp()
        {
            OnMouseUpFromModule?.Invoke(Model);
        }

        public override void OnDespawned()
        {
            base.OnDespawned();

            OnMouseDownAtModule = null;
            OnDragModule = null;
            OnMouseUpFromModule = null;
        }
    }
}