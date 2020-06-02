using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MusicText = null;
    [SerializeField] TextMeshProUGUI SFXText = null;
    [SerializeField] GameObject MenuPanel = null;
    [SerializeField] GameObject SettingsPanel = null;

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
