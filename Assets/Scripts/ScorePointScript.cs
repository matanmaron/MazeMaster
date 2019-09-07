using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointScript : MonoBehaviour
{
    public float Speed;
    public float UpSpeed;
    public bool PickedUp;
    // Start is called before the first frame update
    void Start()
    {
        PickedUp = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PickedUp)
        {
            transform.RotateAround(transform.position, Vector3.up * Speed * Time.deltaTime, 15.0f);
            transform.position += -1 * transform.right * UpSpeed * Time.deltaTime;
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.up * Speed * Time.deltaTime, 1.0f);
        }
    }

    public void PickUp()
    {
        if (!PickedUp)
        {
            var m_Collider = GetComponent<Collider>();
            m_Collider.enabled = false;
            PickedUp = true;
            StartCoroutine(waiter(1));
        }
    }

    IEnumerator waiter(int sec)
    {
        //StartCoroutine(waiter());
        //Wait for x seconds
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }
}
