using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Settings.MuteMusic)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
