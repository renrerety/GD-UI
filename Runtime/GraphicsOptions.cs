using System.Collections.Generic;
using UnityEngine;

namespace gdui.runtime
{
    public class GraphicsOptions : MonoBehaviour
    {
        public static List<Resolution> Resolutions = new List<Resolution>();
        public static List<RefreshRate> RefreshRates = new List<RefreshRate>();

        public void SetResolution(int resolutionIndex)
        {
            Resolution selectedResolution = Resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }

        public void SetFrameRate(int frameRateIndex)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height,
                FullScreenMode.ExclusiveFullScreen,
                RefreshRates[frameRateIndex]);
        }

        private void OnDestroy()
        {
            Resolutions.Clear();
            RefreshRates.Clear();
        }
    }
}