using System.Collections.Generic;
using UnityEngine;

namespace gdui.runtime
{
    public class RefreshRateLister : MonoBehaviour
    {
        [SerializeField] private DynamicDropdown refreshRateDropdown;

        public void GetAvailableRefreshRates()
        {
            var resolutions = Screen.resolutions;
            var refreshRatesSet = new HashSet<RefreshRate>();

            foreach (var resolution in resolutions)
            {
                if (resolution.width != Screen.currentResolution.width ||
                    resolution.height != Screen.currentResolution.height) continue;
                AddToLists(refreshRatesSet, resolution);
            }
        }

        private void AddToLists(ISet<RefreshRate> refreshRatesSet, Resolution resolution)
        {
            refreshRatesSet.Add(resolution.refreshRateRatio);
            GraphicsOptions.RefreshRates.Add(resolution.refreshRateRatio);
            refreshRateDropdown.DropdownOptions.Options.Add(resolution.refreshRateRatio.ToString());
        }
    }
}