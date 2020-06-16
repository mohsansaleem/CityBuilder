using RSG;
using UnityEngine;

namespace PG.CityBuilder.Context.Bootstrap
{
    public class PromiseHandler : MonoBehaviour
    {
        public void Start()
        {
            Promise.UnhandledException += OnPromiseException;
        }

        public void OnDestroy()
        {
            Promise.UnhandledException -= OnPromiseException;
        }

        private void OnPromiseException(object sender, ExceptionEventArgs e)
        {
            Debug.LogException(e.Exception);
        }
    }
}
