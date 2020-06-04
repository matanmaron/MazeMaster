using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] GameObject CameraHolder = null;
    [SerializeField] float Speed = 15;
    float camera_minimumY = -10;
    float camera_maximumY = 50;
    float player_rotationX = 0;
    float camera_rotationY = 0;
    float camera_rotationX = 0;
    Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (GameManager.Instance.GamePaused)
        {
            return;
        }
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        var inputY = Input.GetAxis("Vertical");
        var inputX = Input.GetAxis("Horizontal");
        if (inputX != 0 || inputY != 0)
        {
            WalkAnim(true);
        }
        else
        {
            WalkAnim(false);
        }
        transform.position += transform.forward * Speed * inputY * Time.deltaTime;
        transform.position += transform.right * Speed * inputX * Time.deltaTime;
        player_rotationX += Input.GetAxis("Mouse X") * Settings.MouseSensitivity;
        transform.localEulerAngles = new Vector3(0, player_rotationX, 0);

    }

    void MoveCamera()
    {
        if (Settings.Invert)
        {
            camera_rotationY -= Input.GetAxis("Mouse Y") * Settings.MouseSensitivity;
        }
        else
        {
            camera_rotationY += Input.GetAxis("Mouse Y") * Settings.MouseSensitivity;
        }
        camera_rotationX += Input.GetAxis("Mouse X") * Settings.MouseSensitivity;
        camera_rotationY = Mathf.Clamp(camera_rotationY, camera_minimumY, camera_maximumY);
        CameraHolder.transform.localEulerAngles = new Vector3(-camera_rotationY, camera_rotationX, 0);
        CameraHolder.transform.position = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        var objName = collision.gameObject.tag;
        if (objName.Contains("Zombie"))
        {
            GameManager.Instance.HitEnemy();
        }
        else if (objName.Contains("Keys"))
        {
            PickUpAnim();
            GameManager.Instance.HitKeys(collision);
        }
        else if (objName.Contains("Door"))
        {
            GameManager.Instance.HitDoors(collision);
        }
        else if (objName.Contains("Saw"))
        {
            GameManager.Instance.HitSaw();
        }
        else if (objName.Contains("Score"))
        {
            PickUpAnim();
            GameManager.Instance.HitScore(collision);
        }
        else if (objName.Contains("Finish"))
        {
            PickUpAnim();
            GameManager.Instance.HitFinish(collision);
        }
    }

    void PickUpAnim()
    {
        animator.Play("pickup");
    }

    void WalkAnim(bool play)
    {
        if (play)
        {
            animator.Play("walk");
        }
        else
        {
            animator.Play("idle");
        }
    }
}