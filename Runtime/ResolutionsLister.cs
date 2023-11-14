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
        }

        private void PopulateResolutionDropdown()
        {
            resolutionsDropdown.DropdownOptions.Options.Clear();

            var screenResolutions = Screen.resolutions;
            List<Resolution> filteredResolutions = FilterResolutions(screenResolutions);

            foreach (Resolution resolution in filteredResolutions)
            {
                string res = resolution.width + " x " + resolution.height;

                resolutionsDropdown.DropdownOptions.Options.Add(res);
            }

            GraphicsOptions.resolutions = filteredResolutions.ToList();
        }

        List<Resolution> FilterResolutions(Resolution[] resolutions)
        {
            List<Resolution> filteredList = new List<Resolution>();

            foreach (Resolution resolution in resolutions)
            {
                if (!filteredList.Contains(resolution))
                {
                    if (Mathf.Approximately((float)resolution.width / resolution.height, 16f / 9f))
                    {
                        if (resolution.refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio))
                        {
                            filteredList.Add(resolution);
                        }
                    }
                }
            }

            return filteredList;
        }
    }
}