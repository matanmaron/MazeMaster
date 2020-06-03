using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] AudioSource Bite = null;
    float Speed = 3;
    float rotSpeed = 100f;
    bool isWandering = false;
    bool isRotLeft = false;
    bool isRotRight = false;
    bool isWalking = false;
    Animator animator = null;
    AudioSource audioSource = null;
    Rigidbody rigidbody = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource.PlayDelayed(Random.Range(0, 5f));
    }

    void Update()
    {
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }
        if (isRotRight)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotLeft)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
        if (collision.gameObject.tag == "Player")
        {
            Bite.Play();
            animator.Play("attack");
            transform.LookAt(collision.transform);
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotWait = Random.Range(1, 4);
        int rotLoR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotWait);
        if (rotLoR==1)
        {
            isRotRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotRight = false;
        }
        if (rotLoR==2)
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotLeft = false;
        }
        isWandering = false;
    }
}
