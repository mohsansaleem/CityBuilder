using UnityEngine;
using UnityEngine.UI;

namespace PG.City.Context.Bootstrap
{
    public class BootstrapView : MonoBehaviour
    {
        [Header("References")]
        public Slider ProgressBar;

        public void SetProgress(float progress)
        {
            ProgressBar.value = progress;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

