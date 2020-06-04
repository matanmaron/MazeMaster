using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MusicText = null;
    [SerializeField] TextMeshProUGUI SFXText = null;
    [SerializeField] TextMeshProUGUI InvertMouse = null;
    [SerializeField] GameObject MenuPanel = null;
    [SerializeField] GameObject SettingsPanel = null;
    [SerializeField] Slider MouseSensitivitySlider = null;
    [SerializeField] TextMeshProUGUI MouseSensitivitySliderText = null;

    private void Start()
    {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        if (Settings.MuteMusic)
        {
            MusicText.text = "Music: OFF";
        }
        else
        {
            MusicText.text = "Music: ON";
        }
        if (Settings.MuteSFX)
        {
            SFXText.text = "SFX: OFF";
        }
        else
        {
            SFXText.text = "SFX: ON";
        }
        if (Settings.Invert)
        {
            InvertMouse.text = "Invert Mouse: ON";
        }
        else
        {
            InvertMouse.text = "Invert Mouse: OFF";
        }
        MouseSensitivitySliderText.text = $"Mouse Sensitivity: {Settings.MouseSensitivity}";
        MouseSensitivitySlider.value = Settings.MouseSensitivity;
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnAboutButton()
    {
        Application.OpenURL("https://matanmaron.wixsite.com/home/about");
    }

    public void OnMusic()
    {
        if (Settings.MuteMusic)
        {
            Settings.MuteMusic = false;
            MusicText.text = "Music: ON";
        }
        else
        {
            Settings.MuteMusic = true;
            MusicText.text = "Music: OFF";
        }
    }

    public void OnSFX()
    {
        if (Settings.MuteSFX)
        {
            Settings.MuteSFX = false;
            SFXText.text = "SFX: ON";
        }
        else
        {
            Settings.MuteSFX = true;
            SFXText.text = "SFX: OFF";
        }
    }

    public void OnMouseSensitivity()
    {
        Settings.MouseSensitivity = MouseSensitivitySlider.value;
        MouseSensitivitySliderText.text = $"Mouse Sensitivity: {Settings.MouseSensitivity:0.0}";
    }

    public void OnInvertMouse()
    {
        if (Settings.Invert)
        {
            Settings.Invert = false;
            InvertMouse.text = "Invert Mouse: OFF";
        }
        else
        {
            Settings.Invert = true;
            InvertMouse.text = "Invert Mouse: ON";
        }
    }

    public void OnSettings()
    {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void OnSettingsBack()
    {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
}
