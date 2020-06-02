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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        var inputY = Input.GetAxis("Vertical");
        var inputX = Input.GetAxis("Horizontal");
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
}