using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    public int Health;
    public int Score;
    public bool YellowKey;
    public bool RedKey;
    public bool GreenKey;
    public bool BlueKey;
    public Text TxtKeys; 
    public Text TxtHealth;
    public Text TxtScore;
    public GameObject End;
    public GameObject Win;
    public GameObject Ps;
    public GameObject BloodScreen;
    public Animator anim;
    private bool Invulnerable;
    private float waitTime = 1f;
    public bool GamePaused;
    private float Run = 1;
    private bool pickuping = false;
    private bool walking = false;
    
    [SerializeField] float Speed = 15;
    [SerializeField] float sensitivity = 10;
    [SerializeField] bool invert = false;
    [SerializeField] GameObject CameraHolder;
    float player_rotationX;
    float camera_minimumY = -20;
    float camera_maximumY = 40;
    float camera_rotationY;
    float camera_rotationX;
    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
        BloodScreen.SetActive(false);
        anim = GetComponent<Animator>();
        Invulnerable = false;
        Score = 0;
        End.SetActive(false);
        Win.SetActive(false);
        YellowKey = false;
        RedKey = false;
        GreenKey = false;
        BlueKey = false;
        TxtKeys.text = SetKeys();
        SetHealth();
        SetScore();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetScore()
    {
        TxtScore.text = Score.ToString();
    }
    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    private void GamePause()
    {
        Ps.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameResume()
    {
        Ps.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!GamePaused)
        {
            MovePlayer();
            MoveCamera();
        }
    }

    void MovePlayer()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        WalkAnim();
        transform.position += transform.right * Speed * inputX * Time.deltaTime * Run;
        transform.position += transform.forward * Speed * inputY * Time.deltaTime * Run;
        player_rotationX += Input.GetAxis("Mouse X") * sensitivity;
        transform.localEulerAngles = new Vector3(0, player_rotationX, 0);
    }

    void MoveCamera()
    {
        if (invert)
        {
            camera_rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        }
        else
        {
            camera_rotationY += Input.GetAxis("Mouse Y") * sensitivity;    
        }
        camera_rotationX += Input.GetAxis("Mouse X") * sensitivity;
        camera_rotationY = Mathf.Clamp(camera_rotationY, camera_minimumY, camera_maximumY);
        CameraHolder.transform.localEulerAngles = new Vector3(-camera_rotationY, camera_rotationX, 0);
        CameraHolder.transform.position = transform.position;
    }
    
    private void WalkAnim()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)
            //||Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)
            //||Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)
            ||Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!pickuping)
            {
                walking = true;
                anim.Play("walk");
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)
            //|| Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)
            //|| Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)
            || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (!pickuping)
            {
                walking = false;
                anim.Play("idle");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var objName = collision.gameObject.name;
        if (objName.Contains("Zombie"))
        {
            HitEnemy();
        }
        else if (objName.Contains("Key"))
        {
            PickUpAnim();
            HitKeys(objName);
            collision.gameObject.GetComponentInParent<AudioSource>().Play();
            collision.gameObject.SetActive(false);
            TxtKeys.text = SetKeys();

        }
        else if (objName.Contains("Wall"))
        {
            if (HitWalls(objName))
            {
                collision.gameObject.SetActive(false);
                TxtKeys.text = SetKeys();
            }
        }
        else if(objName.Contains("Saw"))
        {
            DamagePlayer(40);
        }
        else if(objName.Contains("Score"))
        {
            PickUpAnim();
            Score++;
            SetScore();
            collision.gameObject.GetComponentInParent<AudioSource>().Play();
            collision.gameObject.GetComponent<ScorePointScript>().PickUp();
        }
        else if (objName.Contains("Finish"))
        {
            PickUpAnim();
            //finishKey
            gameObject.SetActive(false);
            Win.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void PickUpAnim()
    {
        pickuping = true;
        anim.Play("pickup");
        Invoke("PickUpAnimOff", 1);
    }

    void PickUpAnimOff()
    {
        pickuping = false;
        if (walking)
        {
            anim.Play("walk");
        }
    }


    private void HitEnemy()
    {
        DamagePlayer(15);
    }

    private void DamagePlayer(int hit)
    {
        if (Health > 0)
        {
            if (!Invulnerable)
            {
                Health -= hit;
                if (Health < 1)
                {
                    Health = 0;
                    print("dead");
                    End.SetActive(true);
                    Invoke("Reset", 3);
                    gameObject.SetActive(false);
                }
                SetHealth();
                //make Invulnerable for 2 sec
                StartCoroutine(PlayerState());
            }
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private bool HitWalls(string objName)
    {
        if (objName.Contains("Yellow"))
        {
            if (YellowKey == true)
            {
                YellowKey = false;
                return true;
            }
        }
        else if (objName.Contains("Red"))
        {
            if (RedKey == true)
            {
                RedKey = false;
                return true;
            }
        }
        else if (objName.Contains("Green"))
        {
            if (GreenKey == true)
            {
                GreenKey = false;
                return true;
            }
        }
        else if (objName.Contains("Blue"))
        {
            if (BlueKey == true)
            {
                BlueKey = false;
                return true;
            }
        }
        return false;
    }

    private void HitKeys(string objName)
    {

            if (objName.Contains("Yellow"))
            {
                YellowKey = true;
            }
            else if (objName.Contains("Red"))
            {
                RedKey = true;
            }
            else if (objName.Contains("Green"))
            {
                GreenKey = true;
            }
            else if (objName.Contains("Blue"))
            {
                BlueKey = true;
            }
    }

    private string SetKeys()
    {
        const string yellow = "<color=yellow> KEY </color>";
        const string red = "<color=red> KEY </color>";
        const string green = "<color=green> KEY </color>";
        const string blue = "<color=blue> KEY </color>";
        string txt = "Keys: ";
        if (YellowKey)
        {
            txt += yellow;
        }
        if (RedKey)
        {
            txt += red;
        }
        if (GreenKey)
        {
            txt += green;
        }
        if (BlueKey)
        {
            txt += blue;
        }
        return txt;
    }

    private void SetHealth()
    {
        TxtHealth.text = Health.ToString();
    }

    IEnumerator PlayerState()
    {
        if (Health > 0)
        {
            //StartCoroutine(PlayerState());
            Invulnerable = true;

            BloodScreen.SetActive(true);
            //FadeImg(BloodScreen, 1, 3, callback());
            yield return new WaitForSeconds(waitTime);
            BloodScreen.SetActive(false);

            Invulnerable = false;
        }
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
}
