using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace gdui.runtime
{
    public class GraphicsOptions : MonoBehaviour
    {
        [NotNull] public static List<Resolution> Resolutions { get; set; } = new();
        [NotNull] public static List<RefreshRate> RefreshRates { get; } = new();
        private RefreshRate selectedFrameRate;
        [SerializeField] private RefreshRateLister refreshRateLister;
        [SerializeField] private ResolutionsLister resolutionsLister;

        private void Awake()
        {
            selectedFrameRate = Screen.currentResolution.refreshRateRatio;
            refreshRateLister.GetAvailableRefreshRates();
            resolutionsLister.PopulateResolutionDropdown();
        }

        public void SetResolution(int resolutionIndex)
        {
            var selectedResolution = Resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreenMode,
                selectedFrameRate);
        }

        public void SetFrameRate(int frameRateIndex)
        {
            selectedFrameRate = RefreshRates[frameRateIndex];
            Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreenMode, selectedFrameRate);
        }

        public void ToggleVSync(bool isEnabled)
        {
            QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index, true);
        }

        public void SetFullScreen(bool fullscreen)
        {
            var mode = fullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
            Screen.SetResolution(Screen.width, Screen.height, mode, selectedFrameRate);
        }

        private void OnDestroy()
        {
            Resolutions.Clear();
            RefreshRates.Clear();
        }
    }
}