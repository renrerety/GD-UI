using System.Collections.Generic;
using UnityEngine;

namespace gdui.runtime
{
    public class RefreshRateLister : MonoBehaviour
    {
        private DynamicDropdown refreshRateDropdown;

        private void Awake()
        {
            refreshRateDropdown = GetComponent<DynamicDropdown>();
        }

        private void Start()
        {
            GetAvailableRefreshRates();
            refreshRateDropdown.RefreshCurrentSelection();
        }

        private void GetAvailableRefreshRates()
        {
            var resolutions = Screen.resolutions;
            var refreshRatesSet = new HashSet<RefreshRate>();

            foreach (var resolution in resolutions)
            {
                if (resolution.width != Screen.currentResolution.width ||
                    resolution.height != Screen.currentResolution.height) continue;
                AddToLists(refreshRatesSet, resolution);
                CheckCurrentRefreshRate(resolution);
            }
        }

        private void AddToLists(HashSet<RefreshRate> refreshRatesSet, Resolution resolution)
        {
            refreshRatesSet.Add(resolution.refreshRateRatio);
            GraphicsOptions.RefreshRates.Add(resolution.refreshRateRatio);
            refreshRateDropdown.DropdownOptions.Options.Add(resolution.refreshRateRatio.ToString());
        }

        private void CheckCurrentRefreshRate(Resolution resolution)
        {
            if (resolution.refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio))
            {
                refreshRateDropdown.DropdownOptions.SelectedIndex =
                    refreshRateDropdown.DropdownOptions.Options.IndexOf(resolution.refreshRateRatio.ToString());
            }
        }
    }
}