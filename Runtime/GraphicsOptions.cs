using System.Collections.Generic;
using UnityEngine;

namespace gdui.runtime
{
    public class GraphicsOptions : MonoBehaviour
    {
        public static List<Resolution> Resolutions = new List<Resolution>();
        public static List<RefreshRate> RefreshRates = new List<RefreshRate>();
        private RefreshRate currentFrameRate;

        private void Start()
        {
            currentFrameRate = Screen.currentResolution.refreshRateRatio;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution selectedResolution = Resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreenMode,
                currentFrameRate);
        }

        public void SetFrameRate(int frameRateIndex)
        {
            currentFrameRate = RefreshRates[frameRateIndex];

            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height,
                UnityEngine.Device.Screen.fullScreenMode, currentFrameRate);
        }

        public void ToggleVSync(bool isEnabled)
        {
            QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        }

        private void Update()
        {
            Debug.Log(QualitySettings.vSyncCount);
        }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index, true);
        }

        public void SetFullScreen(bool fullscreen)
        {
            FullScreenMode mode = fullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, mode,
                currentFrameRate);
        }

        private void OnDestroy()
        {
            Resolutions.Clear();
            RefreshRates.Clear();
        }
    }
}