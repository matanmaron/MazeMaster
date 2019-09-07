using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    enum states
    {
        up,
        down,
        wait
    }

    private states state;
    public float Speed;
    public float DownSpeed;
    public float maxY;
    private float minY;
    public int StartDelay;

    // Start is called before the first frame update
    void Start()
    {
        state = states.wait;
        StartCoroutine(waiter(StartDelay));
        minY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (state)
        {
            case states.up:
                GoUp();
                break;
            case states.down:
                GoDown();
                break;
            case states.wait:
                StartCoroutine(waiter(2));
                break;
        }
    }

    void GoUp()
    {
        if (transform.position.y < maxY)
        {
            transform.position += transform.up * Speed * Time.deltaTime;
        }
        else
        {
            state = states.down;
        }
    }

    void GoDown()
    {
        if (transform.position.y > minY)
        {
            transform.position += -1 * transform.up * Speed * Time.deltaTime * DownSpeed;
        }
        else
        {
            state = states.up;
        }
    }

    IEnumerator waiter(int sec)
    {
        //StartCoroutine(waiter());
        //Wait for 4 seconds
        yield return new WaitForSeconds(sec);
        state = states.up;
    }
}
