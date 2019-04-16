using UniRx;
using UnityEngine;

namespace game.city.model.remote
{
    public class EntityRemoteDataModel
    {
        public ReactiveProperty<Vector3> CurrentPosition;

        public EntityRemoteDataModel()
        {
            CurrentPosition = new ReactiveProperty<Vector3>();
        }
    }
}