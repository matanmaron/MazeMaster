using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static GameManager Instance { get; private set; }
    
    [SerializeField] UIManager uiManager = null;
    [SerializeField] GameObject AllEnemies = null;
    [SerializeField] internal GameObject AudioObject = null;
    internal bool GamePaused = false;
    internal bool Cheater = false;
    internal bool CheatInvulnerable = false;

    bool invulnerable = false;
    float waitTime = 1f;
    int idkfa = 0;
    int iddqd = 0;

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
        uiManager.SetHealth(Data.Health);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnGamePaused();
        }
        IDKFA();
        IDDQD();
    }

    void IDDQD()
    {
        if (Input.GetKeyDown(KeyCode.I) && iddqd == 0)
        {
            iddqd++;
        }
        else if (Input.GetKeyDown(KeyCode.D) && iddqd == 1)
        {
            iddqd++;
        }
        else if (Input.GetKeyDown(KeyCode.D) && iddqd == 2)
        {
            iddqd++;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && iddqd == 3)
        {
            iddqd++;
        }
        else if (Input.GetKeyDown(KeyCode.D) && iddqd == 4)
        {
            Debug.Log("IDDQD");
            CheatInvulnerable = !CheatInvulnerable;
            Cheater = true;
            uiManager.SetHealth(0);
            iddqd = 0;
        }
        else if (Input.anyKeyDown)
        {
            iddqd = 0;
        }
    }

    void IDKFA()
    {
        if (Input.GetKeyDown(KeyCode.I) && idkfa == 0)
        {
            idkfa++;
        }
        else if(Input.GetKeyDown(KeyCode.D) && idkfa == 1)
        {
            idkfa++;
        }
        else if (Input.GetKeyDown(KeyCode.K) && idkfa == 2)
        {
            idkfa++;
        }
        else if (Input.GetKeyDown(KeyCode.F) && idkfa == 3)
        {
            idkfa++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && idkfa == 4)
        {
            Debug.Log("IDKFA");
            Data.Keys = new List<KeysEnum>(){ KeysEnum.Blue, KeysEnum.Green, KeysEnum.Red, KeysEnum.Yellow};
            Cheater = true;
            uiManager.SetKeys(Data.Keys);
            uiManager.SetScore(0);
            idkfa = 0;
        }
        else if (Input.anyKeyDown)
        {
            idkfa = 0;
        }
    }

    public void GameResume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; AllEnemies.SetActive(true);
        GamePaused = false;
        uiManager.GameResume();
    }

    void GamePause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AllEnemies.SetActive(false);
        GamePaused = true;
        uiManager.GamePause();
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

    public void Menu()
    {
        SceneManager.LoadScene(0);
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
        Cursor.visible = true;
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
            if (!invulnerable)
            {
                if (!CheatInvulnerable)
                {
                    Data.Health -= hit;
                }
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
            invulnerable = true;
            uiManager.ShowBlood(true);
            yield return new WaitForSeconds(waitTime);
            uiManager.ShowBlood(false);
            invulnerable = false;
        }
    }
}