using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    void Start()
    {
        if (Settings.MuteSFX)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
