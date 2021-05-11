using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{   
    [SerializeField] Dropdown qulityDrop;
    [SerializeField] Slider volSlider;
    [SerializeField] Toggle setFullScreen;
    Resolution [] resolutions;
    public AudioMixer audio;
    public Dropdown resolutionDropdown;
    private int screenInt;
    private bool isFullscreen = false;
    void Awake()
    {   
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Qulity"));
        screenInt = PlayerPrefs.GetInt("toggleState");
        if(screenInt == 1)
        {
            isFullscreen = true;
            setFullScreen.isOn = true;
        }
        else
        {
            setFullScreen.isOn = false;
        }
    }
    void Start()
    {
        volSlider.value = PlayerPrefs.GetFloat("MVolume");
        audio.SetFloat("VolumePar", PlayerPrefs.GetFloat("MVolume"));

        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentScreenResolution = 0;
        for(int i = 0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentScreenResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", currentScreenResolution);
        resolutionDropdown.RefreshShownValue();
    }
    
    public void SetVolume (float volume)
    {   
        PlayerPrefs.SetFloat("MVolume" , volume);
        audio.SetFloat("VolumePar" , PlayerPrefs.GetFloat("MVolume"));
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Qulity" , qualityIndex);
    }
    public void SetScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if(isFullscreen == false)
        {   
            screenInt = 0;
            PlayerPrefs.SetInt("toggleState" , screenInt);
        }
        else
        {   
            screenInt = 1;
            isFullscreen = true;
            PlayerPrefs.SetInt("toggleState" , screenInt);
        }
    }

    public void SetResolutions(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width , resolution.height , Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution" , resolutionIndex);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
