using System;
using NovaSamples.UIControls;
using UnityEngine;

namespace gdui.runtime
{
    public class SaveSettings : MonoBehaviour
    {
        [Header("Graphics")] private int refreshRateIndex;
        private int resolutionIndex;
        private int qualityIndex;
        private bool IsFullscreen;
        private bool isVSyncEnabled;

        [Header("Audio")] private float mainVol;
        private float musicVol;
        private float SFXVol;

        [Header("UI Components")] [SerializeField]
        private DynamicDropdown refreshRateDropdown;

        [SerializeField] private DynamicDropdown resolutionDropdown;
        [SerializeField] private DynamicDropdown qualityDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private Slider mainVolSlider;
        [SerializeField] private Slider musicVolSlider;
        [SerializeField] private Slider sfxVolSlider;
        private GraphicsOptions graphicsOptions;


        private void Awake()
        {
            graphicsOptions = GetComponentInChildren<GraphicsOptions>(true);
        }

        private void Start()
        {
            LoadSettings();
        }

        public void StoreSettings()
        {
            PlayerPrefs.SetInt("refreshRate", refreshRateDropdown.DropdownOptions.SelectedIndex);
            PlayerPrefs.SetInt("qualityIndex", QualitySettings.GetQualityLevel());
            PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.ToggledOn ? 1 : 0);
            PlayerPrefs.SetInt("VSync", QualitySettings.vSyncCount);
            PlayerPrefs.SetFloat("mainVol", mainVolSlider.Value);
            PlayerPrefs.SetFloat("musicVol", musicVolSlider.Value);
            PlayerPrefs.SetFloat("SFXVol", sfxVolSlider.Value);
            PlayerPrefs.Save();
        }

        private void LoadSettings()
        {
            refreshRateIndex = PlayerPrefs.GetInt("refreshRate", 0);
            qualityIndex = PlayerPrefs.GetInt("qualityIndex", 0);
            IsFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
            isVSyncEnabled = PlayerPrefs.GetInt("VSync", 0) > 0;
            mainVol = PlayerPrefs.GetFloat("mainVol", 1f);
            musicVol = PlayerPrefs.GetFloat("musicVol", 1f);
            SFXVol = PlayerPrefs.GetFloat("SFXVol", 1f);
            ApplySettings();
        }

        private void ApplySettings()
        {
            ApplyQuality();
            ApplyRefreshRate();
            ApplyFullscreen();
            ApplyVSync();
            ApplyVolumeSliders();
        }

        private void ApplyVolumeSliders()
        {
            mainVolSlider.Value = mainVol;
            musicVolSlider.Value = musicVol;
            sfxVolSlider.Value = SFXVol;
        }

        private void ApplyVSync()
        {
            vSyncToggle.ToggledOn = isVSyncEnabled;
            graphicsOptions.ToggleVSync(isVSyncEnabled);
        }

        private void ApplyFullscreen()
        {
            fullscreenToggle.ToggledOn = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
        }

        private void ApplyQuality()
        {
            qualityDropdown.DropdownOptions.SelectedIndex = qualityIndex;
            qualityDropdown.RefreshCurrentSelection();
            graphicsOptions.SetQuality(qualityIndex);
        }

        private void ApplyRefreshRate()
        {
            refreshRateDropdown.DropdownOptions.SelectedIndex = refreshRateIndex;
            refreshRateDropdown.RefreshCurrentSelection();
            graphicsOptions.SetFrameRate(refreshRateIndex);
        }

        private void OnApplicationQuit()
        {
            StoreSettings();
        }
    }
}