using System.Collections.Generic;
using UnityEngine;

namespace gdui.runtime
{
    public class RefreshRateLister : MonoBehaviour
    {
        private DynamicDropdown refreshRateDropdown;
        private List<int> availableRefreshRates = new List<int>();

        private void Awake()
        {
            refreshRateDropdown = GetComponent<DynamicDropdown>();
        }

        private void Start()
        {
            Resolution[] resolutions = Screen.resolutions;
            GetAvailableRefreshRates(resolutions);
        }

        private void GetAvailableRefreshRates(Resolution[] resolutions)
        {
            var refreshRatesSet = new HashSet<RefreshRate>();

            foreach (var resolution in resolutions)
            {
                if (refreshRatesSet.Contains(resolution.refreshRateRatio)) return;
                refreshRatesSet.Add(resolution.refreshRateRatio);
                GraphicsOptions.RefreshRates.Add(resolution.refreshRateRatio);
                refreshRateDropdown.DropdownOptions.Options.Add(resolution.refreshRateRatio.ToString());
            }
        }
    }
}