using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] UIManager uiManager = null;

    internal bool GamePaused = false;

    bool Invulnerable = false;
    float waitTime = 1f;

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
        Cursor.lockState = CursorLockMode.Locked;
        GamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnGamePaused();
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

    internal void HitWalls(Collision collision)
    {
        var objName = collision.gameObject.name;
        if (objName.Contains("Yellow"))
        {
            if (Data.Keys.Contains(KeysEnum.Yellow))
            {
                Data.Keys.Remove(KeysEnum.Yellow);
            }
        }
        else if (objName.Contains("Red"))
        {
            if (Data.Keys.Contains(KeysEnum.Red))
            {
                Data.Keys.Remove(KeysEnum.Red);
            }
        }
        else if (objName.Contains("Green"))
        {
            if (Data.Keys.Contains(KeysEnum.Green))
            {
                Data.Keys.Remove(KeysEnum.Green);
            }
        }
        else if (objName.Contains("Blue"))
        {
            if (Data.Keys.Contains(KeysEnum.Blue))
            {
                Data.Keys.Remove(KeysEnum.Blue);
            }
        }
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
            collision.gameObject.SetActive(false);
        }
        else if (objName.Contains("Red"))
        {
            if (!Data.Keys.Contains(KeysEnum.Red))
            {
                Data.Keys.Add(KeysEnum.Red);
            }
            collision.gameObject.SetActive(false);
        }
        else if (objName.Contains("Green"))
        {
            if (!Data.Keys.Contains(KeysEnum.Green))
            {
                Data.Keys.Add(KeysEnum.Green);
            }
            collision.gameObject.SetActive(false);
        }
        else if (objName.Contains("Blue"))
        {
            if (!Data.Keys.Contains(KeysEnum.Blue))
            {
                Data.Keys.Add(KeysEnum.Blue);
            }
            collision.gameObject.SetActive(false);
        }
        uiManager.SetKeys(Data.Keys);
    }

    internal void Hit(Collision collision)
    {
        var objName = collision.gameObject.name;
        if (objName.Contains("Zombie"))
        {
            HitEnemy();
        }
        else if (objName.Contains("Key"))
        {
            //PickUpAnim();
            HitKeys(collision);
            //collision.gameObject.GetComponentInParent<AudioSource>().Play();

        }
        else if (objName.Contains("Wall"))
        {
            HitWalls(collision);
        }
        else if (objName.Contains("Saw"))
        {
            DamagePlayer(40);
        }
        else if (objName.Contains("Score"))
        {
            //PickUpAnim();
            Data.Score++;
            uiManager.SetScore(Data.Score);
            //collision.gameObject.GetComponentInParent<AudioSource>().Play();
            //collision.gameObject.GetComponent<ScorePointScript>().PickUp();
        }
        else if (objName.Contains("Finish"))
        {
            //PickUpAnim();
            uiManager.WinGame();
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void HitEnemy()
    {
        DamagePlayer(15);
    }

    private void DamagePlayer(int hit)
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
            //FadeImg(BloodScreen, 1, 3, callback());
            yield return new WaitForSeconds(waitTime);
            uiManager.ShowBlood(false);
            Invulnerable = false;
        }
    }
}