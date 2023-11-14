using System.Collections.Generic;
using UnityEngine;

namespace gdui.runtime
{
    public class GraphicsOptions : MonoBehaviour
    {
        public static List<Resolution> resolutions = new List<Resolution>();

        public void SetResolution(int resolutionIndex)
        {
            Resolution selectedResolution = resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }

        private void OnDestroy()
        {
            resolutions.Clear();
        }
    }
}