using UnityEngine;

public class Hit : MonoBehaviour
{
    GameObject AudioObject = null;
    AudioSource audioSource = null;
    void Start()
    {
        AudioObject = GameManager.Instance.AudioObject;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError($"{gameObject.name} has no audioSource");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var obj = Instantiate(AudioObject, gameObject.transform.position, gameObject.transform.rotation);
            var asrc = obj.AddComponent<AudioSource>();
            asrc.clip = audioSource.clip;
            asrc.volume = audioSource.volume;
            asrc.spatialBlend = audioSource.spatialBlend;
            asrc.priority = audioSource.priority;
            asrc.pitch = audioSource.pitch;
            asrc.Play();
        }
    }
}
