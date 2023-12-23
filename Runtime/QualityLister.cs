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
            GenerateDropdownOptions();
        }

        private void GenerateDropdownOptions()
        {
            var qualityLevels = QualitySettings.names;
            qualityDropdown.DropdownOptions.Options = qualityLevels.ToList();
        }
    }
}