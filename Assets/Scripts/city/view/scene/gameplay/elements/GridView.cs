using System;
using UnityEngine;

namespace game.city.view
{   
    public class GridView : MonoBehaviour
    {
        public Action OnMouseDownAtGrid;
        
        private void OnMouseDown()
        {
            OnMouseDownAtGrid?.Invoke();
        }
    }
}