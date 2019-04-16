using game.city.model.data;
using UnityEngine;

namespace game.city.view
{
    [CreateAssetMenu(menuName = "Game/MetaData")]
    public class DefaultMetaData : ScriptableObject
    {
        [SerializeField]
        public MetaData Meta;
    }
}