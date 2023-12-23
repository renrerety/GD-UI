using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace gdui.runtime
{
    public class ResolutionsLister : MonoBehaviour
    {
        [SerializeField] private DynamicDropdown resolutionsDropdown;

        public void PopulateResolutionDropdown()
        {
            resolutionsDropdown.DropdownOptions.Options.Clear();

            var screenResolutions = Screen.resolutions;
            var filteredResolutions = FilterResolutions(screenResolutions);

            foreach (var resolution in filteredResolutions)
            {
                AddResolutionOption(resolution, filteredResolutions);
            }

            GraphicsOptions.Resolutions = filteredResolutions.ToList();
        }

        private void AddResolutionOption(Resolution resolution, IList<Resolution> filteredResolutions)
        {
            var res = resolution.width + " x " + resolution.height;

            resolutionsDropdown.DropdownOptions.Options.Add(res);

            if (resolution.width == Screen.width && resolution.height == Screen.height)
            {
                resolutionsDropdown.DropdownOptions.SelectedIndex = filteredResolutions.IndexOf(resolution);
            }

            resolutionsDropdown.RefreshCurrentSelection();
        }

        List<Resolution> FilterResolutions(IEnumerable<Resolution> resolutions)
        {
            var filteredList = new List<Resolution>();

            foreach (var resolution in resolutions)
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