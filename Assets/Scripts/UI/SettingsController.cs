using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    
    public float Sensitivity { get; private set; } = 1f;
    public float Volume { get; private set; } = 1f;
    
    private void Start()
    {
        var savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = Volume;
        SetVolume(savedVolume);

        var savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
        sensitivitySlider.value = Sensitivity;
        SetSensitivity(savedSensitivity);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
        Volume = volume;
    }
    
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        Sensitivity = sensitivity;
    }
}