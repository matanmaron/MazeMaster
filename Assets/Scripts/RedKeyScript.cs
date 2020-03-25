﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeyScript : MonoBehaviour
{
    Color colorStart = Color.white;
    Color colorEnd = Color.red;
    float duration = 1.0f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
    }
}
