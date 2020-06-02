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
        TxtScore.text = score.ToString();
    }

    internal void GamePause()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    internal void GameResume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
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
        TxtHealth.text = health.ToString();
    }

    IEnumerable FadeImg(Graphic img, float alpha, float duration, Action callback)
    {
        Color curr = img.color;
        Color visible = img.color;
        visible.a = alpha;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            img.color = Color.Lerp(curr, visible, counter / duration);
            yield return null;
        }

        if (callback != null)
        {
            callback();
        }
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
