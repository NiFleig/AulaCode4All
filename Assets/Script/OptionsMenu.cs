using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    Resolution[] availableRes;

    public TMP_Dropdown resDropdown;

    void Start()
    {
        availableRes = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> resOptions = new List<string>();

        int currentRes = 0;

        for(int i = 0; i < availableRes.Length; i++)
        {
            string option = availableRes[i].width + " x " + availableRes[i].height;
            resOptions.Add(option);

            if(availableRes[i].width == Screen.currentResolution.width && availableRes[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resDropdown.AddOptions(resOptions);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    public void ChangeResolution(int resolution)
    {
        Resolution selectedRes = availableRes[resolution];
        Screen.SetResolution(selectedRes.width, selectedRes.height, Screen.fullScreen);
    }

    public void ChangeGraphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void Fullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void ChangeMaster(float volume)
    {
        mixer.SetFloat("VolumeMaster", volume);   
    }

    public void ChangeMusic(float volume)
    {
        mixer.SetFloat("VolumeMusic", volume);   
    }

    public void ChangeSFX(float volume)
    {
        mixer.SetFloat("VolumeSFX", volume);   
    }
}
