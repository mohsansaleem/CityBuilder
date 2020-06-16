using PG.CityBuilder.Model.Data;
using UnityEngine;

namespace PG.CityBuilder.Context.Bootstrap
{
    [CreateAssetMenu(menuName = "Game/MetaData")]
    public class DefaultMetaData : ScriptableObject
    {
        [SerializeField]
        public MetaData Meta;
    }
}