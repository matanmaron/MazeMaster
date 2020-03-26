using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerControll Target;
    public float sensitivityX;
    public float sensitivityY;
    public float minimumY;
    public float maximumY;
    float rotationY;
    float rotationX;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!Target.GamePaused)
        {

        }
    }
}
