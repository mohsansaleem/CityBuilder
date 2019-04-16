using game.city.model.data;
using UnityEngine;

namespace game.city.view
{
    [CreateAssetMenu(menuName = "Game/Default GameState")]
    public class DefaultGameState : ScriptableObject
    {
        [SerializeField]
        public UserData User;
    }
}