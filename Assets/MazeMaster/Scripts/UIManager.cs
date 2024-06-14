using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TxtKeys = null;
    [SerializeField] TextMeshProUGUI TxtHealth = null;
    [SerializeField] TextMeshProUGUI TxtScore = null;
    [SerializeField] GameObject End = null;
    [SerializeField] GameObject Win = null;
    [SerializeField] GameObject Pause = null;
    [SerializeField] GameObject BloodScreen = null;

    const string yellow = "<color=yellow> KEY </color>";
    const string red = "<color=red> KEY </color>";
    const string green = "<color=green> KEY </color>";
    const string blue = "<color=blue> KEY </color>";

    void Start()
    {
        BloodScreen.SetActive(false);
        End.SetActive(false);
        Win.SetActive(false);
        SetScore(0);
    }
    internal void SetScore(int score)
    {
        TxtScore.text = $"SCORE: {score}";
        if (GameManager.Instance.Cheater)
        {
            TxtScore.text = $"CHEATER !!";
        }
    }

    internal void GamePause()
    {
        Pause.SetActive(true);
    }

    internal void GameResume()
    {
        Pause.SetActive(false);
    }

    internal void SetKeys(List<KeysEnum> keys)
    {
        StringBuilder txt = new StringBuilder("Keys: ");
        foreach (var k in keys)
        {
            txt.Append(GetKey(k));
        }
        TxtKeys.text = txt.ToString();
    }

    private static string GetKey(KeysEnum key)
    {
        switch (key)
        {
            case KeysEnum.Red:
                return red;
            case KeysEnum.Green:
                return green;
            case KeysEnum.Blue:
                return blue;
            case KeysEnum.Yellow:
                return yellow;
        }
        return string.Empty;
    }

    internal void SetHealth(int health)
    {
        if (GameManager.Instance.CheatInvulnerable)
        {
            TxtHealth.text = $"HEALTH: <color=red>INVULNERABLE</color>";
            SetScore(0);
            return;
        }
        TxtHealth.text = $"HEALTH: {health}";
    }

    internal void WinGame()
    {
        Win.SetActive(true);
    }

    internal void ShowBlood(bool value)
    {
        BloodScreen.SetActive(value);
    }

    internal void ShowEnd()
    {
        End.SetActive(true);
    }
}
