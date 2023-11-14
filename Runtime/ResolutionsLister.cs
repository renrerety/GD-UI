using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace gdui.runtime
{
    public class ResolutionsLister : MonoBehaviour
    {
        private DynamicDropdown resolutionsDropdown;

        private void Awake()
        {
            resolutionsDropdown = GetComponent<DynamicDropdown>();
        }

        private void Start()
        {
            PopulateResolutionDropdown();
            resolutionsDropdown.RefreshCurrentSelection();
        }

        private void PopulateResolutionDropdown()
        {
            resolutionsDropdown.DropdownOptions.Options.Clear();

            var screenResolutions = Screen.resolutions;
            var filteredResolutions = FilterResolutions(screenResolutions);

            foreach (Resolution resolution in filteredResolutions)
            {
                AddResolutionOption(resolution, filteredResolutions);
            }

            GraphicsOptions.Resolutions = filteredResolutions.ToList();
        }

        private void AddResolutionOption(Resolution resolution, List<Resolution> filteredResolutions)
        {
            string res = resolution.width + " x " + resolution.height;

            resolutionsDropdown.DropdownOptions.Options.Add(res);
            if (resolution.Equals(Screen.currentResolution))
                resolutionsDropdown.DropdownOptions.SelectedIndex = filteredResolutions.IndexOf(resolution);
        }

        List<Resolution> FilterResolutions(Resolution[] resolutions)
        {
            var filteredList = new List<Resolution>();

            foreach (Resolution resolution in resolutions)
            {
                if (filteredList.Contains(resolution) || !IsAspectValid(resolution)) continue;
                if (IsRefreshRateValid(resolution))
                {
                    filteredList.Add(resolution);
                }
            }

            return filteredList;
        }

        private static bool IsAspectValid(Resolution resolution)
        {
            return Mathf.Approximately((float)resolution.width / resolution.height, 16f / 9f);
        }

        private static bool IsRefreshRateValid(Resolution resolution)
        {
            return resolution.refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio);
        }
    }
}