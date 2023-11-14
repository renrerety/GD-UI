using NovaSamples.UIControls;
using UnityEngine;

namespace gdui.runtime
{
    public class VsyncChecker : MonoBehaviour
    {
        private Toggle vsyncToggle;

        private void Awake()
        {
            vsyncToggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            vsyncToggle.ToggledOn = QualitySettings.vSyncCount == 1;
        }
    }
}