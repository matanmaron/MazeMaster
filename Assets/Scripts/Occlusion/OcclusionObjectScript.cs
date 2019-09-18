using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionObjectScript : MonoBehaviour
{
    [SerializeField] float DisplayTime = -1;
    Renderer rend;

    private void OnEnable()
    {
        rend = gameObject.GetComponent<Renderer>();
        DisplayTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (DisplayTime > 0)
        {
            DisplayTime -= Time.deltaTime;
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
    }

    internal void HitOcclude(float time)
    {
        DisplayTime = time;
        rend.enabled = true;
    }
}
