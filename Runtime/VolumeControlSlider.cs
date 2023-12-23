using UnityEngine;
using UnityEngine.Audio;

namespace gdui.runtime
{
    public class VolumeControlSlider : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private AudioMixer audioMixer;

        public void OnValueChanged(float value)
        {
            audioMixer.SetFloat(key, -80 + value);
        }
    }
}