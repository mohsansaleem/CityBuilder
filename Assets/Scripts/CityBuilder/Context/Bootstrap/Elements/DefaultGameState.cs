using PG.City.Model.Data;
using UnityEngine;

namespace PG.City.Context.Bootstrap
{
    [CreateAssetMenu(menuName = "Game/Default GameState")]
    public class DefaultGameState : ScriptableObject
    {
        [SerializeField]
        public UserData User;
    }
}