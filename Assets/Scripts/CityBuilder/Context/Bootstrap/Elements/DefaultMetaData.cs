using PG.City.Model.data;
using UnityEngine;

namespace PG.City.Context.Bootstrap
{
    [CreateAssetMenu(menuName = "Game/MetaData")]
    public class DefaultMetaData : ScriptableObject
    {
        [SerializeField]
        public MetaData Meta;
    }
}