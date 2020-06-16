using PG.City.Model.Data;
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