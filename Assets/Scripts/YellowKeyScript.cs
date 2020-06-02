using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowKeyScript : MonoBehaviour
{
    Color colorStart = Color.white;
    Color colorEnd = Color.yellow;
    float duration = 1.0f;
    Renderer rend = null;

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
