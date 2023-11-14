using System;
using System.Linq;
using UnityEngine;

namespace gdui.runtime
{
    public class QualityLister : MonoBehaviour
    {
        private DynamicDropdown qualityDropdown;

        private void Awake()
        {
            qualityDropdown = GetComponent<DynamicDropdown>();
        }

        private void Start()
        {
            GenerateDropdownOptions();
        }

        private void GenerateDropdownOptions()
        {
            var qualityLevels = QualitySettings.names;
            qualityDropdown.DropdownOptions.Options = qualityLevels.ToList();
            foreach (var qualityLevel in qualityLevels)
            {
                if (!qualityLevel.Equals(QualitySettings.names[QualitySettings.GetQualityLevel()])) continue;
                qualityDropdown.DropdownOptions.SelectedIndex = qualityLevels.ToList().IndexOf(qualityLevel);
            }

            qualityDropdown.RefreshCurrentSelection();
        }
    }
}