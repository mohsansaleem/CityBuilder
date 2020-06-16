using PG.Core.thirdparty.RSG.Promise.v1._3._0._0;
using UnityEngine;

namespace PG.City.Context.Bootstrap
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
