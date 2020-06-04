using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Start()
    {
        if (Settings.MuteMusic)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
