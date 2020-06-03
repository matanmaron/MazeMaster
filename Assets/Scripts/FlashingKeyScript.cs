using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingKeyScript : MonoBehaviour
{
    [SerializeField] Color colorEnd = Color.white;
    Color colorStart = Color.white;
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
