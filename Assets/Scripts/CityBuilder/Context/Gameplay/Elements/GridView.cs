using System;
using UnityEngine;

namespace PG.CityBuilder.Context.Gameplay.elements
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