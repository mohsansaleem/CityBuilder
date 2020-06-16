using PG.CityBuilder.Model.Data;
using UnityEngine;

namespace PG.CityBuilder.Context.Bootstrap
{
    [CreateAssetMenu(menuName = "Game/Default GameState")]
    public class DefaultGameState : ScriptableObject
    {
        [SerializeField]
        public UserData User;
    }
}