using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] float Speed = 15;
    [SerializeField] float sensitivity = 10;
    [SerializeField] bool invert = false;
    [SerializeField] GameObject CameraHolder = null;

    bool pickuping = false;
    bool walking = false;
    float player_rotationX;
    float camera_minimumY = -20;
    float camera_maximumY = 40;
    float camera_rotationY = 0;
    float camera_rotationX = 0;
    Animator anim = null;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.GamePaused)
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
        transform.position += transform.right * Speed * inputX * Time.deltaTime;
        transform.position += transform.forward * Speed * inputY * Time.deltaTime;
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
            ||Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!pickuping)
            {
                walking = true;
                anim.Play("walk");
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)
            || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (!pickuping)
            {
                walking = false;
                anim.Play("idle");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.Hit(collision);
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

}
