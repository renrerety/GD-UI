using NovaSamples.UIControls;
using UnityEngine;

namespace gdui.runtime
{
    public class FullscreenChecker : MonoBehaviour
    {
        private Toggle fullscreenToggle;

        private void Awake()
        {
            fullscreenToggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            fullscreenToggle.ToggledOn = Screen.fullScreen;
        }
    }
}