using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointScript : MonoBehaviour
{
    public float Speed = 100;

    void LateUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up * Speed * Time.deltaTime, 1.0f);
    }
}
