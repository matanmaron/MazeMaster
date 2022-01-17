using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointScript : MonoBehaviour
{
    float Speed = 100;
    public bool isUp;
    void LateUpdate()
    {
        if (isUp)
        {
            transform.RotateAround(transform.position, Vector3.up * Speed * Time.deltaTime, 10f);
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.up * Speed * Time.deltaTime, 1.0f);
        }
    }
}
