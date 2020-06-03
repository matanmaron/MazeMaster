using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static GameManager Instance { get; private set; }
    
    [SerializeField] UIManager uiManager = null;
    [SerializeField] GameObject Top = null;
    [SerializeField] internal GameObject AudioObject = null;
    internal bool GamePaused = false;
    internal bool Cheater = false;

    bool Invulnerable = false;
    float waitTime = 1f;
    int cheat = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
#if !UNITY_EDITOR
        Top.SetActive(true);
#endif
        uiManager.SetHealth(Data.Health);
        Cursor.lockState = CursorLockMode.Locked;
        GamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnGamePaused();
        }
        IDKFA();
    }

    private void IDKFA()
    {
        if (Input.GetKeyDown(KeyCode.I) && cheat == 0)
        {
            cheat++;
        }
        else if(Input.GetKeyDown(KeyCode.D) && cheat == 1)
        {
            cheat++;
        }
        else if (Input.GetKeyDown(KeyCode.K) && cheat == 2)
        {
            cheat++;
        }
        else if (Input.GetKeyDown(KeyCode.F) && cheat == 3)
        {
            cheat++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && cheat == 4)
        {
            Debug.Log("IDKFA");
            Data.Keys = new List<KeysEnum>(){ KeysEnum.Blue, KeysEnum.Green, KeysEnum.Red, KeysEnum.Yellow};
            Cheater = true;
            uiManager.SetKeys(Data.Keys);
            uiManager.SetScore(0);
            cheat = 0;
        }
        else if (Input.anyKeyDown)
        {
            cheat = 0;
        }
    }

    void GameResume()
    {
        GamePaused = false;
        uiManager.GameResume();
    }

    void GamePause()
    {
        GamePaused = true;
        uiManager.GameResume();
    }

    internal void OnGamePaused()
    {
        if (GamePaused)
        {
            GameResume();
        }
        else
        {
            GamePause();
        }
    }

    internal void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }

    internal void Quit()
    {
        Application.Quit();
    }

    internal void HitDoors(Collision collision)
    {
        var objName = collision.gameObject.name;
        if (objName.Contains("Yellow"))
        {
            if (Data.Keys.Contains(KeysEnum.Yellow))
            {
                Data.Keys.Remove(KeysEnum.Yellow);
                Destroy(collision.gameObject, 0.01f);
            }
        }
        else if (objName.Contains("Red"))
        {
            if (Data.Keys.Contains(KeysEnum.Red))
            {
                Data.Keys.Remove(KeysEnum.Red);
                Destroy(collision.gameObject, 0.01f);
            }
        }
        else if (objName.Contains("Green"))
        {
            if (Data.Keys.Contains(KeysEnum.Green))
            {
                Data.Keys.Remove(KeysEnum.Green);
                Destroy(collision.gameObject, 0.01f);
            }
        }
        else if (objName.Contains("Blue"))
        {
            if (Data.Keys.Contains(KeysEnum.Blue))
            {
                Data.Keys.Remove(KeysEnum.Blue);
                Destroy(collision.gameObject, 0.01f);
            }
        }
        uiManager.SetKeys(Data.Keys);
    }

    internal void HitKeys(Collision collision)
    {
        var objName = collision.gameObject.name;
        if (objName.Contains("Yellow"))
        {
            if (!Data.Keys.Contains(KeysEnum.Yellow))
            {
                Data.Keys.Add(KeysEnum.Yellow);
            }
            Destroy(collision.gameObject,0.01f);
        }
        else if (objName.Contains("Red"))
        {
            if (!Data.Keys.Contains(KeysEnum.Red))
            {
                Data.Keys.Add(KeysEnum.Red);
            }
            Destroy(collision.gameObject, 0.01f);
        }
        else if (objName.Contains("Green"))
        {
            if (!Data.Keys.Contains(KeysEnum.Green))
            {
                Data.Keys.Add(KeysEnum.Green);
            }
            Destroy(collision.gameObject, 0.01f);
        }
        else if (objName.Contains("Blue"))
        {
            if (!Data.Keys.Contains(KeysEnum.Blue))
            {
                Data.Keys.Add(KeysEnum.Blue);
            }
            Destroy(collision.gameObject, 0.01f);
        }
        uiManager.SetKeys(Data.Keys);
    }

    internal void HitSaw()
    {
        DamagePlayer(40);
    }

    internal void HitFinish(Collision collision)
    {
        uiManager.WinGame();
        Cursor.lockState = CursorLockMode.None;
        Destroy(collision.gameObject, 0.01f);
    }

    internal void HitScore(Collision collision)
    {
        Data.Score++;
        uiManager.SetScore(Data.Score);
        Destroy(collision.gameObject, 0.01f);
    }

    internal void HitEnemy()
    {
        DamagePlayer(15);
    }

    internal void DamagePlayer(int hit)
    {
        if (Data.Health > 0)
        {
            if (!Invulnerable)
            {
                Data.Health -= hit;
                if (Data.Health < 1)
                {
                    Data.Health = 0;
                    uiManager.ShowEnd();
                    Invoke("Reset", 3);
                }
                uiManager.SetHealth(Data.Health);
                //make Invulnerable for 2 sec
                StartCoroutine(PlayerState());
            }
        }
    }

    IEnumerator PlayerState()
    {
        if (Data.Health > 0)
        {
            Invulnerable = true;
            uiManager.ShowBlood(true);
            yield return new WaitForSeconds(waitTime);
            uiManager.ShowBlood(false);
            Invulnerable = false;
        }
    }
}